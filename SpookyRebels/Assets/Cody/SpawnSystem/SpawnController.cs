using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies = null;

    [SerializeField]
    private GameObject boss = null;

    [SerializeField]
    private float spawnRadius = 0, time = 0;

    [SerializeField]
    private int level = 1;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject enemyInstance = Instantiate(enemies[Random.Range(0, enemies.Length)], Vector3.zero, Quaternion.identity);

            Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

            NavMeshHit closestHit;
            if(NavMesh.SamplePosition(spawnPos, out closestHit, 500, 1 ))
            {
                enemyInstance.transform.position = closestHit.position;
                enemyInstance.AddComponent<NavMeshAgent>(); 
            }

            enemyInstance.GetComponent<EnemyValues>().IncreaseValues(level);
            
            yield return new WaitForSeconds(time / level);
        }
    }

    public void SpawnBoss()
    {
        SpawnBossHelper();
    }

    private void SpawnBossHelper()
    {
        GameObject bossInstance = Instantiate(boss, Vector3.zero, Quaternion.identity);

        Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        NavMeshHit closestHit;
        if(NavMesh.SamplePosition(spawnPos, out closestHit, 500, 1 ))
        {
            bossInstance.transform.position = closestHit.position;
            bossInstance.AddComponent<NavMeshAgent>();  
        }

        bossInstance.GetComponent<EnemyValues>().IncreaseValues(level);
    }

    public void IncreaseLevel()
    {
        IncreaseLevelHelper();
    }
    private void IncreaseLevelHelper()
    {
        level++;
    }
}
