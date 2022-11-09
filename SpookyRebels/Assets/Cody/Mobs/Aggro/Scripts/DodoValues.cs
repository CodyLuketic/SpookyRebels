using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DodoValues : MonoBehaviour
{
    private GameManager gameManager = null;
    private DodoFollow dodoFollowScript = null;
    private DodoCombat dodoCombatScript = null;
    private Animator animator = null;

    [Header("Basic Values")]
    [SerializeField]
    private float _health = 0f;

    [SerializeField]
    private float _speed = 0f;
    private float tempSpeed = 0;

    [SerializeField]
    private float _damage = 0f;

    [SerializeField]
    private float _attackSpeed = 0f;
    private float _rotationSpeed = 0f;

    [SerializeField]
    private float animDeathTime = 1f;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        dodoFollowScript = gameObject.GetComponent<DodoFollow>();
        dodoCombatScript = gameObject.GetComponent<DodoCombat>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

        tempSpeed = _speed;
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

        _attackSpeed += level;
        _rotationSpeed += level;

        ApplyValuesHelper();
    }

    // Setters
    public void ZeroSpeed()
    {
        ZeroSpeedHelper();
    }
    private void ZeroSpeedHelper()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
    }
    public void ResetSpeed()
    {
        ResetSpeedHelper();
    }
    private void ResetSpeedHelper()
    {
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
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        animator.SetTrigger("Die");
        ZeroSpeedHelper();

        yield return new WaitForSeconds(animDeathTime);

        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.SetActive(false);
        ResetSpeedHelper();
    }

    public void SetDamage(float damage)
    {
        SetDamageHelper(damage);
    }
    private void SetDamageHelper(float damage)
    {
        _damage = damage;
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
