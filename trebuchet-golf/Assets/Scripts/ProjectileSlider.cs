using System.Collections;
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
    public GameObject gameoverPrefab;
    private FollowCamera followCam;

    public Button launchButton;
    public TMP_Text potentialEnergy, kineticEnergy;
    public float ballVelocity, ballAngle;

    Vector3 startPos;
    bool canLaunch = false;
    float power, totalEnergy, projPotentialEnergy, projKineticEnergy;
    float hoverDistance = 0.5f;
    Rigidbody rb;

    private float launchTime = -10;
    private bool hitCastle = false;

    // Start is called before the first frame update
    void Start()
    {
        followCam = Camera.main.GetComponent<FollowCamera>();
        this.startPos = transform.position;
        this.rb = GetComponent<Rigidbody>();

        this.ballAngle = Mathf.PI / 4.0f;

        this.canLaunch = true;
        this.launchButton.interactable = true;
        this.playerPower.interactable = true;
        this.followCam.OnTeeUp();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {

        if (this.rb.velocity.magnitude > 0)
        {
            this.launchButton.interactable = false;

            this.playerPower.interactable = false;

            this.canLaunch = false;

            //if (this.rb.velocity.magnitude < .8f)
            //{
            //    this.rb.isKinematic = true;
            //    rb.velocity = Vector3.zero;
            //    this.rb.drag = 0.1f;
            //    this.rb.angularDrag = 0.1f;
            //}

        }
        else
        {
            //this.launchButton.interactable = true;
            //this.playerPower.interactable = true;


            //if (!this.canLaunch)
            //    this.followCam.onTeeUp();

            //this.canLaunch = true;
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
        //this.rb.drag = 0.1f;
        //this.rb.angularDrag = 0.1f;
    }

    public void OnCollisionStay(Collision collision)
    {
        //this.rb.drag = 5f;
        //this.rb.angularDrag = 0.1f;
        if (collision.gameObject.tag == "Terrain" && Time.time - launchTime > 1f)
        {
            CreateGameOver();
            this.rb.isKinematic = true;
            this.rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.tag == "Castle")
        {
            hitCastle = true;
            CreateGameOver();
            this.rb.isKinematic = true;
            this.rb.velocity = Vector3.zero;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hole>())
            this.rb.velocity = Vector3.zero;
    }

    public void Launch()
    {
        this.rb.isKinematic = false;
        this.AddBallForce(this.playerPower.value);
        this.followCam.OnBallHit();
        this.totalEnergy = CalculateInitialEnergy();
        launchTime = Time.time;
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
        Vector3 direction = (this.transform.position - this.followCam.transform.position);
        direction = new Vector3(direction.x, 0, direction.z).normalized;

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
        float degree = 2f;
        this.followCam.transform.RotateAround(this.transform.position, Vector3.up, sign * degree);
    }


    private float CalculateInitialEnergy()
    {
        float energy = this.rb.mass * Physics.gravity.y * (this.playerPower.value * 10f);
        return energy;
    }

    private void CreateGameOverPopup()
    {
        GameObject gameoverPopup = Instantiate(this.gameoverPrefab);
        GameoverPopup gameOver = gameoverPopup.GetComponent<GameoverPopup>();
        Transform flag = GameObject.Find("Hole").transform;
        gameOver.Instantiate(this.transform, flag);
        if (hitCastle)
        {
            gameOver.SetText("You hit the castle! Congrats!");
        }
    }

    public void CreateGameOver()
    {
        this.CreateGameOverPopup();
    }



}
