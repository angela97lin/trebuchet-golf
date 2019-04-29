using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public Vector2 wind = new Vector2(0,0);
    public bool randomizeWind = true;
    private Vector3 windStrength;
    private GameObject ball;
    private GameObject windIndicator;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindWithTag("Ball");
        windIndicator = GameObject.FindWithTag("WindIndicator");
        windStrength = new Vector3(wind.x, 0, wind.y);
        NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewRound()
    {
        if (randomizeWind)
        {
            windStrength = Wind.GenerateRandomWind();
        }
        ball.GetComponent<Wind>().SetWind(windStrength);
        windIndicator.GetComponent<WindIndicator>().SetWind(windStrength);
    }
}
