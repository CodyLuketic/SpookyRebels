using UnityEngine;
using UnityEngine.AI;

public class SpawnPassiveMob : MonoBehaviour
{
    [SerializeField]
    private GameObject passiveMob = null;

    private Vector3 spawnPos;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        spawnPos = gameObject.transform.position;

        GameObject passiveMobInstance = Instantiate(passiveMob, spawnPos, Quaternion.identity);

        passiveMobInstance.transform.parent = gameObject.transform;

        UnityEngine.AI.NavMeshHit closestHit;
        if(UnityEngine.AI.NavMesh.SamplePosition(spawnPos, out closestHit, 500, 1 ))
        {
            passiveMobInstance.transform.position = closestHit.position;
            passiveMobInstance.AddComponent<NavMeshAgent>(); 
        }

        passiveMobInstance.GetComponent<PassiveMobMovement>().SetSpawnPoint(gameObject.transform.position);

        passiveMobInstance.GetComponent<PassiveMobValues>().ApplyValues();
    }
}
