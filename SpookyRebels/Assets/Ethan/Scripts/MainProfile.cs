using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProfile : MonoBehaviour
{
    public static MainProfile Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public Profile mainP;
}
