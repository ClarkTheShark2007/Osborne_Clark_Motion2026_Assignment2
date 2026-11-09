using Unity.VisualScripting;
using UnityEngine;

public class CannonballController : MonoBehaviour
{
    public float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        //After lifespan seconds, this object will be destroyed
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.name == "Cannon Ball")
        //{
        //  Destroy(collision.gameObject);
        // Destroy(gameObject);
        //}

        //if (collision.collider.CompareTag("CannonBall"))
        //{
        //    Destroy(collision.gameObject);
        //    Destroy(gameObject);
        //}

        if (collision.collider.GetComponent<CannonballController>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
