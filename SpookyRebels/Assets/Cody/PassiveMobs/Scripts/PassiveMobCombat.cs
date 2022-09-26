using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobCombat : MonoBehaviour
{
    private PassiveMobValues passiveMobValuesScript = null;
    private PassiveMobMovement passiveMobMovementScript = null;

    private Rigidbody passiveMobRb = null;

    private NavMeshAgent passiveMobNav = null;

    private Animator animator = null;

    private bool agroed = false;

    [Header("Melee Only")]
    [SerializeField]
    private float waitTimeAttacking = 0;
    [SerializeField]
    private float waitTimeStill = 0;
    private bool canMelee = false;

    [Header("Ranged Only")]
    [SerializeField]
    private GameObject passiveMobBullet = null;

    // Start is called before the first frame update
    private void Start()
    {
        passiveMobValuesScript = gameObject.GetComponent<PassiveMobValues>();
        passiveMobRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        AttackStart(passiveMobValuesScript.GetMelee());
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
            passiveMobRb.velocity = Vector3.zero;
            passiveMobRb.angularVelocity = Vector3.zero;
        }

        if(other.gameObject.CompareTag("PassiveMob"))
        {
            passiveMobRb.velocity = Vector3.zero;
            passiveMobRb.angularVelocity = Vector3.zero;
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            if(!agroed)
            {
                agroed = true;
                AttackStart(passiveMobValuesScript.GetMelee());
            }

            passiveMobValuesScript.SetHealth(passiveMobValuesScript.GetHealth() - 1);

            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("PassiveMobBullet"))
        {
            passiveMobValuesScript.SetHealth(passiveMobValuesScript.GetHealth() - 1);

            Destroy(other.gameObject);
        }
    }

    private IEnumerator MeleeAttack(Collision other)
    {
        float tempSpeed = passiveMobValuesScript.GetSpeed();
        animator.SetBool("attacking", true);
        passiveMobValuesScript.SetSpeed(0);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeAttacking);
        animator.SetBool("attacking", false);

        passiveMobRb.AddForce(other.GetContact(0).normal * passiveMobValuesScript.GetBounceBack(), ForceMode.Impulse);
        
        yield return new WaitForSeconds(waitTimeStill);
        passiveMobValuesScript.SetSpeed(tempSpeed);
        ResetPhysics();
        canMelee = true;
    }

    private IEnumerator RangedAttack()
    {
        while (true)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
            Instantiate(passiveMobBullet, position, transform.rotation);

            yield return new WaitForSeconds(passiveMobValuesScript.GetAttackSpeed());
        }
    }
    
    private IEnumerator PhysicsConst()
    {
        yield return new WaitForSeconds(1);
        ResetPhysics();
    }

    private void ResetPhysics()
    {
        passiveMobRb.velocity = Vector3.zero;
        passiveMobRb.angularVelocity = Vector3.zero;
    }

    public void SetNavAgent()
    {
        SetNavAgentHelper();
    }

    private void SetNavAgentHelper()
    {
        passiveMobNav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
}
