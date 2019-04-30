using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMarker : MonoBehaviour
{
    public GameObject pathMarker;

    GameObject ball;
    float lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindWithTag("Ball");
        lastSpawn = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z);
    }

    void Update() 
    {
        // Spawn marker balls every second
        if (Time.time - lastSpawn > 2) 
        {
            Vector3 cloneLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject clone = Instantiate(pathMarker, cloneLocation, transform.rotation) as GameObject;
            clone.transform.parent = GameObject.Find("BallTrajectory").transform;
        }
    }
}
