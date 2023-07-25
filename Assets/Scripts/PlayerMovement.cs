using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Transform for where player is facing
    public Transform orientation;

    //Movement Inputs (W,A,S,D)
    public float horizontalInput;
    public float verticalInput;

    //Vector3 for where your player will move
    public Vector3 MoveDirection;

    //Important components
    public Rigidbody rb;
    public BoxCollider hitbox;
    
    //Speed of movement
    public float speed;

    //Components and variables for groundcheck
    public LayerMask groundMask;
    public float GroundDistance = 0.4f;
    bool isGrounded;
    public Transform GroundCheck;

    //Movement multipliers to decrease movement
    public float GroundDrag;
    public float inairMultiplier;

    //Height of the player object
    public float playerHeight;

    //Height of players jump
    public float jumpHeight = 8f;

    //Slope movement
    public RaycastHit rampHit;
    public float maxAngle;
    public bool Slope;

    //Sliding script
    public Sliding slide;

    //Time for boosts
    public float BoostTimeLength;

    //Speedboost
    public GameObject SpeedBoost;
    public SpeedPowerup SpeedBoostScript;

    // Start is called before the first frame update
    void Start()
    {  
        speed = 5.0f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        GameObject SpeedBoost = GameObject.Find("Speed Boost");
        SpeedPowerup SpeedBoostScript = SpeedBoost.GetComponent<SpeedPowerup>();
        GameObject SpeedBoostLocation = GameObject.Find("SpeedBoostLocation");
        Debug.Log("Active?" + gameObject.activeInHierarchy);
    }

    private void Update()
    {
        //Check if player is grounded
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);
        //Apply drag if grounded
        if (isGrounded)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        //Gets movement inputs
        GetInput();
        //Starts jump function
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            DoJump();
        }
    }

    private void FixedUpdate()
    {
        //Starts player moving script
        MovePlayer();
    }

    private void GetInput()
    {
        //Gets your input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Makes your Vector3 MoveDirection variable
        MoveDirection = orientation.right * horizontalInput + orientation.forward * verticalInput;
        //Applies slope movement if on slope
        if(onSlope())
        {
            rb.AddForce(SlopeMoveDirection() * speed * 10f, ForceMode.Force);
        }
        //If not grounded applies InAirMultiplier
        if(isGrounded)
            rb.AddForce(MoveDirection.normalized * speed * 10f, ForceMode.Force);
        else if(!isGrounded)
            rb.AddForce(MoveDirection.normalized * speed * 10f * inairMultiplier, ForceMode.Force);
    }
    private void DoJump()
    {
        //Jump
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        
    }

    public bool onSlope()
    {  
        //Detects if player is on a slope
        if(Physics.Raycast(transform.position, Vector3.down, out rampHit, 1.5f))
        {
            float angle = Vector3.Angle(Vector3.up, rampHit.normal);
            Slope = true;
            return angle < maxAngle && angle != 0;
        }
        else
        {
            Slope = false;
            return false;
        }
        
    }
    private Vector3 SlopeMoveDirection()
    {
        //Projects Vector3 onto slope
        return Vector3.ProjectOnPlane(MoveDirection, rampHit.normal).normalized;
    }

    public void Speedboost()
    {
        //Start Speedboost
        speed = 10f;
        BoostTimeLength = 60f;
        StartCoroutine(BoostTimer());

    }
    public void Jumpboost()
    {
        //Start Jumpboost
        jumpHeight = 16f;
        BoostTimeLength = 20f;
        StartCoroutine(BoostTimer());
    }

    IEnumerator BoostTimer()
    {
        //Starts timer for boosts and resets variables at the end of timer
        yield return new WaitForSeconds(BoostTimeLength);
        speed = 5f;
        jumpHeight = 8f;
    }


}
