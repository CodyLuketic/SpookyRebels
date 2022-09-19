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
    private float waitTimeAttacking = 0;
    [SerializeField]
    private float waitTimeStill = 0;

    private bool canMelee = false;
    // Start is called before the first frame update
    private void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
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
            Debug.Log("Collided");
            StartCoroutine(MeleeAttack());
            canMelee = false;
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log(gameObject.name + "hit");
            enemyValues.SetHealth(enemyValues.GetHealth() - 1);

            Destroy(other.gameObject);
        }
    }

    private IEnumerator MeleeAttack()
    {
        float tempSpeed = enemyValues.GetSpeed();

        animator.SetBool("attacking", true);
        enemyValues.SetSpeed(0);

        // Player Damage Call Goes Here

        yield return new WaitForSeconds(waitTimeAttacking);
        animator.SetBool("attacking", false);
        
        yield return new WaitForSeconds(waitTimeStill);
        enemyValues.SetSpeed(tempSpeed);
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
