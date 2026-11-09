using UnityEngine;
using UnityEngine.InputSystem;

public class ForceTest : MonoBehaviour
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
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePostion = Mouse.current.position.ReadValue();
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePostion);
            worldMousePos.z = 0;

            Vector3 directionToApplyForce = worldMousePos - transform.position;

            rb.AddForce(directionToApplyForce.normalized * forceStrength, ForceMode2D.Force);
        }

        if (Mouse.current.rightButton.isPressed)
        {
            Vector3 mousePostion = Mouse.current.position.ReadValue();
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePostion);
            worldMousePos.z = 0;

            Vector3 directionToApplyForce = worldMousePos - transform.position;

            rb.AddForce(directionToApplyForce.normalized * forceStrength, ForceMode2D.Impulse); //Default on ForceMode2D.Force 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exit");
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Stay");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit");
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger Stay");
    }

    public enum ProjectileType
    {
        arrow, cannon, ball, dog
    }

    public void SpawnProjectile(ProjectileType type)
    {

    }
}
