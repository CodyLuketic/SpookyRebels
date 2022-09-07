using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyParts = null;

    [SerializeField]
    private NavMeshAgent enemy = null;

    [SerializeField]
    private Transform player = null;

    // Update is called once per frame
    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        enemy.SetDestination(player.position);
    }

    public void SetEnemyParts(EnemyScriptableObject enemyScriptableObject)
    {
        SetEnemyPartsHelper(enemyScriptableObject);
    }
    private void SetEnemyPartsHelper(EnemyScriptableObject enemyScriptableObject)
    {
        enemyParts = enemyScriptableObject;
    }
}
