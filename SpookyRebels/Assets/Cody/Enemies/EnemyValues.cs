using UnityEngine;
using UnityEngine.AI;

public class EnemyValues : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyParts = null;

    [Header("Basic Values")]
    [SerializeField]
    private Mesh _mesh = null;
    
    [SerializeField]
    private Material _material = null;

    [SerializeField]
    private int _health = 0;

    [SerializeField]
    private float _speed = 0;

    [SerializeField]
    private float _damage = 0;

    [SerializeField]
    private float _defense = 0;
    
    [Header("Melee Values")]
    [SerializeField]
    private float _bounceBack = 0;

    [Header("Range Values")]
    [SerializeField]
    private float _attackSpeed = 0;


    private void Start()
    {
        SetValues();
        ApplyValues();
    }

    private void SetValues()
    {
        _mesh = enemyParts.enemyMesh;
        _material = enemyParts.enemyMaterial;

        _speed = enemyParts.speed;
        _health = enemyParts.health;
        _damage = enemyParts.damage;
        _defense = enemyParts.defense;

        _bounceBack = enemyParts.bounceBack;
        _attackSpeed = enemyParts.attackSpeed;
    }

    private void ApplyValues()
    {
        gameObject.GetComponent<MeshFilter>().mesh = _mesh;
        gameObject.GetComponent<MeshRenderer>().material = _material;

        gameObject.GetComponent<NavMeshAgent>().speed = _speed;
    }

    public void SetEnemyParts(EnemyScriptableObject enemyScriptableObject)
    {
        SetEnemyPartsHelper(enemyScriptableObject);
    }
    private void SetEnemyPartsHelper(EnemyScriptableObject enemyScriptableObject)
    {
        enemyParts = enemyScriptableObject;
    }

    public void SetMesh(Mesh mesh)
    {
        SetMeshHelper(mesh);
    }
    private void SetMeshHelper(Mesh mesh)
    {
        _mesh = mesh;
    }

    public void SetMaterial(Material material)
    {
        SetMaterialHelper(material);
    }
    private void SetMaterialHelper(Material material)
    {
        _material = material;
    }

    public void SetSpeed(float speed)
    {
        SetSpeedHelper(speed);
    }
    private void SetSpeedHelper(float speed)
    {
        _speed = speed;
    }

    public void SetHealth(int health)
    {
        SetHealthHelper(health);
    }
    private void SetHealthHelper(int health)
    {
        _health = health;
    }

    public void SetDamage(float damage)
    {
        SetDamageHelper(damage);
    }
    private void SetDamageHelper(float damage)
    {
        _damage = damage;
    }

    public void SetDefense(float defense)
    {
        SetDefenseHelper(defense);
    }
    private void SetDefenseHelper(float defense)
    {
        _defense = defense;
    }

    public void SetBounceBack(float bounceBack)
    {
        SetBounceBackHelper(bounceBack);
    }
    private void SetBounceBackHelper(float bounceBack)
    {
        _bounceBack = bounceBack;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        SetAttackSpeedHelper(attackSpeed);
    }
    private void SetAttackSpeedHelper(float attackSpeed)
    {
        _attackSpeed = attackSpeed;
    }
}
