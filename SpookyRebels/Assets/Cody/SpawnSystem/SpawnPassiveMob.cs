using UnityEngine;
using UnityEngine.AI;

public class SpawnPassiveMob : MonoBehaviour
{
    private PassiveMobMovement passiveMobMovScript = null;

    [SerializeField]
    private GameObject passiveMob = null;

    private Vector3 spawnPos;

    private void Start()
    {
        spawnPos = gameObject.transform.position;

        GameObject passiveMobInstance = Instantiate(passiveMob, spawnPos, Quaternion.identity);

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
