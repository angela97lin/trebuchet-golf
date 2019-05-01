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
    public Slider playerPower, projPotential, projKinetic;
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

    public PathPrediction pathPredictionPrefab;
    private PathPrediction currentPrediction;

    private float launchTime = -10;
    private bool hitCastle = false;
    private float animTime;

    public Animator trebuchetAnim;
    [SerializeField]
    private Transform counterweight;

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
        playerPower.onValueChanged.AddListener(delegate { OnPowerChanged(); });

    }

    // Update is called once per frame
    void Update()
    {
        this.counterweight.localScale = new Vector3(this.playerPower.value, this.playerPower.value, this.playerPower.value) * 2f;
    }


    private void FixedUpdate()
    {

        if (this.rb.velocity.magnitude > 0)
        {
            this.launchButton.interactable = false;

            this.playerPower.interactable = false;

            this.canLaunch = false;

            this.totalEnergy = Mathf.Max(this.rb.mass * 0.5f * Mathf.Pow(this.rb.velocity.y, 2), this.totalEnergy);

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
            this.projKineticEnergy = this.rb.mass * 0.5f * Mathf.Pow(this.rb.velocity.y, 2);
            this.projPotentialEnergy = this.rb.mass * Physics.gravity.y * height;

            if (this.totalEnergy > 0f)
            {
                this.projPotentialEnergy = this.totalEnergy - this.projKineticEnergy;
               
                this.projPotential.value = (this.projPotentialEnergy / this.totalEnergy) * 100f;
                this.projKinetic.value = (this.projKineticEnergy / this.totalEnergy) * 100f;
            } else
            {
                this.projPotential.value = 0f;
                this.projKinetic.value = 0f;
            }

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
            this.totalEnergy = 0f;
        }
        if (collision.gameObject.tag == "Castle")
        {
            hitCastle = true;
            CreateGameOver();
            this.rb.isKinematic = true;
            this.rb.velocity = Vector3.zero;
            this.totalEnergy = 0f;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hole>())
            this.rb.velocity = Vector3.zero;
    }

    public void Launch()
    {
        this.trebuchetAnim.SetTrigger("launched");
        /*if (this.trebuchetReady)
        {
            this.rb.isKinematic = false;
            this.AddBallForce();
            this.followCam.OnBallHit();
            //this.totalEnergy = CalculateInitialEnergy();
            launchTime = Time.time;
            this.checkLaunch.Reset();
        }*/

        this.rb.isKinematic = false;
        this.AddBallForce();
        this.followCam.OnBallHit();
        //this.totalEnergy = CalculateInitialEnergy();
        launchTime = Time.time;
        currentPrediction.DestroyIndicator();
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

    void AddBallForce()
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
        PredictPath();
    }


    private float CalculateInitialEnergy()
    {
        float energy = this.rb.mass * Mathf.Abs(Physics.gravity.y )* (this.playerPower.value);
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

    public void OnPowerChanged()
    {
        PredictPath();
    }

    public void OnCameraReset()
    {
        PredictPath();
    }

    private void PredictPath()
    {
        Vector3 launchForce = this.CalculateForceVector();
        if (currentPrediction != null)
        {
            currentPrediction.DestroyAll();
        }
        currentPrediction = Instantiate(pathPredictionPrefab, transform.position, Quaternion.identity);
        currentPrediction.SetForce(launchForce);
    }



}
