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

    [Header("Melee Only")]
    [SerializeField]
    private float waitTimeAttacking = 0f;

    private bool canMelee = false;
    private float tempSpeed = 0f;
    private Coroutine rangedAttack = null;

    private void Start()
    {
        enemyValuesScript = gameObject.GetComponent<EnemyValues>();
        enemyFollowScript = gameObject.GetComponent<EnemyFollow>();
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        bulletPooler = GameObject.FindGameObjectWithTag("BulletPooler").GetComponent<BulletPooler>();

        canMelee = enemyValuesScript.GetMelee();
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

    public void StartRangedAttack()
    {
        rangedAttack = StartCoroutine(RangedAttack());
    }
    private IEnumerator RangedAttack()
    {
        animator.SetBool("attacking", true);
        tempSpeed = enemyValuesScript.GetSpeed();
        enemyValuesScript.SetSpeed(0);
        while(true)
        {
            bulletPooler.SpawnFromPool(transform);
            yield return new WaitForSeconds(enemyValuesScript.GetAttackSpeed());
        }
    }
    public void StopRangedAttack()
    {
        StopCoroutine(rangedAttack);
        animator.SetBool("attacking", false);
        enemyValuesScript.SetSpeed(tempSpeed);
    }
}
