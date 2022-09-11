using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy = null;

    [SerializeField]
    private EnemyScriptableObject[] enemies = null;

    [SerializeField]
    private float spawnRadius = 0, time = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

            GameObject enemyInstance = Instantiate(enemy, spawnPos, Quaternion.identity);
            enemyInstance.GetComponent<EnemyValues>().SetEnemyParts(enemies[Random.Range(0, enemies.Length)]);

            yield return new WaitForSeconds(time);
        }
    }
}
