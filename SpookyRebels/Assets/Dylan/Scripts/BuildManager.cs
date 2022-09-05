using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private GameObject testHousePrefab;

    public void SpawnBuilding(string building)
    {
        if (building.Equals("Test House"))
        {
            Instantiate(testHousePrefab, transform.position, transform.rotation);
        }
    }
}
