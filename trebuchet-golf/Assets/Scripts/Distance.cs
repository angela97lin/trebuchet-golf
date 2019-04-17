using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public Transform ball;
    public Transform flag;

    //public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //scoreText.text = "Distance: ";
    }

    private Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        string dist = "Distance: " + Vector3.Distance(ball.position, flag.position).ToString("F1");
        textComponent.text = dist;
    }
}
