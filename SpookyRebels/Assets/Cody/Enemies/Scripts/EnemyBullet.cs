using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private float waitTime = 0;

    void Start()
    {
        StartCoroutine(DestroyThisTimer());
    }

    private void Update()
    {
        Travel();
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
    }

    private IEnumerator DestroyThisTimer()
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
    }

    private void Travel()
    {
        transform.Translate(Vector3.forward * speed);
    }
}
