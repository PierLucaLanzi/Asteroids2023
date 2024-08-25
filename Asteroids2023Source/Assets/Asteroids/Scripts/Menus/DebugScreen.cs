using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DebugScreen : Singleton<DebugScreen>
{
    public enum Position
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight,
        Center
    }

    [SerializeField] private TextMeshProUGUI upperLeft;
    [SerializeField] private TextMeshProUGUI upperRight;
    [SerializeField] private TextMeshProUGUI lowerLeft;
    [SerializeField] private TextMeshProUGUI lowerRight;
    [SerializeField] private TextMeshProUGUI center;

    private void Start()
    {
        upperLeft.text = "";
        upperRight.text = "";
        lowerLeft.text = "";
        lowerRight.text = "";
        center.text = "";
    }

    public void Show(string text, Position position = Position.UpperRight)
    {
        switch (position)
        {
            case Position.UpperLeft:
                upperLeft.text = text;
                break;
            case Position.UpperRight:
                upperRight.text = text;
                break;
            case Position.LowerLeft:
                lowerLeft.text = text;
                break;
            case Position.LowerRight:
                lowerRight.text = text;
                break;
            case Position.Center:
                center.text = text;
                break;
        }
    }
}
