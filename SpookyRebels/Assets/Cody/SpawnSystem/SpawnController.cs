using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies = null;

    [SerializeField]
    private GameObject boss = null;

    [SerializeField]
    private float minRadius = 0, maxRadius = 0, time = 0;

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
            GameObject enemyInstance = Instantiate(
                enemies[Random.Range(0, enemies.Length)],
                Vector3.zero, Quaternion.identity);

            float ranX = Random.Range(-maxRadius, maxRadius);
            float ranZ = Random.Range(-maxRadius, maxRadius);

            if(ranX < minRadius && ranX > -minRadius && ranZ < minRadius && ranZ > -minRadius)
            {
                int temp = Random.Range(0, 1);

                if(temp == 1)
                {
                    if(Mathf.Sign(ranX) == 1)
                    {
                        ranX += minRadius;
                    }
                    else
                    {
                        ranX -= minRadius;
                    }
                }
                else
                {
                    if(Mathf.Sign(ranZ) == 1)
                    {
                        ranZ += minRadius;
                    }
                    else
                    {
                        ranZ -= minRadius;
                    }
                }
            }
            Vector3 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            spawnPos += new Vector3(ranX, 0,ranZ);

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

        float ranX = Random.Range(-maxRadius, maxRadius);
        float ranZ = Random.Range(-maxRadius, maxRadius);

        if(ranX < minRadius && ranX > -minRadius && ranZ < minRadius && ranZ > -minRadius)
        {
            int temp = Random.Range(0, 1);

            if(temp == 1)
            {
                if(Mathf.Sign(ranX) == 1)
                {
                    ranX += minRadius;
                }
                else
                {
                    ranX -= minRadius;
                }
            }
            else
            {
                if(Mathf.Sign(ranZ) == 1)
                {
                    ranZ += minRadius;
                }
                else
                {
                    ranZ -= minRadius;
                }
            }
        }
        Vector3 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        spawnPos += new Vector3(ranX, 0,ranZ);

        NavMeshHit closestHit;
        if(NavMesh.SamplePosition(spawnPos, out closestHit, 500, 1 ))
        {
            bossInstance.transform.position = closestHit.position;
            bossInstance.AddComponent<NavMeshAgent>();  
        }

        bossInstance.GetComponent<EnemyValues>().ApplyValues();
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
