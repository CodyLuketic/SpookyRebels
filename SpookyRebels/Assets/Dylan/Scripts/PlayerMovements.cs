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

    public Vector3 lookPoint;
    public Vector3 gunPoint; 

    [SerializeField]
    private Animator animator = null;
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

    //Dash stuff
    public float maxDashTime = 2.0f;
    public float dashDistance = 5;
    public float dashStoppingSpeed = 0.1f;
    public float currentDashTime = 10;
    public float dashSpeed = 20;

    //Recoil stuff
    public float maxDashTimeRe = 1.0f;
    public float dashDistanceRe = 2;
    public float dashStoppingSpeedRe = 0.1f;
    public float currentDashTimeRe = 10;
    public float dashSpeedRe = 10;
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
            if (currentDashTimeRe < maxDashTimeRe)
            {
                moveDirection = transform.forward * dashDistance * -1;
                currentDashTimeRe += dashStoppingSpeedRe;
                // Move the controller
                characterController.Move(moveDirection * Time.deltaTime * dashSpeedRe);
            }
            else if (currentDashTime < maxDashTime)
            {
                moveDirection = transform.forward * dashDistance;
                currentDashTime += dashStoppingSpeed;
                // Move the controller
                characterController.Move(moveDirection * Time.deltaTime * dashSpeed);
            }
            else
            {
                //moveDirection = Vector3.zero;

                // We are grounded, so recalculate move direction based on axes
                Vector3 forward = playerFull.transform.TransformDirection(Vector3.forward);
                Vector3 right = playerFull.transform.TransformDirection(Vector3.right);

                // Press Left Shift to run
                bool isRunning = Input.GetKey(KeyCode.LeftShift);
                if (isRunning) { animator.speed = 2; } else { animator.speed = 1; }
                float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

                //float movementDirectionY = moveDirection.y;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                // Move the controller
                characterController.Move(moveDirection * Time.deltaTime);

                if (animator != null)
                {
                    // Animate the character
                    if (moveDirection != new Vector3(0, 0, 0))
                    {
                        animator.SetBool("Walking", true);
                    }
                    else
                    {
                        animator.SetBool("Walking", false);
                    }
                }
            }

        }
        else { animator.SetBool("Walking", false); }

        generateMousePos();
        // Player rotation to Cursor
        lookPoint = mouseWorldPosition;
        lookPoint.y = transform.position.y;
        transform.LookAt(lookPoint, Vector3.up);

        gunPoint = transform.forward;
        /*
        Vector3 getRotation()
        {
            return moveDirection;
        }
        */

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
