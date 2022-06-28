using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public CapsuleCollider col;

    public Transform pcamera;
    private float camRotationSpeed = 5f;
    //these below are so that camera will rotate from -65 to 75 not full 360 degrees
    private float camMinrotY = -60f;
    private float camMaxrotY = 75f;
    private float rotationSmoothSpeed = 10f;

    //public int health=10;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float maxSpeed = 15f;
    //speed is gonna be between walk and run speeds
    private float speed;
    public float jumpForce = 10f;

    private float extraGravity = 45f;

    private float bodyRotationX;
    private float camRotationY;
    private Vector3 directionIntentX;
    private Vector3 directionIntentY;

    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        lookRotation();
        ExtraGravity();
        isGrounded();
    }
    void FixedUpdate()
    {
        Movement();
        if (grounded && Input.GetButtonDown("Jump"))
        {
            jump();
        }
    }
    void lookRotation()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        // get camera and body rotational values
        bodyRotationX += Input.GetAxis("Mouse X") * camRotationSpeed;
        camRotationY += Input.GetAxis("Mouse Y") * camRotationSpeed;

        //Stop our camera from rotating 360 degrees (from min to max);
        camRotationY = Mathf.Clamp(camRotationY,camMinrotY,camMaxrotY);

        //create rotation targets and handle rotations of the body and camera
        Quaternion camTargetRotation = Quaternion.Euler(-camRotationY,0,0);
        Quaternion bodyTargetRotation = Quaternion.Euler(0, bodyRotationX, 0);

        //handle rotations
        //smoothely move from transform.rotation to bodytargetrotation by time.deltaTime*...
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation,
            Time.deltaTime* rotationSmoothSpeed);

        pcamera.localRotation = Quaternion.Lerp(pcamera.localRotation, camTargetRotation,
            Time.deltaTime* rotationSmoothSpeed);

    }
    void Movement()
    {
        //Direction must match camera direction
        directionIntentX = pcamera.right;
        directionIntentX.y = 0;
        directionIntentX.Normalize();

        directionIntentY = pcamera.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();

        //change our players velocity in this direction
        //(first forward then we add/deal with left and right (horizontal))
        rb.velocity = directionIntentY * Input.GetAxis("Vertical") * speed +
            directionIntentX * Input.GetAxis("Horizontal")* speed + Vector3.up*rb.velocity.y;

        //now we keep the velocity within max speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxSpeed);

        //control speed based on our movement state
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkSpeed;
        }
    }

    void ExtraGravity()
    {
        rb.AddForce(Vector3.down * extraGravity);
    }
    void isGrounded()
    {
        //check for collision from position to -transform.up (cuz transform.down doesnt exist)
        //1.25f is max distance the ray should check for collisions
        grounded = Physics.Raycast(transform.position, -transform.up, out _, 1.25f);
    }

    void jump()
    {
        rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
    }
}
