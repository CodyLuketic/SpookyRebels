using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // bullet prefabs
    public Transform firePoint;
    public GameObject lucyShotPrefab;


    public int currentBullets;
    public bool hasBullets = true;

    public Magamon heldAttacking;
    public Magamon heldDefending;

    public bool canSwitch = true;
    public bool canDefend = true;
    public bool canAttack = true;
    public int tempbulletDamage = 0;


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


        //////add the skills that are only at the begining
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (canAttack && hasBullets)
            {
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
            Instantiate(lucyShotPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {


            currentBullets--;
        }
        if (currentBullets == 0)
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
        yield return new WaitForSeconds(heldAttacking.attackSpeed);
        canAttack = true;
    }

    IEnumerator eReloadCooldown()
    {
        // will need to change this for swap speed if i make it
        yield return new WaitForSeconds(heldAttacking.reloadSpeed);
        currentBullets = heldAttacking.bulletCount;
        hasBullets= true;
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
            if(canAttack == false) StopCoroutine(eAttackCooldown());
            if (hasBullets == false) StopCoroutine(eReloadCooldown());
            hasBullets = true;
            canAttack = true;

            //swap sound
            //update ui
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
