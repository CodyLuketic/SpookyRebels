using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyCombat : MonoBehaviour
{
    private EnemyValues enemyValues = null;

    private Animator animator = null;

    [SerializeField]
    private GameObject bullet = null;

    //private Rigidbody rb = null;

    [SerializeField]
    private float waitTime = 0;

    [SerializeField]
    private float waitTimeEnd = 0;

    private bool canMelee = false;
    // Start is called before the first frame update
    private void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        enemyValues = gameObject.GetComponent<EnemyValues>();

        AttackStart(enemyValues.GetMelee());
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
            StartCoroutine(MeleeAttack());
            canMelee = false;
        }
    }

    private IEnumerator MeleeAttack()
    {
        //animator.SetTrigger("Attack");

        yield return new WaitForSeconds(waitTime);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeEnd);
        canMelee = true;
    }

    private IEnumerator RangedAttack()
    {
        while (true)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
            Instantiate(bullet, position, transform.rotation);

            yield return new WaitForSeconds(enemyValues.GetAttackSpeed());
        }
    }
}


