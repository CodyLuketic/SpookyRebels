using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    float m_LastPressTime;
    float m_PressDelay = 0.5f;
    private bool canvasEnabled = false;

    void Update()
    {
        if (Input.GetButton("Popup"))
        {
            popUp();
        }
    }

    public void popUp()
    {
        // Ensure Player Doesn't Get Stuck on Button Press
        if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
            return;
        m_LastPressTime = Time.unscaledTime;

        // Enable or Disable the Canvas
        GetComponent<Canvas>().enabled = !canvasEnabled;
        canvasEnabled = !canvasEnabled;

        // Pause
        if (canvasEnabled) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void popScene(string sceneName)
    {
        Time.timeScale = 1;
        //GameObject.Find("SceneChanger").GetComponent<SceneChanger>().LoadScene(sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
