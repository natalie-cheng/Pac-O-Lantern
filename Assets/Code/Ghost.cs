using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // transform of player, finds player position
    private Transform player;

    // direction and distance of the player
    private Vector2 playerDir;
    private float playerDist;

    /// ghost movement
    private float ghostSpeed; 
    private float ghostAccel = 1.005f;
    private Vector2 acceleration;

    // ghost stats
    private float health = 3;
    private float opacity = 0.75f;

    // ghost rigidbody
    private Rigidbody2D rb;

    // sprite
    private SpriteRenderer spriteRenderer;

    // call start
    private void Start()
    {
        // find position of pumpkin
        player = FindObjectOfType<Pumpkin>().transform;
        // initialize ghost vars
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ghostSpeed = 1;
        acceleration = new Vector2(ghostAccel, ghostAccel);
    }

    // FixedUpdate is based on physics update
    private void FixedUpdate()
    {
        // check if the game is over to destroy spawner
        if (UI.isGameOver)
        {
            Destroy(gameObject);
        }
        // otherwise ghost moves
        else
        {
            // direction of the player
            playerDir = player.position - transform.position;
            // distance to player
            playerDist = playerDir.magnitude;
            rb.AddForce((ghostSpeed / playerDist) * playerDir * acceleration);

            // change the sprite orientation depending on direction
            if (rb.velocity.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    // when the ghost is hit (by a fireball)
    private void OnHit()
    {
        // subtract health
        health -= 1;
        // if dead, destroy object
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        // otherwise alive, adjust opacity
        else
        {
            opacity -= 0.25f;
            spriteRenderer.color = new Color(1, 1, 1, opacity);
        }
    }

    // in case of collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if colliding with a pumpkin
        if (collision.collider.name.Contains("Pumpkin"))
        {
            // destroy object
            Destroy(gameObject);
        }
        // if colliding with a fireball
        else if (collision.collider.name.Contains("Fireball"))
        {
            // call onhit
            OnHit();
        }
    }

}
