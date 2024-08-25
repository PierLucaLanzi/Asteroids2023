using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDisable : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;

    private Coroutine disable;
    
    private void OnEnable()
    {
        // with the object pool we just disable it
        disable = StartCoroutine(Disable(lifetime));
    }

    IEnumerator Disable(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        // when the game object is disabled, 
        // we stop the coroutines it created 
        if (disable is not null)
            StopCoroutine(disable);
    }
}