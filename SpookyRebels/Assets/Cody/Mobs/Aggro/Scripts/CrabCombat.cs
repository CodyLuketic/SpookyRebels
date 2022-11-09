using System.Collections;
using UnityEngine;

public class CrabCombat : MonoBehaviour
{
    private CrabValues crabValuesScript = null;
    private CrabFollow crabFollowScript = null;
    private Animator animator = null;

    private Coroutine attackCoroutine = null;
    private int attackChoice = 0;

    private void Start()
    {
        crabValuesScript = gameObject.GetComponent<CrabValues>();
        crabFollowScript = gameObject.GetComponent<CrabFollow>();

        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.activeSelf)
        {
            crabValuesScript.ZeroSpeed();
            StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.activeSelf)
        {
            crabValuesScript.ResetSpeed();
            StopAttack();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    public void StartAttack()
    {
        StartAttackHelper();
    }
    private void StartAttackHelper()
    {
        animator.speed = crabValuesScript.GetAttackSpeed() / 3.4f;
        attackCoroutine = StartCoroutine(Attack());
    }
    private IEnumerator Attack()
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

            yield return new WaitForSeconds(crabValuesScript.GetAttackSpeed());
        }
    }
    public void StopAttack()
    {
        StopAttackHelper();
    }
    private void StopAttackHelper()
    {
        StopCoroutine(attackCoroutine);
        if(attackChoice == 0)
        {
            animator.SetBool("Attack1", false);
        } else {
            animator.SetBool("Attack2", false);
        }
    }
}
