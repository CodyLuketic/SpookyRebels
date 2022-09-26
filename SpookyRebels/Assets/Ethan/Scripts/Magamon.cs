using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Magamon 
{
    // these might be added
    public string name;
    public Sprite Icon;
    public Sprite gunIcon;
    
    //these are perminent
    public string species;
    public string element;
    public int maxLevel;

    // these kinda change
    public int level;
    public int conditionHealth;
    public float range;
    public int bulletCount;
    public int skillAvailable;
    public SkillTree sTree;
    //these change
    public int attack;
    public float attackSpeed;
    public float reloadSpeed; // this is when out of bullets
    public int health;
    public int speed;
    public float bulletSpeed;

    public float defensetimer;

    ////////////here for animations
}
