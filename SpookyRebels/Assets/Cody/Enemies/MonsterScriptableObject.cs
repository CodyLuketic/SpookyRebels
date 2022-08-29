using UnityEngine;

[CreateAssetMenu(fileName = "MonsterScriptableObject", menuName = "ScriptableObjects/Monster")]
public class MonsterScriptableObject : ScriptableObject
{
    [SerializeField]
    private Mesh _monsterMesh = null;
    public Mesh monsterMesh
    {
        get {return _monsterMesh;}
        set {_monsterMesh = value;}
    }

    [SerializeField]
    private Material _monsterMaterial = null;
    public Material monsterMaterial
    {
        get {return _monsterMaterial;}
        set {_monsterMaterial = value;}
    }

    [SerializeField]
    private int _health = 0;
    public int health
    {
        get {return _health;}
        set {_health = value;}
    }

    [SerializeField]
    private float _speed = 0;
    public float speed
    {
        get {return _speed;}
        set {_speed = value;}
    }

    [SerializeField]
    private float _damage = 0;
    public float damage
    {
        get {return _damage;}
        set {_damage = value;}
    }

    [SerializeField]
    private float _spawnChance = 0;
    public float spawnChance
    {
        get {return _spawnChance;}
        set {_spawnChance = value;}
    }
}
