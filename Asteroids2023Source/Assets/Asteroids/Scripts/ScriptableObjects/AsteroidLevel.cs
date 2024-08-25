using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Level")]
public class AsteroidLevel : ScriptableObject
{
    [SerializeField] private int numberOfLargeAsteroids;
    [SerializeField] private int numberOfBigSaucers;
    [SerializeField] private int numberOfSmallSaucers;

    [SerializeField] private float largeAsteroidSpeedModifier = 1.0f;
    [SerializeField] private float mediumAsteroidSpeedModifier = 1.0f;
    [SerializeField] private float smallAsteroidSpeedModifier = 1.0f;

    [Range(0.25f,0.9f)]
    [SerializeField] private float bigSaucerThreshold = .5f;
    
    [Range(0.25f,0.9f)]
    [SerializeField] private float smallSaucerThreshold = .5f;
    
    #region Properties
    public int NumberOfLargeAsteroids => numberOfLargeAsteroids;
    public int NumberOfBigSaucer => numberOfBigSaucers;
    public int NumberOfSmallSaucer => numberOfSmallSaucers;

    public float LargeAsteroidSpeedModifier => largeAsteroidSpeedModifier; 
    public float MediumAsteroidSpeedModifier => mediumAsteroidSpeedModifier;
    public float SmallAsteroidSpeedModifier => smallAsteroidSpeedModifier;

    public float BigSaucerThreshold => bigSaucerThreshold;
    public float SmallSaucerThreshold => smallSaucerThreshold;
    #endregion
}
