using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Shoot : MonoBehaviour
{
    // bullet prefabs
    public Transform firePoint;
    public Transform leftCrabPoint;
    public Transform rightCrabPoint;
    public GameObject lucyShotPrefab;
    public GameObject CrabMiddleShotPrefab;

    public int currentBullets;
    public bool hasBullets = true;

    public Magamon heldAttacking;
    public Magamon heldDefending;

    public bool canSwitch = true;
    public bool canDefend = true;
    public bool canAttack = true;
    public int tempbulletDamage = 0;
    public float attackSpeedMod = 0;
    //ui
    [SerializeField] private TextMeshProUGUI ammoTxt;

    //switching things
    public bool swapped = false;

    // profile stuff
    public Profile p;

    // Start is called before the first frame update
    void Start()
    {
        p = MainProfile.Instance.mainP;
        heldAttacking = p.magamon[p.equipt1];
        if(p.equipt2 != -1) heldDefending = p.magamon[p.equipt2];

        currentBullets = heldAttacking.bulletCount;
        //Start of start game abilities
        //extra ammo and attack speed
        if (heldAttacking.species == "CystalCrab")
        {
            if (heldAttacking.sTree.Skills[7].skillOwned) currentBullets += 15;
            if (heldAttacking.sTree.Skills[6].skillOwned) attackSpeedMod -= .25f;
        }
        else
        {
            attackSpeedMod = 0;
        }
        


        //////add the skills that are only at the begining
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("fire1");
            if (canAttack && hasBullets)
            {
                //Debug.Log("canAttack");
                Attack();
            }
        }
        if (Input.GetButton("Fire2"))
        {
            if (canDefend)
            {
                Defend();
            }
        }
        if (Input.GetButton("Fire3"))
        {
            if (canSwitch)
            {
                Switch();
            }
        }
    }


    ////shooting method
    void Attack()
    {
        canAttack = false;
        //getting base gun out of way before mods
        if (heldAttacking.species == "Lucyfur")
        {
            //Debug.Log("lucygun");
            Instantiate(lucyShotPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            //Debug.Log("notLucy");
            if (heldAttacking.species == "CystalCrab")
            {
                //Debug.Log("snail");
                //double shot
                if (heldAttacking.sTree.Skills[12].skillOwned)
                {
                    Instantiate(CrabMiddleShotPrefab, leftCrabPoint.position, leftCrabPoint.rotation);
                    Instantiate(CrabMiddleShotPrefab, rightCrabPoint.position, rightCrabPoint.rotation);
                    currentBullets--;
                }
                else 
                {
                   // Debug.Log("snailshoot");
                    Instantiate(CrabMiddleShotPrefab, firePoint.position, firePoint.rotation);
                }
            }

            currentBullets--;
            ammoTxt.text = currentBullets + "";
        }
        if (currentBullets <= 0)
        {
            hasBullets = false;
            StartCoroutine(eReloadCooldown());
        }
        else
        {
            StartCoroutine(eAttackCooldown());
        }
    }

    IEnumerator eAttackCooldown()
    {
        // will need to change this for swap speed if i make it
        yield return new WaitForSeconds(heldAttacking.attackSpeed + attackSpeedMod);
        canAttack = true;
    }

    IEnumerator eReloadCooldown()
    {
        // will need to change this for swap speed if i make it
        yield return new WaitForSeconds(heldAttacking.reloadSpeed);
        currentBullets = heldAttacking.bulletCount;
        if (heldAttacking.species == "CystalCrab")
        {
            if (heldAttacking.sTree.Skills[7].skillOwned) currentBullets += 15;   
        }
            hasBullets = true;
        canAttack = true;
        ammoTxt.text = currentBullets + "";
    }



    ////defending method
    void Defend() 
    { 
    
    }






    // switch monsters
    void Switch()
    {
        canSwitch = false;
        if (p.equipt2 == -1)
        {
            //play cannot change noise
        }
        else
        {
            if (swapped)
            {
                heldAttacking = p.magamon[p.equipt1];
                heldDefending = p.magamon[p.equipt2];
                swapped = false;
            }
            else
            {
                heldAttacking = p.magamon[p.equipt2];
                heldDefending = p.magamon[p.equipt1];
                swapped = true;
            }
            // stop cooldowns
            currentBullets = heldAttacking.bulletCount;
            //start abilities
            if (heldAttacking.species == "CystalCrab")
            {
                if (heldAttacking.sTree.Skills[7].skillOwned) currentBullets += 15;
                if (heldAttacking.sTree.Skills[6].skillOwned) attackSpeedMod -= .25f;
            }
            else
            {
                attackSpeedMod = 0;
            }

            if (canAttack == false) StopCoroutine(eAttackCooldown());
            if (hasBullets == false) StopCoroutine(eReloadCooldown());
            hasBullets = true;
            canAttack = true;

            //swap sound
            //update ui
            if (heldAttacking.species == "Lucyfur")
            {
                ammoTxt.text = "infinite";
            }
            else
            {
                ammoTxt.text = currentBullets + "";
            }
            StartCoroutine(eSwitchCooldown());
        }
    }

    IEnumerator eSwitchCooldown()
    {
        // will need to change this for swap speed if i make it
        yield return new WaitForSeconds(5f);
        canSwitch = true;
    }
}
