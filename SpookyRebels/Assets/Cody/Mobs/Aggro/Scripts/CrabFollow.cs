using UnityEngine;
using UnityEngine.AI;

public class CrabFollow : MonoBehaviour
{
    private NavMeshAgent nav = null;
    private Transform player;
    
    private void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        FollowMelee();
    }

    private void FollowMelee()
    {
        nav.SetDestination(player.position);
    }
}
