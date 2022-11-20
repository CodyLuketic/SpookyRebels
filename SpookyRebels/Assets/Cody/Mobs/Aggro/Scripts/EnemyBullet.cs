using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float waitTime = 1f;

    private void Start()
    {
        StartCoroutine(DestroyThisTimer());
    }

    private void FixedUpdate()
    {
        Travel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyThisTimer()
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }

    private void Travel()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
