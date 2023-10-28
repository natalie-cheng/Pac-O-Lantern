using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    // ghost prefab
    public GameObject ghostPrefab;

    // time between spawns, rate of spawn frequency
    private float spawnTime;
    private float spawnSpeed = 0.95f;

    // radius of free space needed
    private float radius = 15;

    // tracking time
    private float currentTime;

    // window dims
    public Vector2 min;
    public Vector2 max;

    // call start
    private void Start()
    {
        // initialize dims
        min = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // initialize the current times
        currentTime = Time.time;
        spawnTime = 15;
    }

    // frame update
    void Update()
    {
        // if enough spawntime has passed
        if (Time.time > currentTime)
        {
            // find a free point
            Vector2 position = FreePoint();
            // instantiate a ghost at that point
            GameObject ghost = Instantiate(ghostPrefab, position, Quaternion.identity);
            //transform.position = position;

            // add spawntime seconds
            currentTime += spawnTime;
            // reduce spawtime
            spawnTime *= spawnSpeed;
        }
    }

    // generates a free point on the screen
    private Vector2 FreePoint()
    {
        // position is a random point
        Vector2 pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        // check if collision
        bool free = Physics2D.CircleCast(pos, radius, Vector2.up, 0);
        // while there is a collision, generate a new point
        while (!free)
        {
            // new point
            pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            // check if free
            free = Physics2D.CircleCast(pos, radius, Vector2.up, 0);
        }
        // return the free point
        return pos;
    }
}
