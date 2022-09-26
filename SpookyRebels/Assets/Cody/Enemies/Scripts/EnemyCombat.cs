using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
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
    private bool canMelee = false;

    [Header("Ranged Only")]
    [SerializeField]
    private GameObject enemyBullet = null;

    // Start is called before the first frame update
    private void Start()
    {
        enemyValuesScript = gameObject.GetComponent<EnemyValues>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        AttackStart(enemyValuesScript.GetMelee());
        StartCoroutine(PhysicsConst());
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

    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && canMelee)
        {
            StartCoroutine(MeleeAttack(other));
            canMelee = false;
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyRb.velocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;
        }

        if(other.gameObject.CompareTag("PassiveMob"))
        {
            enemyRb.velocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            enemyValuesScript.SetHealth(enemyValuesScript.GetHealth() - 1);

            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }

    private IEnumerator MeleeAttack(Collision other)
    {
        float tempSpeed = enemyValuesScript.GetSpeed();
        animator.SetBool("attacking", true);
        enemyValuesScript.SetSpeed(0);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeAttacking);
        animator.SetBool("attacking", false);

        enemyRb.AddForce(other.GetContact(0).normal * enemyValuesScript.GetBounceBack(), ForceMode.Impulse);
        
        yield return new WaitForSeconds(waitTimeStill);
        enemyValuesScript.SetSpeed(tempSpeed);
        ResetPhysics();
        canMelee = true;
    }

    private IEnumerator RangedAttack()
    {
        while (true)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
            Instantiate(enemyBullet, position, transform.rotation);

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
