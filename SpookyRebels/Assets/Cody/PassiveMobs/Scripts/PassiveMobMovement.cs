using UnityEngine;
using UnityEngine.AI;

public class PassiveMobMovement : MonoBehaviour
{
    private NavMeshAgent passiveMobNav = null;

    private GameObject player = null;

    private Vector3 _spawnPoint;

    [SerializeField]
    private float spawnRadius = 0;

    private bool running = false;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(running)
        {

        }
        else
        {
            MoveTo();
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
