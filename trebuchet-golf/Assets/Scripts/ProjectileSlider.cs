using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileSlider : MonoBehaviour
{

    public Vector3 targetPos;
    public float speed = 10;
    public float arcHeight = 25;
    public Slider playerPower;

    public Button launchButton;
    public Camera cam;

    Vector3 startPos;
    bool canLaunch = false;
    float power;
    Rigidbody rb;
    FollowCamera followCam;

    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
        this.rb = GetComponent<Rigidbody>();
        this.followCam = this.cam.GetComponent<FollowCamera>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void FixedUpdate()
    {
        if(this.rb.velocity.magnitude > 0)
        {
            this.launchButton.interactable = false;

            this.playerPower.interactable = false;

            this.canLaunch = false;


        }
        else
        {
            this.launchButton.interactable = true;
            this.playerPower.interactable = true;


            if (!this.canLaunch)
                this.followCam.onTeeUp();

            this.canLaunch = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        this.rb.drag = 0.1f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.rb.drag = 2f;
    }

    public void Launch()
    {
        this.AddBallForce(this.playerPower.value);
        this.followCam.onBallHit();
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

    void AddBallForce(float playerPower)
    {
        if (this.canLaunch)
        {
            Vector3 direction = (this.transform.position - this.cam.transform.position).normalized;

            

            float step = this.speed * Time.deltaTime;

            Vector3 force = Vector3.RotateTowards(direction, transform.up, Mathf.PI / 2, 0.0f) * (playerPower * 100f);

            this.rb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void Rotate(float sign)
    {
        float degree = 2f;
        this.cam.transform.RotateAround(this.transform.position, Vector3.up, sign * degree);
    }


}
