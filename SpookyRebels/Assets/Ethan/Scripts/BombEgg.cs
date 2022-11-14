using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEgg : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    public int damage = 10;
    public float BMaxTime = 2f;
    public int shells = 4;
    public GameObject eggShell12;
    public GameObject eggShell22;
    public GameObject eggShell32;
    public GameObject eggShell42;
    public GameObject eggShell52;
    public GameObject eggShell62;
    public GameObject eggShell72;
    public GameObject eggShell82;
    public Transform firePoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Shoot pMonScript = player.GetComponent<Shoot>();
        if (pMonScript.heldAttacking.species == "Dodo")
        {
            if (pMonScript.heldAttacking.sTree.Skills[21].skillOwned) BMaxTime += 2;
            if (pMonScript.heldAttacking.sTree.Skills[25].skillOwned) BMaxTime += 4;
            if (pMonScript.heldAttacking.sTree.Skills[22].skillOwned) damage += 10;
            if (pMonScript.heldAttacking.sTree.Skills[26].skillOwned) shells += 4;
        }
        else
        {
            if (pMonScript.heldDefending.sTree.Skills[21].skillOwned) BMaxTime += 2;
            if (pMonScript.heldDefending.sTree.Skills[25].skillOwned) BMaxTime += 4;
            if (pMonScript.heldDefending.sTree.Skills[22].skillOwned) damage += 10;
            if (pMonScript.heldDefending.sTree.Skills[26].skillOwned) shells += 4;
        }
        Destroy(gameObject, BMaxTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name == "CrabAggro(Clone)")
            {
                CrabValues eValScript = other.gameObject.GetComponent<CrabValues>();
                eValScript.SetHealth(eValScript.GetHealth() - damage);
            }
            else if (other.gameObject.name == "DodoAggro(Clone)")
            {
                DodoValues eValScript = other.gameObject.GetComponent<DodoValues>();
                eValScript.SetHealth(eValScript.GetHealth() - damage);
            }

            //eValScript.SetHealth(eValScript.GetHealth() - damage);
            if (shells == 4)
            {
                Instantiate(eggShell12, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
                Instantiate(eggShell32, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
                Instantiate(eggShell42, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
                Instantiate(eggShell22, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
            }
            else if (shells == 8)
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
            Destroy(gameObject);
        }
    }
}
