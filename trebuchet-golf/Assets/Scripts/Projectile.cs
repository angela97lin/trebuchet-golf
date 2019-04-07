using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{

    public Vector3 targetPos;
    public float speed = 10;
    public float arcHeight = 25;

    Vector3 startPos;
    [SerializeField]
    float playerPower = 0.0f;
    bool canLaunch = false;
    private Rigidbody rb;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
        this.rb = GetComponent<Rigidbody>();
        this.cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (this.playerPower < 1.0f)
                this.playerPower += 0.1f;
            this.canLaunch = true;
        }
        else if (Input.GetMouseButtonUp(0))
            this.AddBallForce(this.playerPower);
           //this.ParabolicArc(this.playerPower);

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

            Vector3 force = Vector3.RotateTowards(direction, transform.up, Mathf.PI / 4.0f, 0.0f) * (playerPower * 10f);

            this.rb.AddForce(force, ForceMode.Impulse);
        }


    }


}
