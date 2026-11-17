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
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        //playerInput is determined below with OnMove

        //Accerlation
        velocity = velocity + playerInput.normalized * timeToReachMaxAcceleration * Time.deltaTime;
        playerRb.linearVelocity = velocity;

        //Max Speed Capper
        if(velocity.magnitude >= maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        //Decerlateion 
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

        playerInput.y = 0f;
    }
}
