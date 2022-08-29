using UnityEngine;

[CreateAssetMenu(fileName = "MonsterScriptableObject", menuName = "ScriptableObjects/Monster")]
public class MonsterScriptableObject : ScriptableObject
{
    [SerializeField]
    private int health = 0;
    [SerializeField]
    private int speed = 0;
    [SerializeField]
    private int damage = 0;
    [SerializeField]
    private float spawnChance = 0;
}
