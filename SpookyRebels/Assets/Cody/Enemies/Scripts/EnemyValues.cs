using UnityEngine;
using UnityEngine.AI;

public class EnemyValues : MonoBehaviour
{
    private GameManager gameManager = null;
    private EnemyFollow enemyFollowScript = null;
    private EnemyCombat enemyCombatScript = null;

    [Header("Basic Values")]
    [SerializeField]
    private int _health = 0;

    [SerializeField]
    private float _speed = 0;

    [SerializeField]
    private float _damage = 0;

    [Header("Enemy Type")]
    [SerializeField]
    private bool isMelee = false;

    [SerializeField]
    private bool isBoss = false;
    
    [Header("Melee Values")]
    [SerializeField]
    private float _bounceBack = 0;

    [Header("Ranged Values")]
    [SerializeField]
    private float _attackSpeed = 0;
    private float _rotationSpeed = 0;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        enemyFollowScript = gameObject.GetComponent<EnemyFollow>();
        enemyCombatScript = gameObject.GetComponent<EnemyCombat>();
    }

    public void ApplyValues()
    {
        ApplyValuesHelper();
    }

    private void ApplyValuesHelper()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = _speed;
    }

    public void IncreaseValues(int level)
    {
        IncreaseValuesHelper(level);
    }

    private void IncreaseValuesHelper(int level)
    {
        _speed += level;
        _health += level;
        _damage += level;

        _bounceBack += level;
        _attackSpeed += level;
        _rotationSpeed += level;

        ApplyValuesHelper();
    }

    // Setters
    public void SetSpeed(float speed)
    {
        SetSpeedHelper(speed);
    }
    private void SetSpeedHelper(float speed)
    {
        _speed = speed;
        gameObject.GetComponent<NavMeshAgent>().speed = _speed;
    }

    public void SetHealth(int health)
    {
        SetHealthHelper(health);
    }
    private void SetHealthHelper(int health)
    {
        _health = health;
        
        if(_health <= 0)
        {
            if(isBoss)
            {
                gameManager.Win();
            }

            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void SetDamage(float damage)
    {
        SetDamageHelper(damage);
    }
    private void SetDamageHelper(float damage)
    {
        _damage = damage;
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

    public void SetRotationSpeed(float rotationSpeed)
    {
        SetRotationSpeedHelper(rotationSpeed);
    }
    private void SetRotationSpeedHelper(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }

    // Getters
    public float GetSpeed()
    {
        return GetSpeedHelper();
    }
    private float GetSpeedHelper()
    {
        return _speed;
    }

    public int GetHealth()
    {
        return GetHealthHelper();
    }
    private int GetHealthHelper()
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

    public bool GetMelee()
    {
        return GetMeleeHelper();
    }
    private bool GetMeleeHelper()
    {
        return isMelee;
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

    public float GetRotationSpeed()
    {
        return GetRotationSpeedHelper();
    }
    private float GetRotationSpeedHelper()
    {
        return _rotationSpeed;
    }
}
