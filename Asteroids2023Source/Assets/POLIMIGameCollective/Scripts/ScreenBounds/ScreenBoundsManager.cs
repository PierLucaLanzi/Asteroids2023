using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POLIMIGameCollective;

public class ScreenBoundsManager : MonoBehaviour
{
    private float _top;
    private float _bottom;
    private float _left;
    private float _right;

    [SerializeField] private float _margin = 2f;  
    private void Awake()
    {
        ScreenBounds.ComputeScreenBounds();
        _top = ScreenBounds.Top;
        _bottom = ScreenBounds.Bottom;
        _left = ScreenBounds.Left;
        _right = ScreenBounds.Right;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = transform.position;

        if (position.y > _top + _margin)
            position.y = _bottom - _margin;
        if (position.y < _bottom - _margin)
            position.y = _top + _margin;

        if (position.x > _right + _margin)
            position.x = _left - _margin;
        if (position.x < _left - _margin)
            position.x = _right + _margin;

        transform.position = position;
    }
}
