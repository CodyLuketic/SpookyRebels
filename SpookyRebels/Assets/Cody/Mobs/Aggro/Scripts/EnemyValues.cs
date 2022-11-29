using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyValues : MonoBehaviour
{
    private GameManager gameManager = null;
    private WalkingSounds walkingSoundsScript = null;
    private Animator animator = null;

    [Header("Basic Values")]
    [SerializeField]
    private float _health = 1f;

    [SerializeField]
    private float _speed = 1f;
    private float tempSpeed = 1f;

    [SerializeField]
    private float _damage = 1f;

    [SerializeField]
    private float _attackSpeed = 1f;
    
    [SerializeField]
    private float _rotationSpeed = 1f;

    [SerializeField]
    private float animDeathTime = 1f;

    [SerializeField]
    private bool isBoss = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        walkingSoundsScript = gameObject.GetComponent<WalkingSounds>();
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
        if(!isBoss)
        {
            walkingSoundsScript.StartWalkingLoop();
        }
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
        gameObject.GetComponent<BoxCollider>().enabled = false;
        ZeroSpeedHelper();
        if(!isBoss)
        {
            walkingSoundsScript.StopWalkingLoop();
        }

        yield return new WaitForSeconds(animDeathTime);

        if(isBoss)
        {
            gameManager.Win();
        }
        gameObject.SetActive(false);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
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
