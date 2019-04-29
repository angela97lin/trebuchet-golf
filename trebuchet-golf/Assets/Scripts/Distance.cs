using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public Transform ball;
    public Transform flag;

    private Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        //scoreText.text = "Distance: ";
        textComponent = GetComponent<Text>();
    }

    void Update()
    {
        string dist = "Distance: " + Vector3.Distance(ball.position, flag.position).ToString("F1");
        textComponent.text = dist;
    }
}
