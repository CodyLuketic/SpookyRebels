using System.Collections;
using UnityEngine;

public class CrabCombat : MonoBehaviour
{
    private EnemyValues valuesScript = null;
    private Animator animator = null;

    private Coroutine attackCoroutine = null;
    private bool attacking = false;
    private int attackChoice = 0;

    private void Start()
    {
        valuesScript = gameObject.GetComponent<EnemyValues>();

        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !attacking)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            valuesScript.ResetSpeed();
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

    private IEnumerator Attack()
    {
        float attackSpeed = valuesScript.GetAttackSpeed();
                    
        attacking = true;
        valuesScript.ZeroSpeed();
        animator.speed = attackSpeed;
        attackChoice = Random.Range(0, 2);

        while(true)
        {
            if(attackChoice == 0)
            {
                animator.SetBool("Attack1", true);
            } else {
                animator.SetBool("Attack2", true);
            }
            yield return new WaitForSeconds(1 / attackSpeed);
            // Player Damage Call Goes Here
            Debug.Log("Player Damaged by: " + gameObject);
        }
    }

    private void StopAttack()
    {
        attacking = false;

        StopCoroutine(attackCoroutine);
        if(attackChoice == 0)
        {
            animator.SetBool("Attack1", false);
        } else {
            animator.SetBool("Attack2", false);
        }
    }
}
