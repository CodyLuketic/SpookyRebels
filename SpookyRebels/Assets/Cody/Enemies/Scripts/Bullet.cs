using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private float time;

    // Update is called once per frame
    void Update()
    {
        time++;

        if(time >= 600)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
