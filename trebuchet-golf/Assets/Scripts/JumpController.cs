using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    public float jumpSpeed = 10f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
        if (Input.GetKeyDown("left"))
        {
            rb.velocity += new Vector3(-jumpSpeed, 0, 0);
        }
        if (Input.GetKeyDown("right"))
        {
            rb.velocity += new Vector3(jumpSpeed, 0, 0);
        }
        if (Input.GetKeyDown("up"))
        {
            rb.velocity += new Vector3(0, 0, jumpSpeed);
        }
        if (Input.GetKeyDown("down"))
        {
            rb.velocity += new Vector3(0, 0, -jumpSpeed);
        }
    }
}
