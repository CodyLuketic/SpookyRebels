using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void Update()
    {
        Vector3 playPos = player.transform.position;
        float x = playPos.x;
        float y = playPos.y + 14.57f;
        float z = playPos.z - 10;
        transform.position = new Vector3(x, y, z);
    }
}
