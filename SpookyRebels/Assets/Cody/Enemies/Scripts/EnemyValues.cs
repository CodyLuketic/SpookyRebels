using UnityEngine;
using UnityEngine.AI;

public class EnemyValues : MonoBehaviour
{
    private GameManager gameManager = null;
    private EnemyFollow enemyFollowScript = null;
    //private EnemyMeleeCombat enemyCombatScript = null;

    [Header("Basic Values")]
    [SerializeField]
    private float _health = 0;

    [SerializeField]
    private float _speed = 0;

    [SerializeField]
    private float _damage = 0;

    [SerializeField]
    private float _defense = 0;

    [SerializeField]
    private bool _melee = false;

    [SerializeField]
    private bool isBoss = false;
    
    [Header("Melee Values")]
    [SerializeField]
    private float _bounceBack = 0;

    [Header("Ranged Values")]
    [SerializeField]
    private float _attackSpeed = 0;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        enemyFollowScript = gameObject.GetComponent<EnemyFollow>();
        //enemyCombatScript = gameObject.GetComponent<EnemyCombat>();
    }

    public void ApplyValues()
    {
        ApplyValuesHelper();
    }

    private void ApplyValuesHelper()
    {
        gameObject.GetComponent<EnemyFollow>().SetNavAgent();
       // gameObject.GetComponent<EnemyCombat>().SetNavAgent();
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
        _defense += level;

        _bounceBack += level;
        _attackSpeed += level;

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

    public void SetHealth(float health)
    {
        SetHealthHelper(health);
    }
    private void SetHealthHelper(float health)
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
