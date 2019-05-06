using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookInDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
