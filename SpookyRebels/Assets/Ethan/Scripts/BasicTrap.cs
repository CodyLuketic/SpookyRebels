using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrap : MonoBehaviour
{
    public Profile p;
    public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    public float BMaxTime = 10f;
    public int speed = 10;
    void Start()
    {
        p = MainProfile.Instance.mainP;
        
        player = GameObject.FindGameObjectWithTag("Player");

        if(p.monCount >= 50) Destroy(gameObject);
        //update ui to say you cannot catch anything rn

        PlayerMovements pMovScript = player.GetComponent<PlayerMovements>();
        Vector3 bullDir = pMovScript.gunPoint;

        rb.velocity = bullDir * speed;

        p.basicTrap--; 
        MainProfile.Instance.mainP = p;
        //update ui
        Destroy(gameObject, BMaxTime);
    }

    void update()
    {
        if (this.transform.position.y <= 0.5)
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(0,0,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit?");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Catchable"))
        {
            
            //see if it works 
            //change this line for better traps
            int ran = Random.Range(1, 101);
            CatchInfo catchin = other.GetComponent<CatchInfo>();
            if (catchin.catchRate > ran)
            {
                Debug.Log("catch");
                //successful
                //play animation
                if (catchin.species == "CystalCrab")
                {
                    //play crab caught anim
                    p.magamon[p.monCount] = p.databaseForMagamon[2];
                }
                else if (catchin.species == "Dodo")
                {
                    //play dodo caught anim
                    p.magamon[p.monCount] = p.databaseForMagamon[1];
                }
                //mods
                p.magamon[p.monCount].level += catchin.levelMod;
                p.magamon[p.monCount].skillAvailable += catchin.levelMod;
                p.magamon[p.monCount].attack += catchin.attackMod;
                p.magamon[p.monCount].attackSpeed += catchin.attackSpeedMod;
                p.magamon[p.monCount].health += catchin.healthMod;
                p.magamon[p.monCount].speed += catchin.speedMod;
                p.magamon[p.monCount].bulletSpeed += catchin.bulletSpeedMod;
                p.magamon[p.monCount].skillAvailable += catchin.skillAvailableMod;
                p.monCount++;
                MainProfile.Instance.mainP = p;
            }
            else
            {
                Debug.Log("Fail");
            //play non catch animation
            }

            //ping the ui
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
