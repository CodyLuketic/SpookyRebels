using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = null;

    [SerializeField]
    private GameObject player = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + player.transform.forward;
            Instantiate(bullet, position, player.transform.rotation);
        }
    }
}
