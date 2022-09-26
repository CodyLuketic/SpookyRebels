using System.Collections;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private SpawnController spawnController = null;

    [SerializeField]
    private TMP_Text timeText = null;

    private int timer = 0, tempTimer = 0;

    private void Start()
    {
        spawnController = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<SpawnController>();

        StartCoroutine(TimerCount());
    }

    private IEnumerator TimerCount()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timer++;

            if(timer >= tempTimer + 40)
            {
                tempTimer = timer;

                spawnController.IncreaseLevel();
            }

            if(timer % 50 == 0)
            {
                spawnController.SpawnBoss();
            }

            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
