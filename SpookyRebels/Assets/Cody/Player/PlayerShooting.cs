using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = null;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + Vector3.forward;
            Instantiate(bullet, position, Quaternion.identity);
        }
    }
}
