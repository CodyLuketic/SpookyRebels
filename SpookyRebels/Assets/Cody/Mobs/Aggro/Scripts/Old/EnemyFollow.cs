using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private EnemyValues valuesScript = null;
    private EnemyCombat enemyCombatScript = null;
    private NavMeshAgent enemyNav = null;

    private Transform player;
    private float distance = 0f;
    private bool isFollowing = true;

    [SerializeField]
    private float minDistance = 2f;

    private bool isMelee = false;
    private float rotationSpeed = 0f;
    private bool attackRunning = false;
    
    private void Start()
    {
        valuesScript = gameObject.GetComponent<EnemyValues>();
        enemyCombatScript = gameObject.GetComponent<EnemyCombat>();
        enemyNav = gameObject.GetComponent<NavMeshAgent>();

        //isMelee = valuesScript.GetMelee();
        rotationSpeed = valuesScript.GetRotationSpeed();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
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
        distance = Vector3.Distance(player.position, transform.position); 
        if(enemyNav != null && enemyNav.isOnNavMesh && distance > minDistance)
        {
            if(attackRunning)
            {
                enemyCombatScript.StopRangedAttack();
                attackRunning = false;
            }

            enemyNav.SetDestination(player.position);
        } else if(enemyNav != null && enemyNav.isOnNavMesh) {
            if(!attackRunning)
            {
                enemyCombatScript.StartRangedAttack();
                attackRunning = true;
            }

            enemyNav.SetDestination(transform.position);

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public bool GetIsFollowing()
    {
        return GetIsFollowingHelper();
    }
    private bool GetIsFollowingHelper()
    {
        return isFollowing;
    }
}
