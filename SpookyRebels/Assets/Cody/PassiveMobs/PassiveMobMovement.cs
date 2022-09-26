using UnityEngine;
using UnityEngine.AI;

public class PassiveMobMovement : MonoBehaviour
{
    private NavMeshAgent passiveMobNav = null;

    private GameObject player = null;

    private Vector3 spawnPoint;

    [SerializeField]
    private float spawnRadius = 0;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        float ranX = Random.Range(-spawnPoint.x * spawnRadius, spawnPoint.x * spawnRadius);
        float ranZ = Random.Range(-spawnPoint.z * spawnRadius, spawnPoint.z * spawnRadius);

        //Vector3 moveTo = new Vector3(
       //         Random.insideUnitCircle.normalized.x + ranX,
        //        0,
        //        circlePoint.normalized.z + ranZ) 
         //       * maxRadius;

        if(passiveMobNav != null && passiveMobNav.isOnNavMesh)
        {
            passiveMobNav.SetDestination(player.transform.position);
        }
        else if(passiveMobNav != null)
        {
            Destroy(gameObject);
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
