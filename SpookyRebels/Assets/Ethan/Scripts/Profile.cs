using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Profile
{
    public string name;
    public int Money;
    public int monCount;

    //gems
    public int smallFloraGem;
    public int mediumFloraGem;
    public int largeFloraGem;

    public int smallCrystalGem;
    public int mediumCrystalGem;
    public int largeCrystalGem;

    public int smallCausticGem;
    public int mediumCausticGem;
    public int largeCausticGem;

    public int smallTechGem;
    public int mediumTechGem;
    public int largeTechGem;

    public int smallVoidGem;
    public int mediumVoidGem;
    public int largeVoidGem;

    public int smallSpectralGem;
    public int mediumSpectralGem;
    public int largeSpectralGem;

    // items
    public int itemEquipted = 0;
    public int basicTrap;


    public Magamon[] magamon = new Magamon[50];

    //as more are added you need to increase this number
    public Magamon[] databaseForMagamon = new Magamon[10];
    //skills
    public SkillTree skillTree;

    //equipted
    public int skillMonEquipted = 0;
    public int equipt1 = 0;
    public int equipt2 = -1;
}
