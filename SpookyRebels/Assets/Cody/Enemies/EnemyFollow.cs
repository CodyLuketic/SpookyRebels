using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent enemy = null;

    [SerializeField]
    private Transform player = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        enemy.SetDestination(player.position);
    }
}
