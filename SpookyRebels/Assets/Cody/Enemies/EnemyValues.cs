using UnityEngine;
using UnityEngine.AI;

public class EnemyValues : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject _enemyParts = null;

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

    [SerializeField]
    private bool _melee = false;
    
    [Header("Melee Values")]
    [SerializeField]
    private float _bounceBack = 0;

    [Header("Range Values")]
    [SerializeField]
    private float _attackSpeed = 0;

    private void SetValues()
    {
        _mesh = _enemyParts.enemyMesh;
        _material = _enemyParts.enemyMaterial;

        _speed = _enemyParts.speed;
        _health = _enemyParts.health;
        _damage = _enemyParts.damage;
        _defense = _enemyParts.defense;

        _melee = _enemyParts.melee;
        _bounceBack = _enemyParts.bounceBack;
        _attackSpeed = _enemyParts.attackSpeed;
    }

    private void ApplyValues()
    {
        gameObject.GetComponent<MeshFilter>().mesh = _mesh;
        gameObject.GetComponent<MeshRenderer>().material = _material;

        gameObject.GetComponent<NavMeshAgent>().speed = _speed;
    }

    private void IncreaseValues(int level)
    {
        _speed += level;
        _health += level;
        _damage += level;
        _defense += level;

        _bounceBack += level;
        _attackSpeed += level;
    }

    // Setters
    public void SetEnemyParts(EnemyScriptableObject enemyParts, int level)
    {
        SetEnemyPartsHelper(enemyParts, level);
    }
    private void SetEnemyPartsHelper(EnemyScriptableObject enemyParts, int level)
    {
        _enemyParts = enemyParts;
        SetValues();
        IncreaseValues(level);
        ApplyValues();
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

    // Getters
    public Mesh GetMesh()
    {
        return GetMeshHelper();
    }
    private Mesh GetMeshHelper()
    {
        return _mesh;
    }

    public Material GetMaterial()
    {
        return GetMaterialHelper();
    }
    private Material GetMaterialHelper()
    {
        return _material;
    }

    public float GetSpeed()
    {
        return GetSpeedHelper();
    }
    private float GetSpeedHelper()
    {
        return _speed;
    }

    public float GetHealth()
    {
        return GetHealthHelper();
    }
    private float GetHealthHelper()
    {
        return _health;
    }

    public float GetDamage()
    {
        return GetDamageHelper();
    }
    private float GetDamageHelper()
    {
        return _damage;
    }

    public float GetDefense()
    {
        return GetDefenseHelper();
    }
    private float GetDefenseHelper()
    {
        return _defense;
    }

    public bool GetMelee()
    {
        return GetMeleeHelper();
    }
    private bool GetMeleeHelper()
    {
        return _melee;
    }

    public float GetBounceBack()
    {
        return GetBounceBackHelper();
    }
    private float GetBounceBackHelper()
    {
        return _bounceBack;
    }

    public float GetAttackSpeed()
    {
        return GetAttackSpeedHelper();
    }
    private float GetAttackSpeedHelper()
    {
        return _attackSpeed;
    }
}
