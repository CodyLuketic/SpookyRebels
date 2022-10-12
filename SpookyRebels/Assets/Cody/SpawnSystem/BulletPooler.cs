using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
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

    private void Start()
    {
        SetDictionary();
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
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(index.ToString(), objectPool);
            index++;
        }
    }

    public GameObject SpawnFromPool(Transform enemyTransform) {
        return SpawnFromPoolHelper(enemyTransform);
    }

    private GameObject SpawnFromPoolHelper(Transform enemyTransform)
    {
        string index = Random.Range(0, pools.Count).ToString();
            
        if(!poolDictionary.ContainsKey(index))
        {
            Debug.LogWarning("Pool in index " + index + " doesn't exist");
            return null;
        }
        
        GameObject bulletInstance = poolDictionary[index].Dequeue();
        bulletInstance.SetActive(true);

        Vector3 spawnPos = new Vector3(enemyTransform.position.x, enemyTransform.position.y, enemyTransform.position.z) + enemyTransform.forward;
        bulletInstance.transform.position = spawnPos;
        bulletInstance.transform.eulerAngles = new Vector3(
        bulletInstance.transform.eulerAngles.x,
        enemyTransform.transform.eulerAngles.y,
        bulletInstance.transform.eulerAngles.z );

        poolDictionary[index].Enqueue(bulletInstance);

        return bulletInstance;
    }
}
