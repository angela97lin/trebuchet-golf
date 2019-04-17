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
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, holeArrowPosition.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 2, 0);
    }
}
