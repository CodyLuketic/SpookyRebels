using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private float waitTime = 0;

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
            gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DestroyThisTimer()
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
    }

    private void Travel()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
