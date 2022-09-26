using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMobCombat : MonoBehaviour
{
    private PassiveMobValues passiveMobValues = null;

    private Rigidbody passiveMobRb = null;

    private UnityEngine.AI.NavMeshAgent passiveMobNav = null;

    private Animator animator = null;

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
        passiveMobValues = gameObject.GetComponent<PassiveMobValues>();
        passiveMobRb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

        AttackStart(passiveMobValues.GetMelee());
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
            passiveMobValues.SetHealth(passiveMobValues.GetHealth() - 1);

            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("PassiveMobBullet"))
        {
            passiveMobValues.SetHealth(passiveMobValues.GetHealth() - 1);

            Destroy(other.gameObject);
        }
    }

    private IEnumerator MeleeAttack(Collision other)
    {
        float tempSpeed = passiveMobValues.GetSpeed();
        animator.SetBool("attacking", true);
        passiveMobValues.SetSpeed(0);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeAttacking);
        animator.SetBool("attacking", false);

        float bounce = 6f; // Amount of force to apply
        passiveMobRb.AddForce(other.GetContact(0).normal * bounce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(waitTimeStill);
        passiveMobValues.SetSpeed(tempSpeed);
        ResetPhysics();
        canMelee = true;
    }

    private IEnumerator RangedAttack()
    {
        while (true)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
            Instantiate(passiveMobBullet, position, transform.rotation);

            yield return new WaitForSeconds(passiveMobValues.GetAttackSpeed());
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
