using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeCombat : MonoBehaviour
{
    private EnemyValues enemyValuesScript = null;

    private Rigidbody enemyRb = null;

    private NavMeshAgent enemyNav = null;

    private Animator animator = null;

    [Header("Melee Only")]
    [SerializeField]
    private float waitTimeAttacking = 0;
    [SerializeField]
    private float waitTimeStill = 0;
    private bool canMelee = true;

    private void Start()
    {
        enemyValuesScript = gameObject.GetComponent<EnemyValues>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        StartCoroutine(PhysicsConst());
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && canMelee && gameObject.activeSelf)
        {
            StartCoroutine(MeleeAttack(other));
            canMelee = false;
        }

        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("PassiveMob"))
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

    private IEnumerator MeleeAttack(Collision other)
    {
        ResetPhysics();
        float tempSpeed = enemyValuesScript.GetSpeed();
        animator.SetBool("attacking", true);
        enemyValuesScript.SetSpeed(0);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeAttacking);
        animator.SetBool("attacking", false);

        enemyRb.AddForce(other.GetContact(0).normal * enemyValuesScript.GetBounceBack(), ForceMode.Impulse);
        enemyRb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(waitTimeStill);
        enemyValuesScript.SetSpeed(tempSpeed);
        ResetPhysics();
        canMelee = true;
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
