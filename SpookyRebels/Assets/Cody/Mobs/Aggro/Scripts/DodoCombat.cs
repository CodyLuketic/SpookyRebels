using System.Collections;
using UnityEngine;

public class DodoCombat : MonoBehaviour
{
    private EnemyValues valuesScript = null;
    private WalkingSounds walkingSoundsScript = null;

    private Animator animator = null;

    private Coroutine rangedAttack = null;
    [SerializeField]
    private GameObject bullet = null;

    [SerializeField]
    private float jumpTime = 1f;

    private void Start()
    {
        valuesScript = gameObject.GetComponent<EnemyValues>();
        walkingSoundsScript = gameObject.GetComponent<WalkingSounds>();

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
        animator.speed = valuesScript.GetAttackSpeed();
        rangedAttack = StartCoroutine(RangedAttack());
        walkingSoundsScript.StartWalkingLoop();
    }
    private IEnumerator RangedAttack()
    {
        valuesScript.ZeroSpeed();
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", true);
        
        yield return new WaitForSeconds(jumpTime);
        
        Vector3 targetAngles = transform.GetChild(0).transform.eulerAngles + new Vector3(0, 180f, 0);
        transform.GetChild(0).transform.eulerAngles = targetAngles;
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
            yield return new WaitForSeconds(valuesScript.GetAttackSpeed());
        }
    }

    public void StopRangedAttack()
    {
        StopRangedAttackHelper();
    }
    private void StopRangedAttackHelper()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", true);

        Vector3 targetAngles = transform.GetChild(0).transform.eulerAngles + new Vector3(0, 180f, 0);
        transform.GetChild(0).transform.eulerAngles = targetAngles;

        valuesScript.ResetSpeed();
        StopCoroutine(rangedAttack);
        walkingSoundsScript.StartWalkingLoop();
    }
}
