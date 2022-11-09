using UnityEngine;
using UnityEngine.AI;

public class CrabFollow : MonoBehaviour
{
    private CrabValues crabValuesScript = null;
    private CrabCombat crabCombatScript = null;
    private NavMeshAgent crabNav = null;

    private Transform player;
    
    private void Start()
    {
        crabValuesScript = gameObject.GetComponent<CrabValues>();
        crabCombatScript = gameObject.GetComponent<CrabCombat>();
        crabNav = gameObject.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        FollowMelee();
    }

    private void FollowMelee()
    {
        crabNav.SetDestination(player.position);

    }
}
