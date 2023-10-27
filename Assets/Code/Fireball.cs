using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // if the fireball goes off the screen
    private void OnBecameInvisible()
    {
        // destroy object
        Destroy(gameObject);
    }

    // if it collides with anything (ghost)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy object
        Destroy(gameObject);
    }
}
