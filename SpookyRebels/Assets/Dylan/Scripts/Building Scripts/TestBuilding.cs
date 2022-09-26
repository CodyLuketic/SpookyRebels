using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestBuilding : MonoBehaviour
{
    float m_LastPressTime;
    float m_PressDelay = 0.5f;

    private bool canvasEnabled = false;

    [SerializeField]
    private TextMeshPro myText;

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
                GameObject CanvasObject = GameObject.FindGameObjectWithTag("TestBuildingCanvas");
                CanvasObject.GetComponent<Canvas>().enabled = !canvasEnabled;
                canvasEnabled = !canvasEnabled;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change text color to white on enter
            myText.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change text color to grey on exit
            myText.color = new Color32(75, 75, 75, 255);
        }
    }

}
