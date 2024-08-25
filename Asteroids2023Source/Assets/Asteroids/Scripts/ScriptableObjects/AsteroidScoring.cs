using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/ScoringSettings")]
public class AsteroidScoring : ScriptableObject
{
    [SerializeField] private int largeAsteroidScore = 20;
    [SerializeField] private int mediumAsteroidScore = 50;
    [SerializeField] private int smallAsteroidScore = 100;
    [SerializeField] private int largeSaucerScore = 200;
    [SerializeField] private int smallSaucerScore = 1000;

    [SerializeField] private int smallSaucerAppearsAfter = 40000;
    [SerializeField] private int extraShipAfter = 10000;

    public int SmallSaucerAppearsAfter => smallSaucerAppearsAfter;

    public int ExtraShipAfter => extraShipAfter;
    
    public int GetPoints(AsteroidSize size)
    {
        switch (size)
        {
            case AsteroidSize.Large:
                return largeAsteroidScore;
            case AsteroidSize.Medium:
                return mediumAsteroidScore;
            case AsteroidSize.Small:
            default:
                return smallAsteroidScore;
            // default:
            //     return 0;
        }
    }
}
