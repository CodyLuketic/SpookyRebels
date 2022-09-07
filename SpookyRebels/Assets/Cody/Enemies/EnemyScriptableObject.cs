using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    private Mesh _enemyMesh = null;
    public Mesh enemyMesh
    {
        get {return _enemyMesh;}
        set {_enemyMesh = value;}
    }

    [SerializeField]
    private Material _enemyMaterial = null;
    public Material enemyMaterial
    {
        get {return _enemyMaterial;}
        set {_enemyMaterial = value;}
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
    private float _defense = 0;
    public float defense
    {
        get {return _defense;}
        set {_defense = value;}
    }

    [SerializeField]
    private float _spawnChance = 0;
    public float spawnChance
    {
        get {return _spawnChance;}
        set {_spawnChance = value;}
    }

    
    [SerializeField]
    private Animation _attackAnim = null;
    public Animation attackAnim
    {
        get {return _attackAnim;}
        set {_attackAnim = value;}
    }

    [Header("Type")]
    [SerializeField]
    private bool _melee = false;
    public bool melee
    {
        get {return _melee;}
        set {_melee = value;}
    }

    [Header("Melee Values")]
    [SerializeField]
    private float _bounceBack = 0;
    public float bounceBack
    {
        get {return _bounceBack;}
        set {_bounceBack = value;}
    }

    [Header("Ranged Values")]
    [SerializeField]
    private Mesh _projectileMesh = null;
    public Mesh projectileMesh
    {
        get {return _projectileMesh;}
        set {_projectileMesh = value;}
    }

    [SerializeField]
    private Material _projectileMaterial = null;
    public Material projectileMaterial
    {
        get {return _projectileMaterial;}
        set {_projectileMaterial = value;}
    }

    [SerializeField]
    private float _attackSpeed = 0;
    public float attackSpeed
    {
        get {return _attackSpeed;}
        set {_attackSpeed = value;}
    }
}
