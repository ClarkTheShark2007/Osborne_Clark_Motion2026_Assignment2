using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Vector2 playerInput;

    //Everything is seperated by headers to make it easier to see in the editor
    [Header("Horziontal Movement")]
    public float maxSpeed;
    [SerializeField] Vector2 velocity;
    float timeToReachMaxAcceleration; 
    public float speedGainedTime;
    float timeToReachStopMoving; 
    public float speedLostTime;
    
    [field: Header("Vertical Movement")]
    public LayerMask groundLayer;
    public float distanceFromGround;
    [SerializeField] Vector2 jumpVelocity;
    public float maxJumpHeight;
    public float jumpedGainedOverTime;
    float gravity;
    public float initialJumpVelocity;
    bool wasJumpTriggered;


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

        gravity = -2 * maxJumpHeight / (jumpedGainedOverTime * jumpedGainedOverTime);
        initialJumpVelocity = 2 * maxJumpHeight / jumpedGainedOverTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementUpdate(playerInput);

    }

    //private void MovementUpdate(Vector2 playerInput)
    //{
    //    //playerInput is determined below with OnMove
    //    velocity = playerRb.linearVelocity;

    //    //Accerlation
    //    velocity.x = velocity.x + playerInput.normalized.x * timeToReachMaxAcceleration * Time.deltaTime;

    //    //velocity.y = Mathf.Clamp(velocity.y, -4f, initialJumpVelocity);

    //    //Decerlateion 
    //    if (playerInput == Vector2.zero)
    //    {
    //        velocity.x -= velocity.normalized.x * timeToReachStopMoving * Time.deltaTime;
    //        if(velocity.magnitude <= 0.1f)
    //        {
    //            velocity.x = 0f;
    //        }
    //    }

    //    if (!IsGrounded()) 
    //    {
    //        velocity.y += gravity * Time.deltaTime;
    //    }
    //    else
    //    {
    //        //velocity.y = 0f;
    //    }

    //    //Max Speed Capp
    //    velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

    //    playerRb.linearVelocityX = velocity.x;
    //    playerRb.linearVelocityY = velocity.y;
    //}

    private void MovementUpdate(Vector2 playerInput)
    {
        velocity.y = playerRb.linearVelocity.y;
        //acceleration
        velocity.x += playerInput.normalized.x * timeToReachMaxAcceleration * Time.deltaTime;


        //Max Speed Capp
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        if (playerInput == Vector2.zero)
        {
            velocity.x -= velocity.normalized.x * timeToReachStopMoving * Time.deltaTime;
            //making sure we decelerate properly
            if (velocity.x < 0.1f && velocity.x > -0.1f)
            {
                velocity.x = 0f;
            }
        }

        //apply gravity
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (wasJumpTriggered)
        {
            velocity.y = initialJumpVelocity;
            wasJumpTriggered = false;
        }

        //moving the rigid body
        playerRb.linearVelocity = velocity;
    }


    public bool IsWalking()
    {
        //Animator bool to see if the player is Walking
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
        //To see if the player can jump or not, also determines animation. Shoots a raycast detecting for any layer considered ground
        return Physics2D.Raycast(transform.position, Vector2.down, distanceFromGround, groundLayer);
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

        //Gets previous facing direction, so the player dosnt always face a certain after moving is completed
        return lastFacedDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //See InputAction Map for controls. Works with controllers 
        if(context.performed)
        {
            playerInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            playerInput = context.ReadValue<Vector2>();

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //See InputAction Map for controls. Works with controllers 
        if (context.performed && IsGrounded())
        {

            //Apply gravity that is calcuted over time
            //If player provides input for a jump, give them the intial jump velocity 

            Debug.Log("Jumping");
            wasJumpTriggered = true;

            //playerRb.linearVelocity = new Vector2 (0f, initialJumpVelocity);
        }
    }

    //Put it in Update and the character is jsut floating,
    //Gravity not working when removed
    //Gravity works, number is just too low
}
