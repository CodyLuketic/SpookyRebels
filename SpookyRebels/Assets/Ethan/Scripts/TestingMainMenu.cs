using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingMainMenu : MonoBehaviour
{
    [SerializeField]
    private Canvas SettingsCanvas;
    [SerializeField]
    private Canvas NewGameCanvas;

    private bool subCanvasEnabled = false;

    /////////////////////////////////////////////////////////////////////// CANVAS SWITCH

    public void SettingsProc()
    {
        CleanCanvas(SettingsCanvas);
        SettingsCanvas.enabled = !SettingsCanvas.enabled;
        subCanvasEnabled = true;
    }

    public void NewGameProc()
    {
        CleanCanvas(NewGameCanvas);
        NewGameCanvas.enabled = !NewGameCanvas.enabled;
        subCanvasEnabled = true;
    }

    /////////////////////////////////////////////////////////////////////// HELPER METHODS

    private void CleanCanvas()
    {
        // Check to see if any Layered Canvases are already enabled, disable them all if they are
        if (subCanvasEnabled)
        {
            foreach (GameObject layerCanvas in GameObject.FindGameObjectsWithTag("LayerCanvas"))
            {
                layerCanvas.GetComponent<Canvas>().enabled = false;
            }
            subCanvasEnabled = false;
        }
    }

    private void CleanCanvas(Canvas canvas)
    {
        // Check to see if any Layered Canvases are already enabled, disable them all if they are
        if (subCanvasEnabled)
        {
            foreach (GameObject layerCanvas in GameObject.FindGameObjectsWithTag("LayerCanvas"))
            {
                Canvas tmp = layerCanvas.GetComponent<Canvas>();
                if (!canvas.Equals(tmp))
                {
                    tmp.enabled = false;
                }
            }
            subCanvasEnabled = false;
        }
    }
}
