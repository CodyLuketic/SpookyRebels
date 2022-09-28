using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject timerObject = null;
    private Canvas timerCanvas = null;

    [SerializeField]
    private GameObject winObject = null;
    private Canvas winCanvas = null;

    [SerializeField]
    private GameObject loseObject = null;
    private Canvas loseCanvas = null;

    private void Start()
    {
        timerCanvas = timerObject.GetComponent<Canvas>();
        winCanvas = winObject.GetComponent<Canvas>();
        loseCanvas = loseObject.GetComponent<Canvas>();
    }

    public void Win()
    {
        WinHelper();
    }
    private void WinHelper()
    {
        timerObject.SetActive(false);
        winObject.SetActive(true);
    }

    public void Lose()
    {
        LoseHelper();
    }
    private void LoseHelper()
    {
        timerObject.SetActive(false);
        loseObject.SetActive(true);
    }
}
