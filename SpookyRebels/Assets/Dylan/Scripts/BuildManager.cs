using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private GameObject testHousePrefab;
    [SerializeField]
    private GameObject galleryPrefab;
    [SerializeField]
    /*
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

    public void SpawnBuilding(string building)
    {
        cleanSelection();

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

    private void cleanSelection()
    {
        currentCost = 0;

        if (GameObject.FindGameObjectWithTag("Blueprint") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Blueprint"));
        }
    }

}
