using UnityEngine;
using UnityEngine.AI;

public class DodoFollow : MonoBehaviour
{
    private EnemyValues valuesScript = null;
    private DodoCombat combatScript = null;
    private NavMeshAgent dodoNav = null;
    private Transform player;

    private float distance = 0f;
    [SerializeField]
    private float minDistance = 2f;
    private float rotationSpeed = 0f;
    private bool attackRunning = false;
    
    private void Start()
    {
        valuesScript = gameObject.GetComponent<EnemyValues>();
        combatScript = gameObject.GetComponent<DodoCombat>();
        dodoNav = gameObject.GetComponent<NavMeshAgent>();

        rotationSpeed = valuesScript.GetRotationSpeed();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        FollowRanged();
    }

    private void FollowRanged()
    {
        distance = Vector3.Distance(player.position, transform.position); 
        if(distance > minDistance)
        {
            if(attackRunning)
            {
                combatScript.StopRangedAttack();
                attackRunning = false;
            }

            dodoNav.SetDestination(player.position);
        } else {
            if(!attackRunning)
            {
                combatScript.StartRangedAttack();
                attackRunning = true;
            }

            dodoNav.SetDestination(transform.position);

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
