using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    public GameObject trailIndicatorPrefab;
    public GameObject trailMinimapPrefab;
    public GameObject emptyTransformPrefab;

    private Rigidbody rb;
    private Vector3 launchForce = Vector3.zero;
    private GameObject trailParent;
    private float initY;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
        rb = GetComponent<Rigidbody>();
        trailParent = Instantiate(emptyTransformPrefab, transform.position, Quaternion.identity);
        rb.isKinematic = false;
        rb.AddForce(launchForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stopped)
        {
            return;
        }

        if (transform.position.y < initY - 10)
        {
            stopped = true;
            Destroy(rb);
            return;
        }

        GameObject t = Instantiate(trailIndicatorPrefab, trailParent.transform);
        GameObject tMinimap = Instantiate(trailMinimapPrefab, trailParent.transform);
        t.transform.position = transform.position;
        tMinimap.transform.position = transform.position + Vector3.up * 100;
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
