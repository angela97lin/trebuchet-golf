using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMarker : MonoBehaviour
{
    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindWithTag("Ball");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position.y = 241;
        this.transform.position = new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z);        
    }
}
