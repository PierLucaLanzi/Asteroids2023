using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Transform tr;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 3f;

    private Coroutine disable;
    
    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        // originally we would destroy the object
        // Destroy(gameObject,lifetime);    
        
        // with the object pool we just disable it
        disable = StartCoroutine(Disable(lifetime));
        
        // note that originally, we did this in the start function
        // which is only activated once so in this case, we need
        // to do it in the OnEnable method
    }

    IEnumerator Disable(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        tr.position += speed * Time.deltaTime * tr.up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid") || other.CompareTag("Saucer"))
        {
            // Destroy(gameObject);
            
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // when the game object is disabled, 
        // we stop the coroutines it created 
        if (disable is not null)
            StopCoroutine(disable);
    }
}
