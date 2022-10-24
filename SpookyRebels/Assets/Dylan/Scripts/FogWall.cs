using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    private SceneChanger scenechanger = null;

    [SerializeField]
    string input = "";

    private void Awake()
    {
        if (FindObjectOfType<SceneChanger>() != null)
        {
            scenechanger = FindObjectOfType<SceneChanger>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (input)
            {
                case "World 1":
                    scenechanger.CodyScene();
                    break;
                default:
                    break;
            }
        }
    }
}
