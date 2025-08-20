using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    // The new Input System
    private PlayerInputs input = null;

    private Vector2 moveVector = Vector2.zero;

    [Header("FootSteps")]
    public AudioSource playerFootStepsSrc;
    
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    public float airMultipilier;
    private bool readyToJump;
    
    public static bool lockMovement = false;

    [Header("Keybindings")] 
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check")] 
    public float playerHeight;

    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")] 
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Collide and Slide")] 
    public int maxBounces = 5;
    public float skinWidth = 0.015f;
    public float minWallDistance = 0.5f;
    private Bounds bounds;
    public LayerMask WallEnv;
    RaycastHit Wallhit;
    
    
    
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;


    private void Awake()
    {
        input = new PlayerInputs();
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovOnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovOnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        if (!playerFootStepsSrc.isPlaying && moveVector != Vector2.zero)
        {
            playerFootStepsSrc.Play();
        }
        else if (!input.Player.Movement.IsPressed())
        {
            playerFootStepsSrc.Stop();
        }
       
        
    }

    private void OnMovOnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
       
    }
    
    
    private void Update()
    {
        
        
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        
        MyInput();
        SpeedControl();
        
        // Handel drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    
    private void FixedUpdate()
    {
        if (!lockMovement)
        {
            MovePlayer();
        } 
    }

    // Update is called once per frame
    void MyInput()
    {
        horizontalInput = moveVector.x;
        verticalInput = moveVector.y;
        
        /*
        //when to jump
        if ( input.Player.Jump.triggered && readyToJump && grounded)
        {
            readyToJump = false;
            
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }*/
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        //CollideAndSlide(rb.velocity, transform.position, 0);
        
        // On slope
        if (OnWall())
        {
            rb.AddForce(GetWallMoveDirection() * (moveSpeed - 2) * 5f, ForceMode.Force);
        }
        
        
        else if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
            
        }
        // On ground
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // In air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultipilier, ForceMode.Force);
        }
        
        // Turns off gravity while on slope
        rb.useGravity = !OnSlope();

    }
    private void SpeedControl()
    {
        // Limit velocity on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        // Limiting velocity on ground or air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    /*
    public void Jump()
    {
        exitingSlope = true;
        
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    */
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        else
        {
            return false; 
        }
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    
    private Vector3 GetWallMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, Wallhit.normal).normalized;
    }

    private bool OnWall()
    {
        float distance = 0.4f + skinWidth;
        Vector3 p1 = transform.position + Vector3.down * playerHeight * 0.5F;
        Vector3 p2 = p1 + Vector3.up * playerHeight;
        
        if (Physics.CapsuleCast(p1, p2, 0.45f,
                moveDirection, out Wallhit ,minWallDistance, WallEnv)) //radius was 05f before
        {
            return true;
        }

        return false;

    }

    public static void LockPlayerCursorVisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        lockMovement = true;
        FirstPersonCamera.lockCamera = true;
    }
    
    public static void LockPlayer()
    {
        lockMovement = true;
        FirstPersonCamera.lockCamera = true;
    }
    
    
    public static void UnLockPlayer()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lockMovement = false;
        FirstPersonCamera.lockCamera = false;
    }
}
