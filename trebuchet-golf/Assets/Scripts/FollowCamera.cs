﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FollowCamera : MonoBehaviour
{
    public FollowCameraTarget target;
    [Range(0,1)]
    public float followSpeed = 0.5f;
    public float teeUpOffsetHeight = 1;
    public float teeUpOffsetDistance = 2;
    public bool testTargetFollow = true;
 

    private CameraLocation[] cameraLocations;
    private Hole hole;
    private bool ballInAir = false;



    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
        cameraLocations = Object.FindObjectsOfType<CameraLocation>();
        hole = Object.FindObjectOfType<Hole>();
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
        if (ballInAir)
        {
            updateCameraRotation();
        }
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

    void updateCameraRotation()
    {
        Quaternion currentRotation = transform.rotation;
        transform.LookAt(target.transform);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, followSpeed);
    }

    public void onTeeUp()
    {
        this.LineCameraWithHole();
        ballInAir = false;
    }

    public void onBallHit()
    {
        ballInAir = true;
    }

    public void LineCameraWithHole()
    {
        Vector3 directionToHole = hole.gameObject.transform.position - target.gameObject.transform.position;
        directionToHole.y = 0;
        directionToHole.Normalize();
        Vector3 offset = -directionToHole * teeUpOffsetDistance;
        offset.y = teeUpOffsetHeight;
        transform.position = offset + target.gameObject.transform.position;
        transform.LookAt(hole.transform);
    }
}
