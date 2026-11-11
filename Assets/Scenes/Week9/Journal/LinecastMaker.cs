using UnityEngine;

public class LinecastMaker : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    // Update is called once per frame
    void Update()
    {
        Vector2 lineCastStart = startPoint.position;  
        Vector2 lineCastEnd = endPoint.position;  

        Debug.DrawLine(lineCastStart, lineCastEnd, Color.red);

        RaycastHit2D objectHit = Physics2D.Linecast(lineCastStart, lineCastEnd);

        if(objectHit.collider != null)
        {
            Debug.Log("Laser has hit " + objectHit.collider.gameObject.name);
        }
    }
}
