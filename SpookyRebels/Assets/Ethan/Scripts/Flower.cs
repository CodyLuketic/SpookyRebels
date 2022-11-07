using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public int heal = 10;
    public float BMaxTime = 2f;
    private GameObject player;
    Magamon gun = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Shoot pMonScript = player.GetComponent<Shoot>();
        gun = pMonScript.heldAttacking;

        if (gun.sTree.Skills[7].skillOwned) BMaxTime += 2;
        if (gun.sTree.Skills[13].skillOwned) heal += 10;


        Destroy(gameObject, BMaxTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("heal?");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("playerheal");
            //heal player by heal amount
            Destroy(gameObject);
        }


        
    }
}
