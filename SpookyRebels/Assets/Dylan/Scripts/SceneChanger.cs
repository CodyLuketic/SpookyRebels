using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

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

    public void SkillScene(string monType)
    {
        switch (monType)
        {
            case "Hermine":
                MainProfile.Instance.mainP.skillMonEquipted = 1;
                break;
            case "Dodivy":
                MainProfile.Instance.mainP.skillMonEquipted = 2;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
