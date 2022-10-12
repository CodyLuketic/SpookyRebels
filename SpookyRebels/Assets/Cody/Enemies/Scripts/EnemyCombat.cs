using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    private EnemyValues enemyValuesScript = null;
    private BulletPooler bulletPooler = null;
    private Rigidbody enemyRb = null;
    private NavMeshAgent enemyNav = null;
    private Animator animator = null;

    [Header("Melee Only")]
    [SerializeField]
    private float waitTimeAttacking = 0;
    [SerializeField]
    private float waitTimeStill = 0;
    private bool canMelee = false;

    [Header("Ranged Only")]
    [SerializeField]
    private Transform firePoint = null;

    private void Awake()
    {
        bulletPooler = GameObject.FindGameObjectWithTag("BulletPooler").GetComponent<BulletPooler>();
        enemyValuesScript = gameObject.GetComponent<EnemyValues>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        AttackStart(enemyValuesScript.GetMelee());
    }
    private void AttackStart(bool melee)
    {
        if(melee)
        {
            canMelee = true;
        }
        else
        {
            StartCoroutine(RangedAttack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canMelee && gameObject.activeSelf)
        {
            StartCoroutine(MeleeAttack(other));
            canMelee = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.CompareTag("Bullet"))
        {
            enemyValuesScript.SetHealth(enemyValuesScript.GetHealth() - 1);
            Destroy(other.gameObject);
        }

        //if(other.gameObject.CompareTag("EnemyBullet"))
        //{
        //    other.gameObject.SetActive(false);
        //}
    }

    private IEnumerator MeleeAttack(Collider other)
    {
        float tempSpeed = enemyValuesScript.GetSpeed();
        animator.SetBool("attacking", true);
        enemyValuesScript.SetSpeed(0);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeAttacking);
        animator.SetBool("attacking", false);
        enemyValuesScript.SetSpeed(tempSpeed);
        canMelee = true;
    }

    private IEnumerator RangedAttack()
    {
        while (true)
        {
            bulletPooler.SpawnFromPool(firePoint);
            yield return new WaitForSeconds(enemyValuesScript.GetAttackSpeed());
        }
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
