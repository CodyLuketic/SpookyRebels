using UnityEngine;
using UnityEngine.AI;

public class BlueprintScript : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    private float mouseWheelRotation;
    private bool valid = true;

    Camera myCam;
    NavMeshAgent myAgent;

    public GameObject prefab;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();

        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, ground))
        {
            myAgent.SetDestination(hit.point);
            //transform.position = hit.point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        valid = false;
    }

    private void OnTriggerExit(Collider other)
    {
        valid = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Scroll-wheel based Rotation
        mouseWheelRotation = Input.mouseScrollDelta.y;
        transform.Rotate(Vector3.up, mouseWheelRotation * 10.0f);

        // Is on placeable terrain?
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, ground))
        {
            myAgent.SetDestination(hit.point);
            //transform.position = hit.point;
        }

        // Place
        if (Input.GetMouseButton(0) && valid)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
