using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobMovement : MonoBehaviour
{
    private NavMeshAgent passiveMobNav = null;

    private Rigidbody passiveMobRb = null;

    private GameObject player = null;

    private Vector3 _spawnPoint;

    [SerializeField]
    private float spawnRadius = 0;
    
    [SerializeField]
    private float detectRadius = 0;

    [SerializeField]
    private float runDistance = 0;
    
    [SerializeField]
    private float nextTurnTime = 0;

    private bool running = false;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        passiveMobRb = gameObject.GetComponent<Rigidbody>();

        StartCoroutine(PhysicsConst());
    }

    private void Update()
    {
        if(running && Time.time > nextTurnTime)
        {
            MoveAway();
        }
        else
        {
            MoveTo();
        }

        CheckPlayerPos();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            ResetPhysics();
        }
    }

    private void MoveTo()
    {
        float ranX = Random.Range(-_spawnPoint.x * spawnRadius, _spawnPoint.x * spawnRadius);
        float ranZ = Random.Range(-_spawnPoint.z * spawnRadius, _spawnPoint.z * spawnRadius);

        Vector3 moveTo = new Vector3(ranX, 0, ranZ);

        if(passiveMobNav != null && passiveMobNav.isOnNavMesh)
        {
            passiveMobNav.SetDestination(moveTo);
        }
        else if(passiveMobNav != null)
        {
            Destroy(gameObject);
        }
    }

    private void MoveAway()
    {
        Transform startTransform = transform;
        
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

        Vector3 runTo = transform.position + transform.forward * runDistance;
        Debug.Log("runTo = " + runTo);
        
        NavMeshHit hit;

        NavMesh.SamplePosition(runTo, out hit, 5, 1); 
        Debug.Log("hit = " + hit + " hit.position = " + hit.position);

        nextTurnTime = Time.time + 5;

        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        passiveMobNav.SetDestination(hit.position);
    }

    private void CheckPlayerPos()
    {
        if((player.transform.position - transform.position).sqrMagnitude < detectRadius * detectRadius)
        {
            running = true;
        }
    }

    private IEnumerator PhysicsConst()
    {
        yield return new WaitForSeconds(1);
        ResetPhysics();
    }

    private void ResetPhysics()
    {
        passiveMobRb.velocity = Vector3.zero;
        passiveMobRb.angularVelocity = Vector3.zero;
    }

    public void SetNavAgent()
    {
        SetNavAgentHelper();
    }

    private void SetNavAgentHelper()
    {
        passiveMobNav = gameObject.GetComponent<NavMeshAgent>();
    }

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        SetSpawnPointHelper(spawnPoint);
    }

    private void SetSpawnPointHelper(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }
}
