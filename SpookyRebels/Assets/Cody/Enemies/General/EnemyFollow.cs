using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent enemyNav = null;

    private GameObject player = null;

    private Vector3 position;
    
    private void Start()
    {
        enemyNav = gameObject.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if(enemyNav.isOnNavMesh)
        {
            enemyNav.SetDestination(player.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
