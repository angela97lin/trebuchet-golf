using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 targetPos;
    public float speed = 10;
    public float arcHeight = 1;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        this.transform.LookAt(nextPos - this.transform.position);
        this.transform.position = nextPos;

    }

    void ParabolicArc()
    {
        float z0 = startPos.z;
        float z1 = targetPos.z;

        float distance = z1 - z0;

        float nextZ = Mathf.MoveTowards(this.transform.position.z, z1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(this.startPos.y, targetPos.y, (nextZ - z0) / distance);


    }

   
}
