using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTrap : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    public int damage = 10;
    public float BMaxTime = 2f;
    public int pierce = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Shoot pMonScript = player.GetComponent<Shoot>();
        if (pMonScript.heldAttacking.species == "CystalCrab")
        {
            if (pMonScript.heldAttacking.sTree.Skills[11].skillOwned) BMaxTime += 3;
            if (pMonScript.heldAttacking.sTree.Skills[4].skillOwned) damage += 10;
            if (pMonScript.heldAttacking.sTree.Skills[5].skillOwned) damage += 10;
        }
        else
        {
            if (pMonScript.heldDefending.sTree.Skills[11].skillOwned) BMaxTime += 3;
            if (pMonScript.heldDefending.sTree.Skills[4].skillOwned) damage += 10;
            if (pMonScript.heldDefending.sTree.Skills[5].skillOwned) damage += 10;
        }
        Destroy(gameObject, BMaxTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyValues eValScript = other.gameObject.GetComponent<EnemyValues>();

            eValScript.SetHealth(eValScript.GetHealth() - damage);

            
            pierce--;
            if (pierce <= 0) Destroy(gameObject);
        }


    }
}
