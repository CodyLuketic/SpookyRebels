using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValues : MonoBehaviour
{
    [SerializeField]
    private MonsterScriptableObject monsterParts = null;

    private Mesh monsterMesh = null;

    private Material monsterMaterial = null;

    private int health = 0;

    private float damage = 0;
    // Start is called before the first frame update
    private void Start()
    {
        monsterMesh = monsterParts.monsterMesh;
        monsterMaterial = monsterParts.monsterMaterial;
        health = monsterParts.health;
        damage = monsterParts.damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
