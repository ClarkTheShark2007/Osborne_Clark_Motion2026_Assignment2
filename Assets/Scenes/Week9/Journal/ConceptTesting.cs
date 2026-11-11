using UnityEngine;
using UnityEngine.InputSystem;

public class ConceptTesting : MonoBehaviour
{
    Rigidbody2D rb;
    public float forceStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Force - Contiunes, frame based acceleration (Sprint) 
    //Impulse Instant burst (Explosion, Dash)
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.qKey.isPressed)
        {
            Vector3 mousePostion = Mouse.current.position.ReadValue();
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePostion);
            worldMousePos.z = 0;

            Vector3 directionToApplyForce = worldMousePos - transform.position;

            rb.AddForce(directionToApplyForce.normalized * forceStrength, ForceMode2D.Force);
            rb.AddTorque(forceStrength, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

            if(Physics2D.GetIgnoreCollision(collision.collider, GetComponent<Collider2D>())) {
                Debug.Log("Fake Wall");
            }
        }
        if(Physics2D.GetIgnoreCollision(collision.collider, GetComponent<Collider2D>())) 
        {
            Debug.Log("Real Wall");
        }
        Debug.Log("Collision Enter");
    }
}
