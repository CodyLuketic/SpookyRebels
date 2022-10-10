using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Destroy(gameObject);
    }

    private IEnumerator DestroyThisTimer()
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }

    private void Travel()
    {
        transform.Translate(Vector3.forward * speed);
    }
}
