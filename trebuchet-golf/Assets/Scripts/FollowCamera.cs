using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    [Range(0,1)]
    public float followSpeed = 0.5f;

    private CameraLocation[] cameraLocations;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
        cameraLocations = Object.FindObjectsOfType<CameraLocation>();
        if (cameraLocations.Length == 0)
        {
            Debug.LogError("Please place CameraLocation objects in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateCameraPosition();
        Quaternion currentRotation = transform.rotation;
        transform.LookAt(target);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, followSpeed);
    }

    void updateCameraPosition()
    {
        if (Input.GetKeyDown("r"))
        {
            MoveNearTarget();
        }
        if (cameraLocations.Length != 0)
        {
            Vector3 closestPosition = transform.position;
            float closestDistance = Vector3.Magnitude(transform.position - target.position);
            foreach (CameraLocation cl in cameraLocations)
            {
                float testDistance = Vector3.Magnitude(cl.transform.position - target.position);
                if (testDistance < closestDistance)
                {
                    closestDistance = testDistance;
                    closestPosition = cl.transform.position;
                }
            }
            transform.position = closestPosition;
        }
    }

    public void MoveNearTarget()
    {
        transform.position = target.position + new Vector3(1f,1f,1f);
    }
}
