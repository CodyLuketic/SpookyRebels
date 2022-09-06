using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magamon : MonoBehaviour
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
    public int conditionHealth;
    public int range;
    public int bulletCount;
    public int skillAvailable;

    //these change
    public int attack;
    public int attackSpeed;
    public int reloadSpeed; // this is when out of bullets
    public int health;
    public int speed;
    public int bulletSpeed;

    public int defensetimer;

    ////////////here for animations
}
