using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobMovement : MonoBehaviour
{
    private NavMeshAgent passiveMobNav = null;

    private GameObject player = null;

    private Vector3 spawnPoint;

    [SerializeField]
    private float moveDistance = 0;
    
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
        //float x = Random.RandomRange(spawnPoint.x, spawnPoint.x * moveDistance);

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
