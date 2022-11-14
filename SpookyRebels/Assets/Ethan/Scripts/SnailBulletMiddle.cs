using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailBulletMiddle : MonoBehaviour
{
    //audioclips
    //public AudioClip hit;

    public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    public GameObject GemTrapPrefab;
    public float gemRate = 0;
    //these change
    public float speed = 2f;
    public int damage = 10;
    public float BMaxTime = 2f;
    public int pierce = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //var pMovScript = player.GetComponent("PlayerMovements");
        //Vector3 bullDirection = (Vector3)pMovScript.getRotation();

        PlayerMovements pMovScript = player.GetComponent<PlayerMovements>();
        Vector3 bullDir = pMovScript.gunPoint;

        Shoot pMonScript = player.GetComponent<Shoot>();
        Magamon gun = pMonScript.heldAttacking;

        //setting upper vars
        speed = gun.bulletSpeed;
        BMaxTime = gun.range;

        // bulletRange
        if (gun.sTree.Skills[15].skillOwned)
        {
            BMaxTime += 3;
        }
        else if (gun.sTree.Skills[3].skillOwned)
        {
            BMaxTime += 1;
        }

        // trapRate
        if (gun.sTree.Skills[10].skillOwned)
        {
            gemRate = 4;
        }
        else if (gun.sTree.Skills[1].skillOwned)
        {
            gemRate = 2;
        }

        //bullet speed
        if (gun.sTree.Skills[9].skillOwned)
        {
            speed += 2;
        }

        //piercing bullets
        if (gun.sTree.Skills[14].skillOwned)
        {
            pierce = 4;
        } 
        else if (gun.sTree.Skills[8].skillOwned)
        {
            pierce = 2;
        }
        //spread range
        float ranX = Random.Range(-0.16f, 0.16f);
        float ranZ = Random.Range(-0.16f, 0.16f);
        if (gun.sTree.Skills[2].skillOwned)
        {
             ranX = Random.Range(-0.26f, 0.26f);
             ranZ = Random.Range(-0.26f, 0.26f);
        }

        bullDir = new Vector3((bullDir.x + ranX), 0, (bullDir.z + ranZ));
        //Vector3 bullDirection = GameObject.Find("Player").GetComponent("PlayerMovements").getRotation();
        rb.velocity = bullDir * speed;
        Destroy(gameObject, BMaxTime);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyValues eValScript = other.gameObject.GetComponent<EnemyValues>();

            eValScript.SetHealth(eValScript.GetHealth() - damage);

            int ran = Random.Range(0, 11);
            if(ran < gemRate) Instantiate(GemTrapPrefab, transform.position, transform.rotation);
            pierce--;
            if(pierce <= 0) Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

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
