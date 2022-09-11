using System.Collections;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timeText = null;

    [SerializeField]
    private int timer = 0;

    private void Start()
    {
        StartCoroutine(TimerCount());
    }

    private IEnumerator TimerCount()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timer++;

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
