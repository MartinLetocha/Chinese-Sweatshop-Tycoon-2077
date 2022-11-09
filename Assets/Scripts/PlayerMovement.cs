using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public Transform orientation;
    private float horizontal;
    private float vertical;
    private Vector3 moveDirection;
    public KeyCode runKey = KeyCode.LeftShift;
    private System.Object speedP = null;
    public float runningSpeed;
    //is  added on top of speed

    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;

    public System.Object _speedP
    {
        get { return _speedP;}
        set
        {
            if (_speedP == null)
            {
                _speedP = value;
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        speedP = speed;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    { 
       grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
       MyInput();
       SpeedControl();
       if (grounded)
       {
           rb.drag = groundDrag;
       }
       else
       {
           rb.drag = 0;
       }
    }

    void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(runKey))
        {
            speed = runningSpeed;
        }
        else
        {
            speed = (float)speedP;
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        if (grounded)
        {
            rb.AddForce(moveDirection * (speed * 10), ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection * (speed * 10 * airMultiplier), ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}

