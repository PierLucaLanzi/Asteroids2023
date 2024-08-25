using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AsteroidSize
{
    Small=0, Medium=1, Large=2
}

public class Asteroid : MonoBehaviour
{
    private static int _id = 0;
    public int ID { get; private set; }

    public float[] AsteroidScale = new float[3] { 0.2f, 0.5f, 1.2f };
    public float[] AsteroidSpeed = new float[3] { 5f, 4.5f, 4f };
    public float[] AsteroidRotationSpeed = new float[3] { 45f, 40f, 35f };
    public Sprite[] AsteroidSprites;
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 90f;

    private Transform tr = null;
    private SpriteRenderer spriteRenderer = null;
    private AsteroidField asteroidField = null;

    private Vector3 direction = Vector3.right;
    
    private float rotationDirection = 0f;
    private AsteroidSize asteroidSize = AsteroidSize.Large;
    private bool rotating = false;

    public AsteroidSize Size => asteroidSize;
    public Vector2 Position => tr.position;
    public Vector2 Direction => direction;
    public bool Rotating => rotating;

    public bool LookForAsteroidField = false;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        _id++;
        ID = _id;
    }

    private void OnEnable()
    {
        // if (LookForAsteroidField)
        // {
            // asteroidField = GameObject.FindObjectOfType<AsteroidField>();
            // if (asteroidField)
            // {
            //     asteroidField.AddAsteroid(this);
            // }
        // }
    }

    public void SetRotation(bool rotate)
    {
        rotating = rotate;
        
        if (rotate)
        {
            if (Random.value < 0.5f)
                rotationDirection = 1f;
            else
                rotationDirection = -1f;
        }
        else
        {
            rotationDirection = 0f;
        }
    }

    public void SetAsteroidField(AsteroidField field)
    {
        asteroidField = field;
    }
    
    public void SetRandomSprite()
    {
        if (AsteroidSprites.Length > 0 && spriteRenderer!=null)
        {
            int selectedSprite = (int) (UnityEngine.Random.value * AsteroidSprites.Length);
            spriteRenderer.sprite = AsteroidSprites[selectedSprite];
        }
    }
    
    public void EnableRotation()
    {
        SetRotation(true);
    }

    public void DisableRotation()
    {
        SetRotation(false);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = new Vector3(direction.x, direction.y, 0);
    }
    
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    
    public void SetSize(AsteroidSize asteroidAsteroidSize)
    {
        int index = (int)asteroidAsteroidSize;
        
        asteroidSize = asteroidAsteroidSize;
        
        tr.localScale = new Vector3(AsteroidScale[index], AsteroidScale[index], tr.localScale.z);
        _speed = AsteroidSpeed[index];
        _rotationSpeed = AsteroidRotationSpeed[index];
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        tr.position += _speed * Time.fixedDeltaTime * direction;

        if (rotating)
        {
            tr.Rotate(0f,0f, 
                rotationDirection*_rotationSpeed*Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Laser"))
        {
            asteroidField.AsteroidDestroyed(this);
        }
    }

}
