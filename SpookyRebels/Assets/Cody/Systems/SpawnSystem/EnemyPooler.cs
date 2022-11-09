using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    private List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private Coroutine spawnCoroutine = null;

    [SerializeField]
    private GameObject boss = null;

    [SerializeField]
    private float minRadius = 0, maxRadius = 0, time = 0;

    [SerializeField]
    private int level = 1;

    private void Start()
    {
        SetDictionary();
        spawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    private void SetDictionary()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        int index = 0;

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = gameObject.transform;
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(index.ToString(), objectPool);
            index++;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            SpawnFromPool();

            yield return new WaitForSeconds(time / level);
        }
    }

    private GameObject SpawnFromPool()
    {
        string index = Random.Range(0, pools.Count).ToString();
            
        if(!poolDictionary.ContainsKey(index))
        {
            Debug.LogWarning("Pool in index " + index + " doesn't exist");
            return null;
        }
        
        GameObject enemyInstance = poolDictionary[index].Dequeue();
        enemyInstance.SetActive(true);

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
        }

        enemyInstance.GetComponent<NavMeshAgent>().enabled = true;
        if(enemyInstance.name == "CrabContainer")
        {
            enemyInstance.GetComponent<CrabValues>().IncreaseValues(level);
        } else if(enemyInstance.name == "DodoContainer")
        {
            enemyInstance.GetComponent<DodoValues>().IncreaseValues(level);
        }
        
        poolDictionary[index].Enqueue(enemyInstance);

        return enemyInstance;
    }

    public GameObject SpawnBoss()
    {
        return SpawnBossHelper();
    }

    private GameObject SpawnBossHelper()
    {
        GameObject bossInstance = Instantiate(boss);

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

        return bossInstance;
    }

    public void IncreaseLevel()
    {
        IncreaseLevelHelper();
    }
    private void IncreaseLevelHelper()
    {
        level++;
    }

    public void EndSpawnCoroutine()
    {
        EndSpawnCoroutineHelper();
    }
    private void EndSpawnCoroutineHelper()
    {
        StopCoroutine(spawnCoroutine);
    }
}
