using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [SerializeField]
    private GameObject testHousePrefab;
    [SerializeField]
    private GameObject galleryPrefab;
    /*
    [SerializeField]
    private GameObject daycarePrefab;
    [SerializeField]
    private GameObject repairPrefab;
    [SerializeField]
    private GameObject settingsPrefab;
    [SerializeField]
    private GameObject gymPrefab;
    [SerializeField]
    private GameObject shipPrefab;
    */

    private int currentCost = 0;
    private bool buildEnabled = false;

    [SerializeField]
    private Canvas buildCanvas;
    [SerializeField]
    private Canvas hubCanvas;
    [SerializeField]
    private GameObject buildPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Return"))
        {
            hubCanvas.enabled = buildEnabled;
            buildCanvas.enabled = !buildEnabled;

            buildEnabled = !buildEnabled;

            if (!buildEnabled && FindObjectsOfType<BlueprintScript>() != null)
            {
                CleanSelection();
            }
        }
    }

    public void SpawnBuilding(string building)
    {
        CleanSelection();

        switch (building)
        {
            case "Gallery":
                Instantiate(galleryPrefab, transform.position, transform.rotation);
                currentCost = 10;
                break;
            default:
                Instantiate(testHousePrefab, transform.position, transform.rotation);
                currentCost = 0;
                break;
        }
    }

    private void CleanSelection()
    {
        currentCost = 0;

        if (FindObjectsOfType<BlueprintScript>() != null)
        {
            foreach (BlueprintScript blueprint in FindObjectsOfType<BlueprintScript>())
            {
                blueprint.Cancel();
            }
        }
    }

    public void OpenPanel()
    {
        print("stuck on bandaid");
        buildPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        buildPanel.SetActive(false);
    }

}
