using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    public ProjectileSlider ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ball.Rotate(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ball.Rotate(1);

        }
    }
    
}
