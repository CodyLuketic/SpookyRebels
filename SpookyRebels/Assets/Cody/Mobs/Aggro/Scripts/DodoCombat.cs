using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DodoCombat : MonoBehaviour
{
    private DodoValues dodoValuesScript = null;
    private DodoFollow dodoFollowScript = null;
    private Animator animator = null;

    private Coroutine rangedAttack = null;

    [SerializeField]
    private GameObject bullet = null;

    private void Start()
    {
        dodoValuesScript = gameObject.GetComponent<DodoValues>();
        dodoFollowScript = gameObject.GetComponent<DodoFollow>();

        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    public void StartRangedAttack()
    {
        StartRangedAttackHelper();
    }
    private void StartRangedAttackHelper()
    {
        animator.speed = dodoValuesScript.GetAttackSpeed();
        rangedAttack = StartCoroutine(RangedAttack());
    }
    private IEnumerator RangedAttack()
    {
        dodoValuesScript.ZeroSpeed();
        animator.SetBool("Walk", false);

        animator.SetBool("Jump", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Jump", false);
        
        while(true)
        {
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
            yield return new WaitForSeconds(dodoValuesScript.GetAttackSpeed());
        }
    }

    public void StopRangedAttack()
    {
        StopRangedAttackHelper();
    }
    private void StopRangedAttackHelper()
    {
        if(!dodoValuesScript.GetDying())
        {
            animator.SetBool("Walk", true);
        }
        dodoValuesScript.ResetSpeed();
        StopCoroutine(rangedAttack);
    }
}
