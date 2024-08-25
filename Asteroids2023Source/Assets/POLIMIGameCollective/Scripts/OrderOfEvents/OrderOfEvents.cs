using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderOfEvents : MonoBehaviour
{
    bool firstUpdate = true;
    public string objectName;

    private void Awake()
    {
        Debug.LogFormat("{0} - Awake", objectName);
    }

    private void OnEnable()
    {
        Debug.LogFormat("{0} - OnEnable", objectName);
    }
    // Start is called before the first frame update
    private void Start()
    {
        Debug.LogFormat("{0} - Start",objectName);
    }

    // Update is called once per frame
    private void Update()
    {
        if(firstUpdate == true)
        {
            firstUpdate = false;
            Debug.LogFormat("{0} - First Update", objectName);
        }
    }

    private void OnDisable()
    {
        Debug.LogFormat("{0} - OnDisable", objectName);
    }

    private void OnDestroy()
    {
        Debug.LogFormat("{0} - Destroyed", objectName);
    }
}
