using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobMovement : MonoBehaviour
{
    private NavMeshAgent passiveMobNav = null;

    private Rigidbody passiveMobRb = null;

    private GameObject player = null;

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
    }

    private void Update()
    {
        if(CheckPlayerPos() && Time.time > nextTurnTime)
        {
            MoveAway();
        }
        else if(!CheckPlayerPos() && Time.time > nextTurnTime)
        {
            MoveTo();
        }
    }

    private void MoveTo()
    {
        float ranX = Random.Range(-transform.position.x * spawnRadius, transform.position.x * spawnRadius);
        float ranZ = Random.Range(-transform.position.z * spawnRadius, transform.position.z * spawnRadius);

        Vector3 moveTo = new Vector3(ranX, 0, ranZ);

        if(passiveMobNav != null && passiveMobNav.isOnNavMesh)
        {
            passiveMobNav.SetDestination(moveTo);
        }
        else if(passiveMobNav != null)
        {
            Destroy(gameObject);
        }

        nextTurnTime = Time.time + 5;
    }

    private void MoveAway()
    {
        Transform startTransform = transform;
        
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

        Vector3 runTo = transform.position + transform.forward * runDistance;
        
        NavMeshHit hit;

        NavMesh.SamplePosition(runTo, out hit, 5, 1); 

        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        passiveMobNav.SetDestination(hit.position);

        nextTurnTime = Time.time + 5;
    }

    private bool CheckPlayerPos()
    {
        if((player.transform.position - transform.position).sqrMagnitude < detectRadius * detectRadius)
        {
            return true;
        } else {
            return false;
        }
    }

    public void SetNavAgent()
    {
        SetNavAgentHelper();
    }

    private void SetNavAgentHelper()
    {
        passiveMobNav = gameObject.GetComponent<NavMeshAgent>();
    }
}
