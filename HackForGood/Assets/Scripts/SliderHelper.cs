﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHelper : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    public void SetVerticalNormalizedPosition(float value)
    {
        scrollRect.verticalNormalizedPosition = 1 - value;
    }

    public void SetSliderValue(Vector2 values)
    {
        GetComponent<Slider>().value = 1 - scrollRect.verticalNormalizedPosition;
    }
}
