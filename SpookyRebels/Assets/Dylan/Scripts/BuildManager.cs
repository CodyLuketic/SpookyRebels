using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private GameObject testHousePrefab;

    private int currentCost;

    public void SpawnBuilding(string building)
    {
        cleanSelection();

        if (building.Equals("Test House"))
        {
            Instantiate(testHousePrefab, transform.position, transform.rotation);
            currentCost = 1;
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
