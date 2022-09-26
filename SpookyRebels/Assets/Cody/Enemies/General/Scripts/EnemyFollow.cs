using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent enemyNav = null;

    private GameObject player = null;

    private Vector3 position;
    
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
        if(enemyNav != null && enemyNav.isOnNavMesh)
        {
            enemyNav.SetDestination(player.transform.position);
        }
        else if(enemyNav != null)
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
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
    }
}
