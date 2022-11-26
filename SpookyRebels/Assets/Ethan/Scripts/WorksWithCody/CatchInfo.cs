using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatchInfo : MonoBehaviour
{
    //This will make it so they can have modifiers for catching

    public string species;
    public int catchRate;

    //these can be modded
    public int levelMod;
    public int attackMod;
    public float attackSpeedMod;
    public int healthMod;
    public int speedMod;
    public float bulletSpeedMod;
    public int skillAvailableMod;

    void Start()
    {
        //this is where you add in the modifiers
    }
    }
