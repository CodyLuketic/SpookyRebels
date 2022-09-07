using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyParts = null;

    private Animator animator = null;

    [SerializeField]
    private GameObject bullet = null;

    //private Rigidbody rb = null;

    [SerializeField]
    private float waitTimeMelee = 0;

    [SerializeField]
    private float waitTimeEnd = 0;

    [SerializeField]
    private float waitTimeRanged = 0;

    private bool canMelee = false;
    // Start is called before the first frame update
    private void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();

        AttackStart(enemyParts.melee);
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

        yield return new WaitForSeconds(waitTimeMelee);

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

            yield return new WaitForSeconds(waitTimeRanged);
        }
    }
}


