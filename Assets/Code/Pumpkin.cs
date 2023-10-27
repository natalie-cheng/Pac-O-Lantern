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

    // cool down for shooting
    public float coolDownTime = 0.3f;
    public float currentTime = 0;

    // rigidbody and adjustment for shooting out the top
    private Rigidbody2D rb;
    private Vector3 shootAdjust = new Vector3(-0.275f, 0, 0);

    // call start
    private void Start()
    {
        // initialize pumpkin vars
        rb = GetComponent<Rigidbody2D>();
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
            // instantiate ball
            GameObject ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

            // get rigidbody and shoot out from the top of the pumpkin
            Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.velocity = 10 * transform.up + shootAdjust;

            // adjust tracking time
            currentTime = Time.time;
        }
    }
}
