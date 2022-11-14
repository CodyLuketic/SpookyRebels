using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBullet : MonoBehaviour
{
    //audioclips
    //public AudioClip hit;

    public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    public Transform firePoint;
    //these change
    public float speed = 2f;
    public int damage = 10;
    public float BMaxTime = 2f;

    public int cluster = 0;
    public int flowerSpawn = 0;
    public GameObject flowerPrefab;
    int flowerRate = 0;

    public GameObject eggShell12;
    public GameObject eggShell22;
    public GameObject eggShell32;
    public GameObject eggShell42;
    public GameObject eggShell52;
    public GameObject eggShell62;
    public GameObject eggShell72;
    public GameObject eggShell82;
    //.4 is the middle strength
    public GameObject eggShell14;
    public GameObject eggShell24;
    public GameObject eggShell34;
    public GameObject eggShell44;
    public GameObject eggShell54;
    public GameObject eggShell64;
    public GameObject eggShell74;
    public GameObject eggShell84;
    // .8 is strongest
    public GameObject eggShell18;
    public GameObject eggShell28;
    public GameObject eggShell38;
    public GameObject eggShell48;
    public GameObject eggShell58;
    public GameObject eggShell68;
    public GameObject eggShell78;
    public GameObject eggShell88;
    Magamon gun = null;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //var pMovScript = player.GetComponent("PlayerMovements");
        //Vector3 bullDirection = (Vector3)pMovScript.getRotation();

        PlayerMovements pMovScript = player.GetComponent<PlayerMovements>();
        Vector3 bullDir = pMovScript.gunPoint;

        Shoot pMonScript = player.GetComponent<Shoot>();
         gun = pMonScript.heldAttacking;

        //setting upper vars
        speed = gun.bulletSpeed;
        BMaxTime = gun.range;

        if (gun.sTree.Skills[10].skillOwned)
        {
            cluster = 8;
        }
        else if (gun.sTree.Skills[4].skillOwned)
        {
            cluster = 4;
        }
        else if (gun.sTree.Skills[1].skillOwned)
        {
            cluster = 2;
        }

        if (gun.sTree.Skills[2].skillOwned) flowerRate += 5;
        if (gun.sTree.Skills[6].skillOwned) flowerRate += 5;
        if (gun.sTree.Skills[12].skillOwned) flowerRate += 10;
        // start skills

        rb.velocity = bullDir * speed;
        Destroy(gameObject, BMaxTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit?");
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("enemyhit");
            if (other.gameObject.name == "CrabAggro(Clone)")
            {
                CrabValues eValScript = other.gameObject.GetComponent<CrabValues>();
                eValScript.SetHealth(eValScript.GetHealth() - damage);
            } else if (other.gameObject.name == "DodoAggro(Clone)")
            {
                DodoValues eValScript = other.gameObject.GetComponent<DodoValues>();
                eValScript.SetHealth(eValScript.GetHealth() - damage);
            }

            int ran = Random.Range(0, 101);
            if (ran < flowerRate) Instantiate(flowerPrefab, transform.position, transform.rotation);


            if (gun.sTree.Skills[11].skillOwned)
            {
                if (cluster == 2)
                {
                    Debug.Log("cluster2");
                    Instantiate(eggShell18, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell28, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                }
                else if (cluster == 4)
                {
                    Instantiate(eggShell18, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell38, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell48, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell28, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                }
                else if (cluster == 8)
                {
                    Instantiate(eggShell18, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell38, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell48, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell28, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell58, (firePoint.position + new Vector3(1f, 0f, 0f)), firePoint.rotation);
                    Instantiate(eggShell68, (firePoint.position + new Vector3(-1f, 0f, 0f)), firePoint.rotation);
                    Instantiate(eggShell78, (firePoint.position + new Vector3(0f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell88, (firePoint.position + new Vector3(0f, 0f, 1f)), firePoint.rotation);
                }
            }
            else if (gun.sTree.Skills[5].skillOwned)
            {
                if (cluster == 2)
                {
                    Instantiate(eggShell14, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell24, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                }
                else if (cluster == 4)
                {
                    Instantiate(eggShell14, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell34, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell44, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell24, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                }
                else if (cluster == 8)
                {
                    Instantiate(eggShell14, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell34, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell44, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell24, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell54, (firePoint.position + new Vector3(1f, 0f, 0f)), firePoint.rotation);
                    Instantiate(eggShell64, (firePoint.position + new Vector3(-1f, 0f, 0f)), firePoint.rotation);
                    Instantiate(eggShell74, (firePoint.position + new Vector3(0f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell84, (firePoint.position + new Vector3(0f, 0f, 1f)), firePoint.rotation);
                }
            }
            else
            {
                if (cluster == 2)
                {
                    Instantiate(eggShell12, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell22, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                }
                else if (cluster == 4)
                {
                    Instantiate(eggShell12, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell32, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell42, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell22, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                }
                else if (cluster == 8)
                {
                    Instantiate(eggShell12, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell32, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                    Instantiate(eggShell42, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell22, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell52, (firePoint.position + new Vector3(1f, 0f, 0f)), firePoint.rotation);
                    Instantiate(eggShell62, (firePoint.position + new Vector3(-1f, 0f, 0f)), firePoint.rotation);
                    Instantiate(eggShell72, (firePoint.position + new Vector3(0f, 0f, -1f)), firePoint.rotation);
                    Instantiate(eggShell82, (firePoint.position + new Vector3(0f, 0f, 1f)), firePoint.rotation);
                }
            }





            Destroy(gameObject);

        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
