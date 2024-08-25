using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardTarget : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    
    private bool followingWithY = false;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        target.z = 0f;

        Vector3 difference = (target - transform.position) ;
		
        float rotation = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg - (followingWithY?90f:0f);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        
    }
}
