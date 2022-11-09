using UnityEngine;
using UnityEngine.AI;

public class DodoFollow : MonoBehaviour
{
    private DodoValues dodoValuesScript = null;
    private DodoCombat dodoCombatScript = null;
    private NavMeshAgent dodoNav = null;

    private Transform player;

    private float distance = 0f;
    [SerializeField]
    private float minDistance = 2f;
    private float rotationSpeed = 0f;
    private bool attackRunning = false;
    
    private void Start()
    {
        dodoValuesScript = gameObject.GetComponent<DodoValues>();
        dodoCombatScript = gameObject.GetComponent<DodoCombat>();
        dodoNav = gameObject.GetComponent<NavMeshAgent>();

        rotationSpeed = dodoValuesScript.GetRotationSpeed();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        FollowRanged();
    }

    private void FollowRanged()
    {
        distance = Vector3.Distance(player.position, transform.position); 
        if(dodoNav != null && dodoNav.isOnNavMesh && distance > minDistance)
        {
            if(attackRunning)
            {
                dodoCombatScript.StopRangedAttack();
                attackRunning = false;
            }

            dodoNav.SetDestination(player.position);
        } else if(dodoNav != null && dodoNav.isOnNavMesh) {
            if(!attackRunning)
            {
                dodoCombatScript.StartRangedAttack();
                attackRunning = true;
            }

            dodoNav.SetDestination(transform.position);

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
