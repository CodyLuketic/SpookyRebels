using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobMovement : MonoBehaviour
{
    private NavMeshAgent passiveMobNav = null;

    private Rigidbody passiveMobRb = null;

    private GameObject player = null;

    private Vector3 position;

    [Header("Melee Only")]
    [SerializeField]
    private float waitTime = 0;
    
    private void Start()
    {
        passiveMobRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if(passiveMobNav != null && passiveMobNav.isOnNavMesh)
        {
            passiveMobNav.SetDestination(player.transform.position);
        }
        else if(passiveMobNav != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Bounce(other));
        }

        if(other.gameObject.CompareTag("PassiveMob"))
        {
            passiveMobRb.velocity = Vector3.zero;
            passiveMobRb.angularVelocity = Vector3.zero;
        }
    }

    private IEnumerator Bounce(Collision other)
    {
        float tempSpeed = passiveMobNav.speed;
        passiveMobNav.speed = 0;

        float bounce = 6f; //amount of force to apply
        passiveMobRb.AddForce(other.GetContact(0).normal * bounce, ForceMode.Impulse);

        yield return new WaitForSeconds(waitTime);
        passiveMobNav.speed = tempSpeed;
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
}
