using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPooler : MonoBehaviour
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

    private int poolIndex = 0;

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

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(poolIndex.ToString(), objectPool);
            Debug.Log(poolIndex);
            poolIndex++;
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
            
        Debug.Log(index);
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
        enemyInstance.GetComponent<EnemyValues>().IncreaseValues(level);
        
        poolDictionary[index].Enqueue(enemyInstance);

        return enemyInstance;
    }

    public void SpawnBoss()
    {
        SpawnBossHelper();
    }

    private void SpawnBossHelper()
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
