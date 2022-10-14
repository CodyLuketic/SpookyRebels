using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    float m_LastPressTime;
    float m_PressDelay = 0.5f;
    private bool canvasEnabled = false;

    public Vector3 lookPoint;
    public Vector3 gunPoint; 

    //private Animator animator = null;
    //public Texture2D crosshair;
    CharacterController characterController;

    [SerializeField]
    GameObject playerFull;
    [SerializeField]
    Camera cam;

    public Vector3 moveDirection = Vector3.zero;
    public static Vector3 mouseWorldPosition;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        //Vector2 cursorOffset = new Vector2(crosshair.width/2, crosshair.height/2);
        //SetCursor(crosshair, cursorOffset, CursorMode.Auto);
    }

    private void generateMousePos()
    {
        // Fetch Cursor position
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mouseWorldPosition = ray.GetPoint(distance);
        }
    }

    public Vector3 returnMousePos()
    {
        return mouseWorldPosition;
    }

    void Update()
    {
        // Check that we can Move
        if (canMove)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = playerFull.transform.TransformDirection(Vector3.forward);
            Vector3 right = playerFull.transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            //float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);



        }

        generateMousePos();
        // Player rotation to Cursor
        lookPoint = mouseWorldPosition;
        lookPoint.y = transform.position.y;
        transform.LookAt(lookPoint, Vector3.up);

        gunPoint = transform.forward;
        Vector3 getRotation()
        {
            return moveDirection;
        }

        if (Input.GetButton("Popup"))
        {
            // Ensure Player Doesn't Get Stuck on Button Press
            if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
                return;
            m_LastPressTime = Time.unscaledTime;

            // Enable or Disable the Canvas
            GameObject CanvasObject = GameObject.FindGameObjectWithTag("popupCanvas");
            CanvasObject.GetComponent<Canvas>().enabled = !canvasEnabled;
            canvasEnabled = !canvasEnabled;
        }


        // Extraneous Code

        /*
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        */

        /*
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        */
    }
}
