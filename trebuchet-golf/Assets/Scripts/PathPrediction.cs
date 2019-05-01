using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    public GameObject trailIndicatorPrefab;
    public GameObject trailMinimapPrefab;
    public GameObject indicatorContainerPrefab;
    public GameObject minimapContainerPrefab;

    private Rigidbody rb;
    private Vector3 launchForce = Vector3.zero;
    private GameObject trailIndicatorParent;
    private GameObject trailMinimapParent;
    private float initY;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
        rb = GetComponent<Rigidbody>();
        trailIndicatorParent = Instantiate(indicatorContainerPrefab, transform.position, Quaternion.identity);
        trailMinimapParent = Instantiate(minimapContainerPrefab, transform.position, Quaternion.identity);
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

        GameObject t = Instantiate(trailIndicatorPrefab, trailIndicatorParent.transform);
        GameObject tMinimap = Instantiate(trailMinimapPrefab, trailMinimapParent.transform);
        t.transform.position = transform.position;
        tMinimap.transform.position = transform.position + Vector3.up * 100;
    }

    public void SetForce(Vector3 force)
    {
        launchForce = force;
    }

    public void DestroyIndicator()
    {
        trailIndicatorParent.SetActive(false);
    }

    public void DestroyAll()
    {
        Destroy(trailIndicatorParent);
        Destroy(trailMinimapParent);
        Destroy(this.gameObject);
    }
}
