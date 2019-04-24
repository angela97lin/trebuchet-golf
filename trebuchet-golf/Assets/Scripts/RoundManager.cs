using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private Vector3 windStrength;
    private GameObject ball;
    private GameObject windIndicator;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindWithTag("Ball");
        windIndicator = GameObject.FindWithTag("WindIndicator");
        NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewRound()
    {
        windStrength = Wind.GenerateRandomWind();
        Debug.Log("Wind Strength : " + windStrength);
        ball.GetComponent<Wind>().SetWind(windStrength);
        windIndicator.GetComponent<WindIndicator>().SetWind(windStrength);
    }
}
