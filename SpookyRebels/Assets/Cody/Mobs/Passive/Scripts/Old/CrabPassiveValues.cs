using UnityEngine;
using UnityEngine.AI;

public class CrabPassiveValues : MonoBehaviour
{
    [Header("Basic Values")]
    [SerializeField]
    private float _health = 1f;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private float _damage = 1f;

    [SerializeField]
    private float _defense = 1f;

    public void ApplyValues()
    {
        ApplyValuesHelper();
    }

    private void ApplyValuesHelper()
    {
        gameObject.GetComponent<PassiveMobMovement>().SetNavAgent();
        gameObject.GetComponent<NavMeshAgent>().speed = _speed;
    }

    public void IncreaseValues(int level)
    {
        IncreaseValuesHelper(level);
    }

    private void IncreaseValuesHelper(int level)
    {
        _speed += level;
        _damage += level;
        _defense += level;

        ApplyValues();
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
            Destroy(gameObject);
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
}
