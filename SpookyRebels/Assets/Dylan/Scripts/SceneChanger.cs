using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void EthanScene()
    {
        SceneManager.LoadScene(1);
    }

    public void DylanScene()
    {
        SceneManager.LoadScene(2);
    }

    public void CodyScene()
    {
        SceneManager.LoadScene(3);
    }

    public void SkillScene()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
