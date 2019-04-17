using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleArrow : MonoBehaviour
{
    public Transform holeArrowPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 holeDirection = holeArrowPosition.position - Camera.main.transform.position;
        Vector3 cameraDirection = Camera.main.transform.forward;
        if (Vector3.Dot(holeDirection, cameraDirection) > 0)
        {
            transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, holeArrowPosition.transform.position);
        }
        else
        {
            transform.position = new Vector3(100000, 100000, 0);
        }
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 2, 0);
    }
}
