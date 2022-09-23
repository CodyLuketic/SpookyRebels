using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingMainMenu : MonoBehaviour
{
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
}
