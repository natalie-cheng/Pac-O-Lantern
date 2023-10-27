using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    /// Transform from the player object
    /// Used to find the player's position
    private Transform player;

    /// Vector from us to the player
    private Vector2 OffsetToPlayer => player.position - transform.position;

    /// Unit vector in the direction of the player, relative to us
    private Vector2 HeadingToPlayer => OffsetToPlayer.normalized;

    /// ghost movement
    public float ghostSpeed = 1;
    public float ghostAccel = 1.005f;
    private Vector2 acceleration;

    // ghost stats
    public float health = 3;
    public float opacity = 0.75f;

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
        acceleration = new Vector2(ghostAccel, ghostAccel);
    }

    // FixedUpdate is based on physics update
    private void FixedUpdate()
    {
        var offsetToPlayer = OffsetToPlayer;
        var distanceToPlayer = offsetToPlayer.magnitude;
        rb.AddForce((ghostSpeed / distanceToPlayer) * offsetToPlayer * acceleration);

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
            // play sound
            // destroy object
            Destroy(gameObject);
            // game over?
        }
        // if colliding with a fireball
        else if (collision.collider.name.Contains("Fireball"))
        {
            // call onhit
            OnHit();
        }
    }

}
