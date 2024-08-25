using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Asteroids/Asteroid Field Parameters")]
public class AsteroidFieldParameters : ScriptableObject
{
    [Header("Large Asteroid")] 
    public float LargeAsteroidSpeed = 3f;
    public float LargeAsteroidScale = 1f;

    [Header("Medium Asteroid")] 
    public float MediumAsteroidSpeed = 3f;
    public float MediumAsteroidScale = 1f;

    [Header("Small Asteroid")] 
    public float SmallAsteroidSpeed = 3f;
    public float SmallAsteroidScale = 1f;

    [Header("SpaceShip Parameters")] 
    public float thrustForce = 1f;
    public float rotationSpeed = 45f;
    public float coolDownTime = 5f;
    public float blinkingSpeed = 3f;

    [Header("Missile")]
    [SerializeField] private float missileSpeed = 5;
    [SerializeField] private float missileLifeTime = 3;

    public float MissileSpeed
    {
        get { return missileSpeed; }
    }

    public float MissileLifetime
    {
        get { return missileLifeTime; }
    }
}
