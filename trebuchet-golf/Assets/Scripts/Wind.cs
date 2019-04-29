using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wind : MonoBehaviour
{
    public Vector3 windStrength;

    private Rigidbody rb;
    private static float generatedWindScale = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(windStrength);
    }

    public void SetWind(Vector3 newWind)
    {
        windStrength = newWind;
    }

    public Vector3 GetWind()
    {
        return windStrength;
    }

    public static Vector3 GenerateRandomWind()
    {
        return new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f) * generatedWindScale * 2;
    }
}
