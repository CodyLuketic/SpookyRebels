using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    private SpawnManager spawnManager = null;

    [SerializeField]
    private TMP_Text timeText = null;

    private int timer = 0, tempTimer = 0;

    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

        StartCoroutine(TimerCount());
    }

    private IEnumerator TimerCount()
    {
        while(timer <= 300)
        {
            yield return new WaitForSeconds(1);
            timer++;

            if(timer >= tempTimer + 40)
            {
                tempTimer = timer;

                spawnManager.IncreaseLevel();
            }
            UpdateTimeText();
        }

        spawnManager.SpawnBoss();

        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
