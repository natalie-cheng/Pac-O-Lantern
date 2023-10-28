using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    // fireball prefab
    public GameObject BallPrefab;

    // speed and rotation of pumpkin
    public float pumpkinSpeed = 3;
    public float rotateSpeed = 150;

    // speed of fireball
    public float ballSpeed = 5;

    // cool down for shooting
    private float coolDownTime = 0.3f;
    private float currentTime;

    // rigidbody and adjustment for shooting out the top
    private Rigidbody2D rb;
    // start with 3 lives
    private float health = 3;
    //private float shootAdjust = 0.1f;
    private Quaternion rotateAdjust = Quaternion.Euler(0, 0, 90);

    // call start
    private void Start()
    {
        // initialize pumpkin body
        rb = GetComponent<Rigidbody2D>();
        // initialize the current time
        currentTime = Time.time;
    }

    // frame update
    private void Update()
    {
        // always moving
        Move();
        // check if shooting
        if (Input.GetAxis("Fire")==1)
        {
            Shoot();
        }
    }

    // move pumpkin around maze
    private void Move()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    rb.velocity = Vector2.right * pumpkinSpeed;
        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    rb.velocity = Vector2.left * pumpkinSpeed;
        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    rb.velocity = Vector2.up * pumpkinSpeed;
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    rb.velocity = Vector2.down * pumpkinSpeed;
        //}

        // horizontal and vertical input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // set the direction, increase by speed
        Vector2 vec = new Vector2(horizontal, vertical);
        rb.velocity = vec * pumpkinSpeed;

        // take in input rotation and adjust
        float rotate = Input.GetAxis("Rotate");
        rb.angularVelocity = rotate * rotateSpeed;
    }

    // shooting a fireball
    private void Shoot()
    {
        // if enough time has passed since last fireball shot
        if ((Time.time - currentTime) > coolDownTime)
        {
            //Debug.Log(transform.position);
            //Vector3 position = new Vector3(transform.position.x*shootAdjust, transform.position.y, transform.position.z);
            // instantiate ball
            GameObject ball = Instantiate(BallPrefab, transform.position, transform.rotation * rotateAdjust);

            // get rigidbody and shoot out from the top of the pumpkin
            Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.velocity = ballSpeed * transform.up;

            // adjust tracking time
            currentTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if colliding with a ghost
        if (collision.collider.name.Contains("Ghost"))
        {
            // lose a life
            health--;
            // adjust health
            UI.ChangeHealth(health);
        }
    }
}
