using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindIndicator : MonoBehaviour
{
    public TMPro.TextMeshPro textMesh;
    public Material arrowMaterial;
    public Gradient windStrengthGradient;
    private Vector3 windDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 flattenedCameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        float zSetting = Vector3.Angle(flattenedCameraForward, windDirection);
        float sign = Vector3.Cross(flattenedCameraForward, windDirection).y / Mathf.Abs(Vector3.Cross(flattenedCameraForward, windDirection).y);
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 180 - sign * zSetting);
    }

    public void SetWind(Vector3 wind)
    {
        windDirection = wind;
        textMesh.text = "Wind speed: " + ((float)((int)(windDirection.magnitude * 100)) / 100f).ToString();

        // Change material color of wind indicator
        float strength = (float)windDirection.magnitude / Mathf.Sqrt(200);
        Color indicatorColor = windStrengthGradient.Evaluate(strength);
        arrowMaterial.color = indicatorColor;
    }
}
