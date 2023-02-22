using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float movementSpeed;
    public Rigidbody rb;
    public Animator anim;
    private Vector2 moveDirection;
    private float lastX;
    private float lastY;

    private string latest = "X";
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }
    void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if(moveX != lastX && moveY != lastY)
        {
            lastX = moveX;
            lastY = moveY;

        }
        else if (moveX != lastX && moveY == lastY && moveX == 0)
        {
            moveDirection = new Vector2(0, moveY).normalized;
            lastX = moveX;
        }
        else if (moveX != lastX && moveY == lastY)
        {
            moveDirection = new Vector2(moveX, 0).normalized;
            lastX = moveX;

        }
        else if (moveX == lastX && moveY != lastY && moveY == 0)
        {
            moveDirection = new Vector2(moveX, 0).normalized;
            lastY = moveY;
        }
        else if (moveX == lastX && moveY != lastY)
        {
            moveDirection = new Vector2(0, moveY).normalized;
            lastY = moveY;
        }
        else if (moveX == 0 && moveY == 0)
        {
            lastX = moveX;
            lastY = moveY;
            moveDirection = new Vector2(0, 0).normalized;
        }
        /*if (moveX != 0 && moveY != 0)
        {
            if (latest == "X")
            {
                moveDirection = new Vector2(moveX, 0).normalized;
            }
            else if(latest == "Y")
            {
                moveDirection = new Vector2(0, moveY).normalized;
            }
        }
        else if (moveX == 0 && moveY == 0)
        {
            moveDirection = new Vector2(0, 0).normalized;
        }
        else if (moveX != 0)
        {
            moveDirection = new Vector2(moveX, 0).normalized;
            latest = "X";
        }
        else if (moveY != 0)
        {
            moveDirection = new Vector2(0, moveY).normalized;
            latest = "Y";
        }*/

        
        //Debug.Log(latest);
        /*if (moveX < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if(moveX > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }

        if (moveX == 0 && moveY == 0)
        {
            anim.SetBool("isMoving", false); 
        }
        else
        {
            anim.SetBool("isMoving", true);
        }*/
    }

    void Move()
    {
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, 0, moveDirection.y * movementSpeed);
    }
}

