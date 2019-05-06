using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRangeSlider : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        fill.color = gradient.Evaluate((slider.value - slider.minValue)/(slider.maxValue - slider.minValue));
    }
}
