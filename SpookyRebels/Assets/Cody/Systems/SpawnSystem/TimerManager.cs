using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private EnemyPooler enemyPooler = null;

    private Coroutine timeCoroutine = null;

    [SerializeField]
    private TMP_Text timeText = null;

    private int timer = 0, tempTimer = 0;

    [SerializeField]
    private float bossSpawnTime = 300f;

    private void Start()
    {
        timeCoroutine = StartCoroutine(TimerCount());
    }

    private IEnumerator TimerCount()
    {
        while(timer <= bossSpawnTime)
        {
            yield return new WaitForSeconds(1);
            timer++;

            if(timer >= tempTimer + 45)
            {
                tempTimer = timer;

                enemyPooler.IncreaseLevel();
            }
            UpdateTimeText();
        }

        enemyPooler.EndSpawnCoroutine();

        enemyPooler.SpawnBoss();

        UpdateTimeText();

        EndTimerCount();
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    private void EndTimerCount()
    {
        StopCoroutine(timeCoroutine);
    }
}
