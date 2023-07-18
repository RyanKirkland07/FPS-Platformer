using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 MoveDirection;

    public Rigidbody rb;
    public BoxCollider hitbox;
    
    public float speed;

    public LayerMask groundMask;
    public float GroundDistance = 0.4f;
    bool isGrounded;
    public Transform GroundCheck;

    public float GroundDrag;
    public float inairMultiplier;

    public float playerHeight;

    public float jumpHeight = 8f;

    public RaycastHit rampHit;
    public float maxAngle;

    public Sliding slide;



    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);
        if (isGrounded)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        GetInput();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            DoJump();
        }
        if(onSlope())
        {
            Debug.Log("Slope working");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        MoveDirection = orientation.right * horizontalInput + orientation.forward * verticalInput;
        if(onSlope())
        {
            rb.AddForce(SlopeMoveDirection() * speed * 10f, ForceMode.Force);
        }
        if(isGrounded)
            rb.AddForce(MoveDirection.normalized * speed * 10f, ForceMode.Force);
        else if(!isGrounded)
            rb.AddForce(MoveDirection.normalized * speed * 10f * inairMultiplier, ForceMode.Force);
    }
    private void DoJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        
    }

    private bool onSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out rampHit, 1f))
        {
            float angle = Vector3.Angle(Vector3.up, rampHit.normal);
            return angle < maxAngle && angle != 0;
        }

        return false;
    }
    private Vector3 SlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(MoveDirection, rampHit.normal).normalized;
    }
}
