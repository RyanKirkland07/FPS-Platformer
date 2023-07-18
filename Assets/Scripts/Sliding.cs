using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    //Movement
    private Rigidbody rb;
    public CharacterController pc;
    public Vector3 inputDirection;

    // Transforms
    public Transform orientation;
    public Transform Capsule;
    public BoxCollider hitbox;

    //Slide Variables
    public float maxSlideTime;
    public float slideTimer;
    public float slidepower;

    //Slide Check
    bool sliding;
    bool isCrouched;

    //Horizontal and Vertical
    private float horizontalInput;
    private float verticalInput;

    // Ground Check
    public bool Grounded;
    public LayerMask groundMask;
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;


    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<CharacterController>();
        maxSlideTime = 1.5f;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Grounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

        if(Input.GetKeyDown(KeyCode.LeftShift) && (horizontalInput != 0 || verticalInput != 0) && Grounded)
        {
            StartSlide();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && sliding)
        {
            StopSlide();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && (horizontalInput == 0) && (verticalInput == 0))
        {
            crouchStart();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            stopCrouch();
        }
    }

    private void FixedUpdate()
    {
        if(sliding && Grounded)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        sliding = true;
        Capsule.localScale = new Vector3(Capsule.localScale.x, 0.5f, Capsule.localScale.z);
        //hitbox.size = new Vector3(1, 1, 1);
        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(inputDirection * slidepower, ForceMode.Impulse);
        slideTimer -= Time.deltaTime;
        if(slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        inputDirection = new Vector3(0, 0 , 0);
        sliding = false;
        Capsule.localScale = new Vector3(Capsule.localScale.x, 1, Capsule.localScale.z);
        //hitbox.size = new Vector3(1, 2, 1);
    }

    private void crouchStart()
    {
        Capsule.localScale = new Vector3(Capsule.localScale.x, 0.5f, Capsule.localScale.z);
        //hitbox.size = new Vector3(1, 1, 1);
        bool isCrouched = true;
    }
    private void stopCrouch()
    {
        Capsule.localScale = new Vector3(Capsule.localScale.x, 1f, Capsule.localScale.z);
        //hitbox.size = new Vector3(1, 2, 1);
    }
    }
