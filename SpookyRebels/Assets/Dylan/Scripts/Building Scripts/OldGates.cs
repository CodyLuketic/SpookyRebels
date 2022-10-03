using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OldGates : MonoBehaviour
{
    float m_LastPressTime;
    float m_PressDelay = 0.5f;

    private bool canvasEnabled = false;

    [SerializeField]
    private TextMeshPro myText;
    [SerializeField]
    private GameObject myActivateText;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButton("Interact"))
            {
                // Ensure Player Doesn't Get Stuck on Button Press
                if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
                    return;
                m_LastPressTime = Time.unscaledTime;

                // Disable or Enable Player Movement on Activation
                PlayerMovements player = other.GetComponent<PlayerMovements>();
                player.canMove = !player.canMove;

                // Enable or Disable the Canvas
                GameObject CanvasObject = GameObject.FindGameObjectWithTag("OldGatesCanvas");
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

            // Turn on activate prompt
            myActivateText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change text color to grey on exit
            myText.color = new Color32(75, 75, 75, 255);

            // Turn off activate prompt
            myActivateText.SetActive(false);
        }
    }
}
