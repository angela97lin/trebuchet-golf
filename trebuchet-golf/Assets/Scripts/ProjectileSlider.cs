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
    Camera cam;

    Vector3 startPos;
    bool canLaunch = false;
    float power;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
        this.cam = Camera.main;
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerPower.value > 0)
        {
            this.canLaunch = true;
            //this.launchButton.onClick.AddListener(() => ParabolicArc(this.playerPower.value));
        }
            
    }

    public void Launch()
    {
        /*while(this.transform.position != this.targetPos)
            this.ParabolicArc(this.playerPower.value);*/
        this.AddBallForce(this.playerPower.value);
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

            Vector3 force = Vector3.RotateTowards(direction, transform.up, Mathf.PI / 4.0f, 0.0f) * playerPower;

            this.rb.AddForce(force, ForceMode.Impulse);
        }


    }


}
