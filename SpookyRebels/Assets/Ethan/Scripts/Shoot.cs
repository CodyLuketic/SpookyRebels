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
    public Transform eggBombPoint;
    public Transform rightCrabPoint;
    public GameObject lucyShotPrefab;
    public GameObject CrabMiddleShotPrefab;
    public Transform GemWavePoint;
    public GameObject GemWavePrefab;
    public GameObject eggShotPrefab;
    public GameObject bigEggShotPrefab;
    public GameObject EggBombPrefab;
    public float recoil = 1;
    public int currentBullets;
    public bool hasBullets = true;
    public Transform eggPoint;
    public Magamon heldAttacking;
    public Magamon heldDefending;

    public bool canSwitch = true;
    public bool canDefend = true;
    public bool canAttack = true;
    public int tempbulletDamage = 0;
    public float attackSpeedMod = 0;
    //ui
    [SerializeField] private TextMeshProUGUI ammoTxt;

    // player conections
    //public Rigidbody rb;
    [SerializeField]
    private GameObject player;
    //switching things
    public bool swapped = false;

    //skill stuff
    public bool ammoLoss = true;
    // profile stuff
    public Profile p;
    public PlayerMovements pMovScript = null;
    Vector3 bullDir = Vector3.zero;

    //dash stuff
    public bool canDash = true;
    public bool canDoubleDash = false;
    public bool doubleDashable = false;
    public bool dashTemp = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pMovScript = player.GetComponent<PlayerMovements>();


        bullDir = pMovScript.gunPoint;

        p = MainProfile.Instance.mainP;
        heldAttacking = p.magamon[p.equipt1];
        if(p.equipt2 != -1) heldDefending = p.magamon[p.equipt2];

        currentBullets = heldAttacking.bulletCount;
        //Start of start game abilities
        //extra ammo and attack speed
        startSkills();
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
            if (canDefend || canDoubleDash)
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
                    if(ammoLoss) currentBullets--;
                }
                else 
                {
                   // Debug.Log("snailshoot");
                    Instantiate(CrabMiddleShotPrefab, firePoint.position, firePoint.rotation);
                }
            }
            else if (heldAttacking.species == "Dodo")
            {
                //Debug.Log("dodo");
                //big egg
                if (heldAttacking.sTree.Skills[3].skillOwned)
                {
                    Instantiate(bigEggShotPrefab, eggPoint.position, eggPoint.rotation);
                }
                else
                {
                    Instantiate(eggShotPrefab, eggPoint.position, eggPoint.rotation);
                }
                if(recoil != 0) pMovScript.currentDashTimeRe = 0;
                //rb.velocity = bullDir * recoil * -1;
            }

            if (ammoLoss) currentBullets--;
            if (currentBullets < 0) currentBullets = 0;
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
        //Debug.Log("defense");
        if (heldDefending.species == "Lucyfur")
        {
            //play sound clip
        }
        else if (heldDefending.species == "CystalCrab")
        {
            if (heldDefending.sTree.Skills[18].skillOwned) Instantiate(GemWavePrefab, GemWavePoint.position, GemWavePoint.rotation);
            StartCoroutine(eShieldMode());
        }
        else if (heldDefending.species == "Dodo")
        {
            Debug.Log("dash");
            //Egg bomb
            if (heldDefending.sTree.Skills[18].skillOwned)
            {
                int ran = Random.Range(0, 3);
                if (ran < 2) Instantiate(EggBombPrefab, eggBombPoint.position, eggBombPoint.rotation);
            }
            //do the dash
            pMovScript.currentDashTime = 0;
            if (canDash) canDash = false;
            else canDoubleDash = false;
            StartCoroutine(eDashMode());
        }

        canDefend = false;
        StartCoroutine(eDefendCooldown());
    }

    IEnumerator eShieldMode()
    {
        pMovScript.canMove = false;
        canSwitch = false;
        //make no damage
        if (heldDefending.sTree.Skills[19].skillOwned)
        {
            heldAttacking.attackSpeed /= 2;
        }
        if (heldDefending.sTree.Skills[23].skillOwned)
        {
            ammoLoss = false;
        }

        if (heldDefending.sTree.Skills[17].skillOwned)
        {
            yield return new WaitForSeconds(5);
        } 
        else
        {
            yield return new WaitForSeconds(3);
        }

        if (heldDefending.sTree.Skills[23].skillOwned)
        {
            ammoLoss = true;
        }
        if (heldDefending.sTree.Skills[19].skillOwned)
        {
            heldAttacking.attackSpeed *= 2;
        }
        //can take damage
        canSwitch = true;
        pMovScript.canMove = true;
    }

    IEnumerator eDashMode()
    {
        dashTemp = canDoubleDash;
        canDoubleDash = false;
        if (heldDefending.sTree.Skills[23].skillOwned)
        { 
            //cantake damage = false
        }
        yield return new WaitForSeconds(1);
        //cantake damage = true
        canDoubleDash = dashTemp;
    }

    IEnumerator eDefendCooldown()
    {
        
        yield return new WaitForSeconds(heldDefending.defensetimer);
        canDefend = true;
        canDash = true;
        if(doubleDashable) canDoubleDash = true;
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
            startSkills();

            // stop cooldowns
            if (canAttack == false) StopCoroutine(eAttackCooldown());
            if (canDefend == false) StopCoroutine(eDefendCooldown());
            if (hasBullets == false) StopCoroutine(eReloadCooldown());
            hasBullets = true;
            canAttack = true;
            canDefend = true;
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

    
    void startSkills()
    { 
    
        if (heldAttacking.species == "CystalCrab")
        {
            if (heldAttacking.sTree.Skills[7].skillOwned) currentBullets += 15;
            if (heldAttacking.sTree.Skills[6].skillOwned) attackSpeedMod -= .25f;
        }
        else
        {
            attackSpeedMod = 0;
        }

        if (heldDefending.species == "CystalCrab")
        {
            if (heldDefending.sTree.Skills[24].skillOwned) heldDefending.defensetimer = 10;
        }



        if (heldAttacking.species == "Dodo")
        {
            if (heldAttacking.sTree.Skills[8].skillOwned) recoil += 0.5f;
            if (heldAttacking.sTree.Skills[14].skillOwned) recoil += 0.5f;
            if (heldAttacking.sTree.Skills[9].skillOwned) recoil -= 0.5f;
            if (heldAttacking.sTree.Skills[15].skillOwned) recoil -= 0.5f;
            pMovScript.maxDashTimeRe = recoil;
        }
        if(heldDefending.species == "Dodo")
        {
            if (heldDefending.sTree.Skills[24].skillOwned) doubleDashable = canDoubleDash = true;
            else doubleDashable = canDoubleDash = false;
            if (heldDefending.sTree.Skills[19].skillOwned) pMovScript.maxDashTime = 3;
            else pMovScript.maxDashTime = 1.5f;
            if (heldDefending.sTree.Skills[20].skillOwned) heldDefending.defensetimer = 5;
            else if (heldDefending.sTree.Skills[17].skillOwned) heldDefending.defensetimer = 7;
        }

    }


}
 
