using UnityEngine;
using UnityEngine.AI;

public class BlueprintScript : MonoBehaviour
{
    private Vector3 mousePos;
    private float mouseWheelRotation;
    private bool valid = true;

    [SerializeField]
    private GameObject me;
    [SerializeField]
    private GameObject prefab;
    private GameObject player;

    private 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Get Mouse Position
        PlayerMovements playerScript = player.GetComponent<PlayerMovements>();
        mousePos = playerScript.returnMousePos();

        me.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 100);
        me.transform.position = mousePos;
    }

    private void OnTriggerStay(Collider other)
    {
        valid = false;
        me.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 100);
    }

    private void OnTriggerExit(Collider other)
    {
        valid = true;
        me.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 100);
    }

    private float FindMyHeight()
    {
        return GetComponent<MeshFilter>().mesh.bounds.extents.y * 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Move to Mouse
        me.transform.position = new Vector3(mousePos.x, mousePos.y + FindMyHeight(), mousePos.z);

        // Get Mouse Position
        PlayerMovements playerScript = player.GetComponent<PlayerMovements>();
        mousePos = playerScript.returnMousePos();

        // Scroll-wheel based Rotation
        mouseWheelRotation = Input.mouseScrollDelta.y;
        transform.Rotate(Vector3.up, mouseWheelRotation * 10.0f);

        // Place
        if (Input.GetButton("Fire1") && valid)
        {
            GameObject baby = Instantiate(prefab, transform.position, transform.rotation);
            baby.transform.parent = GameObject.Find("Buildings").transform;
            
            // Destroy Self and Resources
            Destroy(gameObject);
        }
    }

}
