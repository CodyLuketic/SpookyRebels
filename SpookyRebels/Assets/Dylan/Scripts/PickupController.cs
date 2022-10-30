using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("Pickup Settings")]

    [SerializeField]
    Transform holdArea;
    [SerializeField]
    float throwForce = 2000.0f;

    private GameObject heldObj;
    private Rigidbody heldObjRB;
    private float objY;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (heldObj != null)
            {
                ThrowObject();
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        if (obj.CompareTag("PassiveMob"))
        {
            objY = obj.transform.position.y;

            heldObjRB = obj.GetComponent<Rigidbody>();
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.velocity = Vector3.zero;
            heldObjRB.angularVelocity = Vector3.zero;

            heldObjRB.transform.position = holdArea.position;
            heldObjRB.transform.parent = holdArea;
            heldObj = obj;
        }
    }

    void DropObject()
    {
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObj.transform.position = new Vector3(heldObj.transform.position.x, objY, heldObj.transform.position.z);

        heldObjRB.constraints = RigidbodyConstraints.FreezePositionY;
        heldObj.transform.parent = null;
        heldObj = null;
    }

    void ThrowObject()
    {
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.AddForce(holdArea.transform.forward * throwForce);

        heldObjRB.constraints = RigidbodyConstraints.FreezePositionY;
        heldObj.transform.parent = null;
        heldObj = null;
    }
}
