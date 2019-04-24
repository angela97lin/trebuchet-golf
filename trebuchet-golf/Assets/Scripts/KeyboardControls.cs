using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileSlider))]
public class KeyboardControls : MonoBehaviour
{
    private ProjectileSlider ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<ProjectileSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ball.Rotate(-1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ball.Rotate(1);

        }
    }
    
}
