using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public FollowCameraTarget target;
    [Range(0,1)]
    public float followSpeed = 0.5f;
    public bool testTargetFollow = true;

    private CameraLocation[] cameraLocations;
    private bool ballInAir = false;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
        cameraLocations = Object.FindObjectsOfType<CameraLocation>();
        if (cameraLocations.Length == 0)
        {
            Debug.LogError("Please place CameraLocation objects in the scene!");
        }
        if (testTargetFollow)
        {
            onBallHit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetRigidbody().velocity.y < 0 && ballInAir) // Only change camera position once ball begins downward arc
        {
            updateCameraPosition();
        }
        Quaternion currentRotation = transform.rotation;
        transform.LookAt(target.transform);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, followSpeed);
    }

    void updateCameraPosition()
    {
        if (cameraLocations.Length != 0)
        {
            Vector3 closestPosition = transform.position;
            float closestDistance = Vector3.Magnitude(transform.position - target.transform.position);
            foreach (CameraLocation cl in cameraLocations)
            {
                float testDistance = Vector3.Magnitude(cl.transform.position - target.transform.position);
                if (testDistance < closestDistance)
                {
                    closestDistance = testDistance;
                    closestPosition = cl.transform.position;
                }
            }
            transform.position = closestPosition;
        }
    }

    void onTeeUp()
    {
        //TODO: Get ball location and line up with hole.
        ballInAir = false;
    }

    void onBallHit()
    {
        ballInAir = true;
    }
}
