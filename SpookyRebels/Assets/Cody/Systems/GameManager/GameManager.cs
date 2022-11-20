using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject timerObject = null;

    [SerializeField]
    private GameObject materialObject = null;

    [SerializeField]
    private GameObject popupObject = null;

    [SerializeField]
    private GameObject winObject = null;

    [SerializeField]
    private GameObject loseObject = null;

    public void Win()
    {
        WinHelper();
    }
    private void WinHelper()
    {
        timerObject.SetActive(false);
        materialObject.SetActive(false);
        popupObject.SetActive(false);
        winObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void Lose()
    {
        LoseHelper();
    }
    private void LoseHelper()
    {
        timerObject.SetActive(false);
        materialObject.SetActive(false);
        popupObject.SetActive(false);
        loseObject.SetActive(true);

        Time.timeScale = 0;
    }
}
