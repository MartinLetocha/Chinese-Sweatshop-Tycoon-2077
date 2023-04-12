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
    private Vector2 moveDirection;
    private float lastX;
    private float lastY;

    private string latest = "X";

    public SpriteRenderer playerRen;
    public Animator playerAnim;

    private float _animBuffer;

    private float _maxAmount = 0.05f;
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
        playerAnim.SetBool("isWalking", true);
        playerAnim.SetBool("isWalkingSide", false);
        playerAnim.SetFloat("yAxis", moveY);

        if (moveX != 0)
        {
            playerAnim.SetBool("isWalkingSide", true);
            if (moveX < 0)
            {
                playerRen.flipX = true;
            }
            else
            {
                playerRen.flipX = false;
            }
        }
        
        
        if(moveX != lastX && moveY != lastY)
        {
            lastX = moveX;
            lastY = moveY;
        }
        else if (moveX == lastX && moveY == lastY)
        {
            if (moveX != 0)
            {
                playerAnim.SetBool("isWalkingSide", true);
                if (moveX < 0)
                {
                    playerRen.flipX = true;
                }
                else
                {
                    playerRen.flipX = false;
                }
            }
        }
        else if (moveX != lastX && moveY == lastY && moveX == 0)
        {
            moveDirection = new Vector2(0, moveY).normalized;
            playerAnim.SetBool("isWalkingSide", false);
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
            playerAnim.SetBool("isWalkingSide", false);
            lastY = moveY;
        }
        else if (moveX == 0 && moveY == 0)
        {
            lastX = moveX;
            lastY = moveY;
            moveDirection = new Vector2(0, 0).normalized;
            playerAnim.SetBool("isWalking", false);
            playerRen.flipX = false;
        }

        if (moveX == 0 && moveY == 0)
        {
            _animBuffer += Time.deltaTime;
            if (_animBuffer >= _maxAmount)
            {
                playerAnim.SetBool("isWalking", false);
            }
        }
        else
        {
            _animBuffer = 0;
        }
    }

    void Move()
    {
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, 0, moveDirection.y * movementSpeed);
    }
}

