using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent enemyNav = null;

    private Rigidbody enemyRb = null;

    private GameObject player = null;

    private Vector3 position;

    [Header("Melee Only")]
    [SerializeField]
    private float waitTime = 0;
    
    private void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if(enemyNav != null && enemyNav.isOnNavMesh)
        {
            enemyNav.SetDestination(player.transform.position);
        }
        else if(enemyNav != null)
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

        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyRb.velocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;
        }
    }

    private IEnumerator Bounce(Collision other)
    {
        float tempSpeed = enemyNav.speed;
        enemyNav.speed = 0;

        float bounce = 6f; //amount of force to apply
        enemyRb.AddForce(other.GetContact(0).normal * bounce, ForceMode.Impulse);

        yield return new WaitForSeconds(waitTime);
        enemyNav.speed = tempSpeed;
        enemyRb.velocity = Vector3.zero;
        enemyRb.angularVelocity = Vector3.zero;
    }

    public void SetNavAgent()
    {
        SetNavAgentHelper();
    }

    private void SetNavAgentHelper()
    {
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
    }
}
