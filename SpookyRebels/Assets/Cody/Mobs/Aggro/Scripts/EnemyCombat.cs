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
    [SerializeField]
    private float attackSpeedModifier = 0f;

    [SerializeField]
    private GameObject bullet = null;

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
            // Change to player damage
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
        animator.speed = enemyValuesScript.GetAttackSpeed();
        rangedAttack = StartCoroutine(RangedAttack());
    }
    private IEnumerator RangedAttack()
    {
        enemyValuesScript.ZeroSpeed();
        animator.SetBool("Walk", false);

        animator.SetBool("Jump", true);
        yield return new WaitForSeconds(5f);
        animator.SetBool("Jump", false);
        while(true)
        {
            //bulletPooler.SpawnFromPool(transform);
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.4f);
            GameObject bulletInstance = Instantiate(bullet);

            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
            spawnPos.y += 1;
            bulletInstance.transform.position = spawnPos;
            bulletInstance.transform.eulerAngles = new Vector3(
            bulletInstance.transform.eulerAngles.x,
            transform.transform.eulerAngles.y,
            bulletInstance.transform.eulerAngles.z );

            animator.SetBool("Attack", false);
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
        animator.SetBool("Walk", true);
        StopCoroutine(rangedAttack);
    }
}
