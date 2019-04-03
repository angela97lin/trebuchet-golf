using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for marking valid locations for the camera to be when following the ball.
public class CameraLocation : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
