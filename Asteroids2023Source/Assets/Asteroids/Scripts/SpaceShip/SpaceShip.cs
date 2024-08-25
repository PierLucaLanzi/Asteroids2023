using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180;

    [Space(10)]
    [Header("Thrust")]
    [SerializeField] private GameObject thrustSprite;
    [SerializeField] private float thrustForce = 5;

    [Space(10)]
    [Header("Firing System")]
    [SerializeField] private Transform firingTransform;
    [SerializeField] private Laser laserPrefab;
    [SerializeField] private GameObject laserGameObject;
    [Space(10)]
    [Header("Explosion")]
    [SerializeField] private SpaceShipExplosion ExplosionPrefab;

    [Space(10)]
    [Header("Blinking Sprites")]
    public  SpriteRenderer[] spriteRenderers;

    private Rigidbody2D rb;
    private Transform tr;
    private SpriteRenderer spriteRenderer;
    
    private bool thrust = false;
    public bool Indestructable { get; private set; } = false;

    private float rotation = 0;
    
    private void Awake()
    {
        // create 50 lasers objects if it needs more it
        ObjectPoolingManager.Instance.CreatePool (laserGameObject, 50, 50);
        
        GetComponents();
    }
    
    // Update is called once per frame
    void Update()
    {
        thrust = false;

        rotation = Input.GetAxisRaw("Horizontal");
        
        // fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // thrust
        if (Input.GetKey(KeyCode.UpArrow))
        {
            thrust = true;
            // AudioManager.Instance.PlaySpaceshipThrust();
        }
        
        thrustSprite.SetActive(thrust);

        // hyperspace
        if (Input.GetKey(KeyCode.I))
        {
            EnableIndestructible();
        }

        // hyperspace
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
        }
        
    }

    private void Fire()
    {
        // Laser laser = Instantiate(laserPrefab, firingTransform.position, firingTransform.rotation);
        GameObject go = ObjectPoolingManager.Instance.GetObject (laserGameObject.name);
        go.transform.position = firingTransform.position;
        go.transform.rotation = firingTransform.rotation;
        
        if (AudioManager.Instance is not null)
            AudioManager.Instance.PlaySpaceshipFire();
    }
    
    private void FixedUpdate()
    {
        if (thrust)
        {
            rb.AddForce(thrustForce*tr.up, 
                ForceMode2D.Force);
        }
        
        rb.MoveRotation(rb.rotation - rotation*rotationSpeed*Time.fixedDeltaTime);
    }

    private void GetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Thrust thrustComponent = GetComponentInChildren<Thrust>();

        if (thrustComponent != null)
        {
            thrust = thrustComponent.gameObject;
        }

        FiringPosition firingPositionComponent = GetComponentInChildren<FiringPosition>();
        if (firingPositionComponent != null)
        {
            firingTransform = firingPositionComponent.gameObject.transform;
        }

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

    }

    public void ResetPosition()
    {
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Indestructable)
            return;
        
        if (other.CompareTag("Laser") || other.CompareTag("Asteroid") || other.CompareTag("Saucer"))
        {
            
            // TODO
            // - instantiate the explosion
            Instantiate(ExplosionPrefab, tr.position, Quaternion.identity);
            // - play the explosion sound
            if (AudioManager.Instance is not null)
            {
                AudioManager.Instance.PlaySpaceshipExplosion();
            }
            // - tell the game manager
            GameManager.Instance.SpaceShipDestroyed();
        }
    }

    public void EnableIndestructible()
    {
        StartCoroutine(IndestructibleCoroutine());
    }

    IEnumerator IndestructibleCoroutine()
    {
        Indestructable = true;
        yield return SpriteEffects.BlinkCoroutine(spriteRenderers, 10f, 0.75f, 0.25f, 0.95f);
        
        Indestructable = false;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public Vector3 EstimatedPosition()
    {
        return tr.position + (Vector3) rb.velocity;
    }
}
