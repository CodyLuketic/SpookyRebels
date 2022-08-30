using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private MonsterScriptableObject monsterParts = null;

    [SerializeField]
    private NavMeshAgent enemy = null;

    [SerializeField]
    private Transform player = null;

    // Start is called before the first frame update
    private void Start()
    {
        enemy.speed = monsterParts.speed;
    }

    // Update is called once per frame
    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        enemy.SetDestination(player.position);
    }
}
