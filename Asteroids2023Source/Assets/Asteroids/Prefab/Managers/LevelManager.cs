using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SimpleAsteroidLevel[] simpleAsteroidLevels;

    public SimpleAsteroidLevel GetLevel(int n)
    {
        if (n < simpleAsteroidLevels.Length && n>=0)
        {
            return simpleAsteroidLevels[n];
        }
        else
        {
            return simpleAsteroidLevels[^1];
        }
    }
}
