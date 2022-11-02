using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    private EnemyValues enemyValuesScript = null;
    private EnemyFollow enemyFollowScript = null;
    private BulletPooler bulletPooler = null;
    private Rigidbody enemyRb = null;
    private NavMeshAgent enemyNav = null;
    private Animator animator = null;

    private bool isMelee = false;
    private Coroutine rangedAttack = null;
    private Coroutine meleeAttack = null;
    private int attackChoice = 0;

    private void Start()
    {
        enemyValuesScript = gameObject.GetComponent<EnemyValues>();
        enemyFollowScript = gameObject.GetComponent<EnemyFollow>();
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

        bulletPooler = GameObject.FindGameObjectWithTag("BulletPooler").GetComponent<BulletPooler>();

        isMelee = enemyValuesScript.GetMelee();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isMelee && other.gameObject.CompareTag("Player") && gameObject.activeSelf)
        {
            enemyValuesScript.ZeroSpeed();
            StartMeleeAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(isMelee && other.gameObject.CompareTag("Player") && gameObject.activeSelf)
        {
            enemyValuesScript.ResetSpeed();
            StopMeleeAttack();
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.CompareTag("Bullet"))
        {
            // Change 1 to player damage
            enemyValuesScript.SetHealth(enemyValuesScript.GetHealth() - 1);
            Destroy(other.gameObject);
        }
    }

    public void StartMeleeAttack()
    {
        StartMeleeAttackHelper();
    }
    private void StartMeleeAttackHelper()
    {
        animator.speed = enemyValuesScript.GetAttackSpeed() / 3.4f;
        meleeAttack = StartCoroutine(MeleeAttack());
    }
    private IEnumerator MeleeAttack()
    {
        attackChoice = Random.Range(0, 2);

        if(attackChoice == 0)
        {
            animator.SetBool("Attack1", true);
        } else {
            animator.SetBool("Attack2", true);
        }

        while(true)
        {
            // Player Damage Call Goes Here

            yield return new WaitForSeconds(enemyValuesScript.GetAttackSpeed());
        }
    }
    public void StopMeleeAttack()
    {
        StopMeleeAttackHelper();
    }
    private void StopMeleeAttackHelper()
    {
        StopCoroutine(meleeAttack);
        if(attackChoice == 0)
        {
            animator.SetBool("Attack1", false);
        } else {
            animator.SetBool("Attack2", false);
        }
    }

    public void StartRangedAttack()
    {
        StartRangedAttackHelper();
    }
    private void StartRangedAttackHelper()
    {
        animator.speed = enemyValuesScript.GetAttackSpeed() / 3.4f;
        rangedAttack = StartCoroutine(RangedAttack());
    }
    private IEnumerator RangedAttack()
    {
        enemyValuesScript.ZeroSpeed();
        animator.SetBool("Attack", true);
        while(true)
        {
            bulletPooler.SpawnFromPool(transform);
            yield return new WaitForSeconds(enemyValuesScript.GetAttackSpeed());
        }
    }
    public void StopRangedAttack()
    {
        StopRangedAttackHelper();
    }
    private void StopRangedAttackHelper()
    {
        enemyValuesScript.ResetSpeed();
        animator.SetBool("Attack", false);
        StopCoroutine(rangedAttack);
    }
}
