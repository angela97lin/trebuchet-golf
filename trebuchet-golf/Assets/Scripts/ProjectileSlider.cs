﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileSlider : MonoBehaviour
{

    public Vector3 targetPos;
    public float speed = 10;
    public float arcHeight = 25;
    public Slider playerPower;
    public GameObject trajectoryPrefab;

    public Button launchButton;
    public Camera cam;
    public TMP_Text potentialEnergy, kineticEnergy;
    public float ballVelocity, ballAngle;
     int resolution = 10;

    Vector3 startPos;
    bool canLaunch = false;
    float power, totalEnergy, projPotentialEnergy, projKineticEnergy, gravity;
    float hoverDistance = 0.5f;
    Rigidbody rb;
    FollowCamera followCam;
    LineRenderer lr;
    List<GameObject> allArcPoints;

    private void Awake()
    {
        this.lr = GetComponent<LineRenderer>();
        this.gravity = Mathf.Abs(Physics.gravity.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
        this.rb = GetComponent<Rigidbody>();
        this.followCam = this.cam.GetComponent<FollowCamera>();

        this.ballAngle = Mathf.PI / 4.0f;
        this.allArcPoints = new List<GameObject>();
        this.RenderArc();
    }

    // Update is called once per frame
    void Update()
    {
        //this.ParabolicArc(this.playerPower.value);
        
    }


    private void FixedUpdate()
    {

        if (this.rb.velocity.magnitude > 0)
        {
            this.launchButton.interactable = false;

            this.playerPower.interactable = false;

            this.canLaunch = false;

            if (this.rb.velocity.magnitude < .8f)
            {
                this.rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                this.rb.drag = 0.1f;
                this.rb.angularDrag = 0.1f;
            }

        }
        else
        {
            

            this.launchButton.interactable = true;
            this.playerPower.interactable = true;


            if (!this.canLaunch)
                this.followCam.onTeeUp();

            this.canLaunch = true;
        }

        RaycastHit hit;
        Ray downRay = new Ray(this.transform.position, -Vector3.up);

        if(Physics.Raycast(downRay, out hit))
        {
            float height = Mathf.Abs(hit.distance - hoverDistance);
            this.projKineticEnergy = this.rb.mass * 0.5f * Mathf.Pow(this.rb.velocity.magnitude, 2);
            this.projPotentialEnergy = -1 * this.rb.mass * Physics.gravity.y * height;


            this.potentialEnergy.text = "Potential Energy: " + this.projPotentialEnergy.ToString("F1");
            this.kineticEnergy.text = "Kinetic Energy: " + this.projKineticEnergy.ToString("F1");

        }
    }

    public void OnCollisionExit(Collision collision)
    {
        this.rb.drag = 0.1f;
        this.rb.angularDrag = 0.1f;
    }

    public void OnCollisionStay(Collision collision)
    {
        this.rb.drag = 5f;
        this.rb.angularDrag = 0.1f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hole>())
            this.rb.velocity = Vector3.zero;
    }

    public void Launch()
    {
        this.ClearArcPoints();
        this.rb.isKinematic = false;
        this.AddBallForce(this.playerPower.value);
        this.followCam.onBallHit();
        this.totalEnergy = CalculateInitialEnergy();
    }

    void ParabolicArc(float playerPower)
    {
        if (this.canLaunch)
        {
            this.targetPos = new Vector3(this.targetPos.x, this.targetPos.y, playerPower * this.targetPos.z);
            this.arcHeight = playerPower * this.arcHeight;


            Vector3 nextPos;

            float z0 = startPos.z;
            float z1 = targetPos.z;

            float distance = z1 - z0;

            float nextZ = Mathf.MoveTowards(this.transform.position.z, z1, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(this.startPos.y, targetPos.y, (nextZ - z0) / distance);
            float arc = this.arcHeight * (nextZ - z0) * (nextZ - z1) / (-0.25f * distance * distance);

            nextPos = new Vector3(transform.position.x, baseY + arc, nextZ);

            this.transform.LookAt(nextPos - transform.position);
            this.transform.position = nextPos;
        }

    }

    Vector3 CalculateForceVector()
    {
        Vector3 direction = (this.transform.position - this.cam.transform.position);
        direction = new Vector3(direction.x, 0, direction.z).normalized;

        //float step = this.speed * Time.deltaTime;

        Vector3 force = Vector3.RotateTowards(direction, Vector3.up, this.ballAngle, 0.0f) * (this.playerPower.value * 100f);

        return force;
    }

    void AddBallForce(float playerPower)
    {
        if (this.canLaunch)
        {

            Vector3 force = this.CalculateForceVector();

            this.rb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void Rotate(float sign)
    {
        this.ClearArcPoints();
        float degree = 2f;
        this.cam.transform.RotateAround(this.transform.position, Vector3.up, sign * degree);
    }


    private float CalculateInitialEnergy()
    {
        float energy = this.rb.mass * Physics.gravity.y * (this.playerPower.value * 10f);
        return energy;
    }

    void RenderArc()
    {
        Vector3 launchForce = this.CalculateForceVector();

        this.ballVelocity = ((launchForce / this.rb.mass) * Time.fixedDeltaTime).magnitude;
        Vector3[] trajectoryPoints = this.CalculateArcArray();

        for(int i = 0; i < trajectoryPoints.Length; i++)
        {
            GameObject projPoint = Instantiate(this.trajectoryPrefab, this.transform);
        }
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[this.resolution + 1];

        float maxDistance = (Mathf.Pow(this.ballVelocity, 2) * Mathf.Sin(2 * this.ballAngle)) / this.gravity;

        for (int i = 0; i <= this.resolution; i++)
        {
            float t = (float)i / (float)this.resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;

    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float z = t * maxDistance;
        float y = z * Mathf.Tan(this.ballAngle) - ((this.gravity * Mathf.Pow(z,2)/(2 * Mathf.Pow(this.ballVelocity,2) * Mathf.Pow(Mathf.Cos(this.ballAngle),2))));
        return new Vector3(z, y);

    }

    void ClearArcPoints()
    {
        for(int i = this.allArcPoints.Capacity; i >=0; i--)
        {
            Destroy(this.allArcPoints[i]);
        }
    }





}
