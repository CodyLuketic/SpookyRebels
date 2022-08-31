using UnityEngine;

public class EnemyValues : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyParts = null;

    [Header("Basic Values")]
    [SerializeField]
    private Mesh enemyMesh = null;
    
    [SerializeField]
    private Material enemyMaterial = null;

    [SerializeField]
    private int health = 0;

    [SerializeField]
    private float damage = 0;

    [SerializeField]
    private float defense = 0;
    
    [Header("Melee Values")]
    [SerializeField]
    private float bounceBack = 0;

    [Header("Range Values")]
    [SerializeField]
    private float attackSpeed = 0;


    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        enemyMesh = enemyParts.enemyMesh;
        enemyMaterial = enemyParts.enemyMaterial;
        health = enemyParts.health;
        damage = enemyParts.damage;
        defense = enemyParts.defense;
        bounceBack = enemyParts.bounceBack;
        attackSpeed = enemyParts.attackSpeed;
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
