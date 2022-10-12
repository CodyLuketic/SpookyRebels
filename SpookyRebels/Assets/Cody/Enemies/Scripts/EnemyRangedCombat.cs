using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedCombat : MonoBehaviour
{
    private EnemyValues enemyValuesScript = null;

    private BulletPooler bulletPooler = null;

    private Rigidbody enemyRb = null;

    private NavMeshAgent enemyNav = null;

    private Animator animator = null;

    private void Start()
    {
        bulletPooler = GameObject.FindGameObjectWithTag("BulletPooler").GetComponent<BulletPooler>();

        enemyValuesScript = gameObject.GetComponent<EnemyValues>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        StartCoroutine(RangedAttack());
        StartCoroutine(PhysicsConst());
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("PassiveMob"))
        {
            ResetPhysics();
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            enemyValuesScript.SetHealth(enemyValuesScript.GetHealth() - 1);

            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator RangedAttack()
    {
        while (true)
        {
            bulletPooler.SpawnFromPool(transform);

            yield return new WaitForSeconds(enemyValuesScript.GetAttackSpeed());
        }
    }
    
    private IEnumerator PhysicsConst()
    {
        yield return new WaitForSeconds(1);
        ResetPhysics();
    }

    private void ResetPhysics()
    {
        enemyRb.velocity = Vector3.zero;
        enemyRb.angularVelocity = Vector3.zero;
    }

    public void SetNavAgent()
    {
        SetNavAgentHelper();
    }

    private void SetNavAgentHelper()
    {
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
    }
}
