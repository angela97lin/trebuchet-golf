using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PathPrediction : MonoBehaviour
{
    public GameObject trailIndicatorPrefab;
    public GameObject emptyTransformPrefab;

    private Rigidbody rb;

    private Vector3 launchForce = Vector3.zero;

    private GameObject trailParent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailParent = Instantiate(emptyTransformPrefab, transform.position, Quaternion.identity);
        rb.isKinematic = false;
        rb.AddForce(launchForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject t = Instantiate(trailIndicatorPrefab, trailParent.transform);
        t.transform.position = transform.position;
    }

    public void SetForce(Vector3 force)
    {
        launchForce = force;
    }

    public void DestroyAll()
    {
        Destroy(trailParent);
        Destroy(this.gameObject);
    }
}
