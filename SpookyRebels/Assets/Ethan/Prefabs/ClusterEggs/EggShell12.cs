using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggShell12 : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public int damage = 10;
    public float BMaxTime = 2f;
    public int whichshell = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (whichshell < 8) damage = 10;
        else if (whichshell < 16) damage = 20;
        else damage = 30;


        if (whichshell == 0) rb.velocity = new Vector3(1f, 0f, 1f) * speed;
        else if (whichshell == 8) rb.velocity = new Vector3(1f, 0f, 1f) * speed;
        else if (whichshell == 16) rb.velocity = new Vector3(1f, 0f, 1f) * speed;


        else if (whichshell == 1) rb.velocity = new Vector3(-1f, 0f, -1f) * speed;
        else if (whichshell == 9) rb.velocity = new Vector3(-1f, 0f, -1f) * speed;
        else if (whichshell == 17) rb.velocity = new Vector3(-1f, 0f, -1f) * speed;

        else if (whichshell == 2) rb.velocity = new Vector3(-1f, 0f, 1f) * speed;
        else if (whichshell == 10) rb.velocity = new Vector3(-1f, 0f, 1f) * speed;
        else if (whichshell == 18) rb.velocity = new Vector3(-1f, 0f, 1f) * speed;

        else if (whichshell == 3) rb.velocity = new Vector3(1f, 0f, -1f) * speed;
        else if (whichshell == 11) rb.velocity = new Vector3(1f, 0f, -1f) * speed;
        else if (whichshell == 19) rb.velocity = new Vector3(1f, 0f, -1f) * speed;

        else if (whichshell == 4) rb.velocity = new Vector3(1f, 0f, 0f) * speed;
        else if (whichshell == 12) rb.velocity = new Vector3(1f, 0f, 0f) * speed;
        else if (whichshell == 20) rb.velocity = new Vector3(1f, 0f, 0f) * speed;

        else if (whichshell == 5) rb.velocity = new Vector3(-1f, 0f, 0f) * speed;
        else if (whichshell == 13) rb.velocity = new Vector3(-1f, 0f, 0f) * speed;
        else if (whichshell == 21) rb.velocity = new Vector3(-1f, 0f, 0f) * speed;

        else if (whichshell == 6) rb.velocity = new Vector3(0f, 0f, -1f) * speed;
        else if (whichshell == 14) rb.velocity = new Vector3(0f, 0f, -1f) * speed;
        else if (whichshell == 22) rb.velocity = new Vector3(0f, 0f, -1f) * speed;

        else if (whichshell == 7) rb.velocity = new Vector3(0f, 0f, 1f) * speed;
        else if (whichshell == 15) rb.velocity = new Vector3(0f, 0f, 1f) * speed;
        else if (whichshell == 23) rb.velocity = new Vector3(0f, 0f, 1f) * speed;

        /*
        Instantiate(eggShell18, (firePoint.position + new Vector3(1f, 0f, 1f)), firePoint.rotation);
        Instantiate(eggShell38, (firePoint.position + new Vector3(-1f, 0f, 1f)), firePoint.rotation);
        Instantiate(eggShell48, (firePoint.position + new Vector3(1f, 0f, -1f)), firePoint.rotation);
        Instantiate(eggShell28, (firePoint.position + new Vector3(-1f, 0f, -1f)), firePoint.rotation);
        Instantiate(eggShell58, (firePoint.position + new Vector3(1f, 0f, 0f)), firePoint.rotation);
        Instantiate(eggShell68, (firePoint.position + new Vector3(-1f, 0f, 0f)), firePoint.rotation);
        Instantiate(eggShell78, (firePoint.position + new Vector3(0f, 0f, -1f)), firePoint.rotation);
        Instantiate(eggShell88, (firePoint.position + new Vector3(0f, 0f, 1f)), firePoint.rotation);
        */
        Destroy(gameObject, BMaxTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit?");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemyhit");
            //EnemyValues eValScript = other.gameObject.GetComponent<EnemyValues>();

            //eValScript.SetHealth(eValScript.GetHealth() - damage);
            if (other.gameObject.name == "CrabContainer(Clone)")
            {
                CrabValues eValScript = other.gameObject.GetComponent<CrabValues>();
                eValScript.SetHealth(eValScript.GetHealth() - damage);
            }
            else if (other.gameObject.name == "DodoContainer(Clone)")
            {
                DodoValues eValScript = other.gameObject.GetComponent<DodoValues>();
                eValScript.SetHealth(eValScript.GetHealth() - damage);
            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
