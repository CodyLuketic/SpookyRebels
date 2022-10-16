using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemWave : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    public int damage = 10;
    public float BMaxTime = 1f;
    public int maxRange = 5;
    public int growFactor = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Shoot pMonScript = player.GetComponent<Shoot>();
        if (pMonScript.heldDefending.species == "CystalCrab")
        {
            if (pMonScript.heldDefending.sTree.Skills[21].skillOwned) maxRange += 5;
            if (pMonScript.heldDefending.sTree.Skills[25].skillOwned) maxRange += 5;
            if (pMonScript.heldDefending.sTree.Skills[22].skillOwned) damage += 10;
            if (pMonScript.heldDefending.sTree.Skills[26].skillOwned) damage += 10;
        }
        else
        {
            if (pMonScript.heldAttacking.sTree.Skills[21].skillOwned) maxRange += 5;
            if (pMonScript.heldAttacking.sTree.Skills[25].skillOwned) maxRange += 5;
            if (pMonScript.heldAttacking.sTree.Skills[22].skillOwned) damage += 10;
            if (pMonScript.heldAttacking.sTree.Skills[26].skillOwned) damage += 10;
        }
        StartCoroutine(eScale());
        //Destroy(gameObject, BMaxTime);
    }

    IEnumerator eScale()
    {
        float timer = 0;
            while (maxRange > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyValues eValScript = other.gameObject.GetComponent<EnemyValues>();

            eValScript.SetHealth(eValScript.GetHealth() - damage);

        }


    }
}
