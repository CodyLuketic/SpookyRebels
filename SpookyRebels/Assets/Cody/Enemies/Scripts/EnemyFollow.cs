using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private EnemyValues valuesScript = null;
    private NavMeshAgent enemyNav = null;

    private Transform player;

    [SerializeField]
    private float minDistance = 2f;

    private bool isMelee = false;
    
    private void Start()
    {
        valuesScript = gameObject.GetComponent<EnemyValues>();
        isMelee = valuesScript.GetMelee();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(isMelee)
        {
            FollowMelee();
        } else {
            FollowRanged();
        }
    }

    private void FollowMelee()
    { 
        if(enemyNav != null && enemyNav.isOnNavMesh)
        {
            enemyNav.SetDestination(player.position);
        }
    }

    private void FollowRanged()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance < 0)
        {
            distance *= -1;
        }
        
        if(enemyNav != null && enemyNav.isOnNavMesh && distance > minDistance)
        {
            enemyNav.SetDestination(player.position);
        } else {
            enemyNav.SetDestination(transform.position);
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
