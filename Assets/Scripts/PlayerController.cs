using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Vector2 playerInput;

    [Header("Horziontal Movement")]
    public float maxSpeed;
    [SerializeField] Vector2 velocity;
    float timeToReachMaxAcceleration; 
    public float speedGainedTime;
    float timeToReachStopMoving; 
    public float speedLostTime;


    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        timeToReachMaxAcceleration = maxSpeed / speedGainedTime;
        timeToReachStopMoving = maxSpeed / speedLostTime;
    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        velocity = velocity + playerInput.normalized * timeToReachMaxAcceleration * Time.deltaTime;
        playerRb.linearVelocity = velocity;

        if(velocity.magnitude >= maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        if(playerInput == Vector2.zero)
        {
            velocity -= velocity.normalized * timeToReachStopMoving * Time.deltaTime;
            if(velocity.magnitude <= 0.1f)
            {
                velocity = Vector3.zero;
            }
        }
     }

    public bool IsWalking()
    {
        if(playerInput == Vector2.zero)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsGrounded()
    {
        return true;
    }

    private FacingDirection lastFacedDirection;
    public FacingDirection GetFacingDirection()
    {
        if(playerInput.x >= 0.1)
        {
            lastFacedDirection = FacingDirection.right;
        }
        else if (playerInput.x <= -0.1)
        {
            lastFacedDirection = FacingDirection.left;
        }

        return lastFacedDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            playerInput = context.ReadValue<Vector2>();

        }

        playerInput.y = 0f;
    }
}
