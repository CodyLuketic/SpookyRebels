using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public Profile p;
    [SerializeField] private TextMeshProUGUI monsterTxt;
    [SerializeField] private TextMeshProUGUI skillTxt;
    [SerializeField] private TextMeshProUGUI skilltextTxt;
    [SerializeField] private TextMeshProUGUI requirementsTxt;
    public int selectedSkill;

    void Start()
    {
        p = MainProfile.Instance.mainP;
        monsterTxt.text = p.magamon[p.skillMonEquipted].name;
        displayInfo();

        //put in sprites here
    }




    public void displayInfo()
    {
            skillTxt.text = p.magamon[p.skillMonEquipted].sTree.Skills[selectedSkill].skillName;
            skilltextTxt.text = p.magamon[p.skillMonEquipted].sTree.Skills[selectedSkill].skillDescription;
            requirementsTxt.text = p.magamon[p.skillMonEquipted].sTree.Skills[selectedSkill].skillCost;

    }


    //buttons
    public void select0()
    {
        selectedSkill = 0;
        displayInfo();
    }
    public void select1()
    {
        selectedSkill = 1;
        displayInfo();
    }
    public void select2()
    {
        selectedSkill = 2;
        displayInfo();
    }
    public void select3()
    {
        selectedSkill = 3;
        displayInfo();
    }
    public void select4()
    {
        selectedSkill = 4;
        displayInfo();
    }
    public void select5()
    {
        selectedSkill = 5;
        displayInfo();
    }
    public void select6()
    {
        selectedSkill = 6;
        displayInfo();
    }
    public void select7()
    {
        selectedSkill = 7;
        displayInfo();
    }
    public void select8()
    {
        selectedSkill = 8;
        displayInfo();
    }
    public void select9()
    {
        selectedSkill = 9;
        displayInfo();
    }
    public void select10()
    {
        selectedSkill = 10;
        displayInfo();
    }
    public void select11()
    {
        selectedSkill = 11;
        displayInfo();
    }
    public void select12()
    {
        selectedSkill = 12;
        displayInfo();
    }
    public void select13()
    {
        selectedSkill = 13;
        displayInfo();
    }
    public void select14()
    {
        selectedSkill = 14;
        displayInfo();
    }
    public void select15()
    {
        selectedSkill = 15;
        displayInfo();
    }
    public void select16()
    {
        selectedSkill = 16;
        displayInfo();
    }
    public void select17()
    {
        selectedSkill = 17;
        displayInfo();
    }
    public void select18()
    {
        selectedSkill = 18;
        displayInfo();
    }
    public void select19()
    {
        selectedSkill = 19;
        displayInfo();
    }
    public void select20()
    {
        selectedSkill = 20;
        displayInfo();
    }
    public void select21()
    {
        selectedSkill = 21;
        displayInfo();
    }
    public void select22()
    {
        selectedSkill = 22;
        displayInfo();
    }
    public void select23()
    {
        selectedSkill = 23;
        displayInfo();
    }
    public void select24()
    {
        selectedSkill = 24;
        displayInfo();
    }
    public void select25()
    {
        selectedSkill = 25;
        displayInfo();
    }
    public void select26()
    {
        selectedSkill = 26;
        displayInfo();
    }
    public void select27()
    {
        selectedSkill = 27;
        displayInfo();
    }
    public void select28()
    {
        selectedSkill = 28;
        displayInfo();
    }
    public void select29()
    {
        selectedSkill = 29;
        displayInfo();
    }
    public void select30()
    {
        selectedSkill = 30;
        displayInfo();
    }
    public void select31()
    {
        selectedSkill = 31;
        displayInfo();
    }
    public void select32()
    {
        selectedSkill = 32;
        displayInfo();
    }
    public void select33()
    {
        selectedSkill = 33;
        displayInfo();
    }
    public void select34()
    {
        selectedSkill = 34;
        displayInfo();
    }
    public void select35()
    {
        selectedSkill = 35;
        displayInfo();
    }
    public void select36()
    {
        selectedSkill = 36;
        displayInfo();
    }
    public void select37()
    {
        selectedSkill = 37;
        displayInfo();
    }

}
