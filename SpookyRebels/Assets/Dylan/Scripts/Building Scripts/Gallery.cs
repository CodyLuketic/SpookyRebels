using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallery : MonoBehaviour
{
    float m_LastPressTime;
    float m_PressDelay = 0.5f;

    private bool canvasEnabled = false; 

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButton("Fire1"))
            {
                // Ensure Player Doesn't Get Stuck on Button Press
                if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
                    return;
                m_LastPressTime = Time.unscaledTime;

                // Disable or Enable Player Movement on Activation
                PlayerMovements player = other.GetComponent<PlayerMovements>();
                player.canMove = !player.canMove;

                // Enable or Disable the Canvas
                GameObject CanvasObject = GameObject.FindGameObjectWithTag("GalleryCanvas");
                CanvasObject.GetComponent<Canvas>().enabled = !canvasEnabled;
                canvasEnabled = !canvasEnabled;
            }
        }
    }

}
