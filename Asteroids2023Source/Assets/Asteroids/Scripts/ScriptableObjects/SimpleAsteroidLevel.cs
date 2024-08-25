using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/SimpleAsteroidsLevel")]
public class SimpleAsteroidLevel : ScriptableObject
{
    [SerializeField] private int noAsteroids;
    public int NoAsteroids => noAsteroids;
}
