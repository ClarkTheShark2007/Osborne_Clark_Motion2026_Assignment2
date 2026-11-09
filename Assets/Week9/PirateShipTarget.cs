using UnityEngine;

public class PirateShipTarget : MonoBehaviour
{
    public ScoreboardController scoreboardController;

    void OnCollisionEnter2D(Collision2D collision)
    {
        scoreboardController.Score++; 
    }
}
