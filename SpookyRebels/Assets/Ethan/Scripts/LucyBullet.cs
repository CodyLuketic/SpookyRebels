using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucyBullet : MonoBehaviour
{
    //audioclips
    //public AudioClip hit;

    public Rigidbody rb;
    [SerializeField]
    private GameObject player;

    //these change
    public float speed = 2f;
    public int damage = 10;
    public float BMaxTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //var pMovScript = player.GetComponent("PlayerMovements");
        //Vector3 bullDirection = (Vector3)pMovScript.getRotation();

        PlayerMovements pMovScript = player.GetComponent<PlayerMovements>();
        Vector3 bullDir = pMovScript.gunPoint;

        //Vector3 bullDirection = GameObject.Find("Player").GetComponent("PlayerMovements").getRotation();
        rb.velocity = bullDir * speed;
        Destroy(gameObject, BMaxTime);
    }

    /* Have to implement with Cody enemy code
    void OnTriggerEnter(Collider other)
    {
        magEnemy enemy = other.GetComponent<magEnemy>();
        if (enemy != null)
        {
            AudioSource.PlayClipAtPoint(hit, transform.position);
            enemy.Damage();
            enemy.CheckDeath();
            

        }
        Destroy(gameObject);
    }
    */
}
