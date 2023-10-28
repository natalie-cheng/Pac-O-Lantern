using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    // in a collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if colliding with a pumpkin
        if (collision.collider.name.Contains("Pumpkin"))
        {
            // add a point to score
            UI.IncreaseScore();
            // play sound
            // destroy the candy
            Destroy(gameObject);
        }
    }
}
