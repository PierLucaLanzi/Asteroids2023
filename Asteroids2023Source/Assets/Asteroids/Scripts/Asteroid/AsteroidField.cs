using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POLIMIGameCollective;
using Unity.Mathematics;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AsteroidField : MonoBehaviour
{
    // public bool TestGenerationFromCenter = false;
    // public bool GenerationFromBelt = true;

    private bool rotation = false;
    private bool randomSprites = false;
    private float shorterSide = 0f;
    private int noAsteroids = 0;
    private List<Asteroid> asteroidField = new List<Asteroid>();
    
    [Header("Asteroids")]
    [SerializeField] private GameObject asteroidPrefab;
    
    [SerializeField] private GameObject asteroidExplosionPrefab;
    [SerializeField] private float asteroidBeltSize = 2f;
    [SerializeField] private float targetAreaRange = .3f;
    [SerializeField] private Interval timeBetweenAsteroids;
    [SerializeField] private int noSplits = 2;
    
    // private AsteroidLevel currentLevel;
    // private int numberOfSmallSaucers = 0;
    // private int numberOfLargeSaucers = 0;

    // private int maxNumberOfAsteroids = 0;
    // private int numberOfDestroyedAsteroids = 0;
    // private float smallSaucersThreshold = 0f;
    // private float largeSaucersThreshold = 0f;
    
    private Coroutine asteroidFieldGenerationCoroutine = null;

    private void Awake()
    {
        // create 50 asteroids objects if it needs more it adds it to the pool
        ObjectPoolingManager.Instance.CreatePool (asteroidPrefab, 50, 100);
        ObjectPoolingManager.Instance.CreatePool (asteroidExplosionPrefab, 50, 100);
    }

    private void Start()
    {
        ScreenBounds.ComputeScreenBounds();
        shorterSide = Mathf.Min(ScreenBounds.Width, ScreenBounds.Height);
    }

    Asteroid GetAsteroid(Vector3 position, Quaternion rotation)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject (asteroidPrefab.name);
        go.transform.position = position;
        go.transform.rotation = rotation;
        Asteroid asteroid = go.GetComponent<Asteroid>();
        return asteroid;
    }
    #region GamePlay
    
    Asteroid SplitAsteroid(Asteroid asteroid)
    {
        Vector3 position = asteroid.Position;
        Vector3 direction = asteroid.Direction;

        // original instantiation
        Asteroid asteroidSplit = GetAsteroid(position, Quaternion.identity);

        // generate a random direction, we could use the original direction to generate the asteroid dimensions
        Vector3 splitDirection =  Random.insideUnitCircle.normalized;
        
        asteroidSplit.SetDirection(splitDirection);
        asteroidSplit.SetAsteroidField(this);

        if (asteroid.Size==AsteroidSize.Large)
            asteroidSplit.SetSize(AsteroidSize.Medium);
        else if (asteroid.Size==AsteroidSize.Medium)
            asteroidSplit.SetSize(AsteroidSize.Small);
        return asteroidSplit;
    }
    
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        // print("ASTEROID FIELD CONTAINS "+asteroidField.Count+" ASTEROIDS");
        if (asteroid.Size == AsteroidSize.Large || asteroid.Size == AsteroidSize.Medium)
        {
            for (int i = 0; i < noSplits; i++)
            {
                Asteroid splitAsteroid = SplitAsteroid(asteroid);
                asteroidField.Add(splitAsteroid);
                noAsteroids++;
            }
        }
        
        // TODO 
        // - Play explosion effect
        // Instantiate(asteroidExplosionPrefab, asteroid.Position, Quaternion.identity);
        GameObject go = ObjectPoolingManager.Instance.GetObject (asteroidExplosionPrefab.name);
        go.transform.position = asteroid.Position;
        go.transform.rotation = Quaternion.identity;

        // destroy the asteroid
        asteroidField.Remove(asteroid);
        noAsteroids--;
        
        
        // Destroy(asteroid.gameObject);
        asteroid.gameObject.SetActive(false);
        
        // update the game state
        // TODO
        // - tell the game manager that an asteroid was destroyed
        // - if there are no more asteroids the game manager should load the next level
        GameManager.Instance.AsteroidDestroyed(asteroid.Size);
        
        if (noAsteroids == 0)
        {
            // TODO
            GameManager.Instance.NextLevel();
        }
    }
    
    #endregion
    
    #region CreateAsteroidField
    
    public void CreateAsteroidField(int n)
    {
        asteroidFieldGenerationCoroutine = StartCoroutine(GenerateAsteroidFieldCoroutine(n));
    }

    public void ClearAsteroidField()
    {
        // delete the remaining asteroids
        for (int i = 0; i < asteroidField.Count; i++)
        {
            // Destroy(asteroidField[i].gameObject);
            asteroidField[i].gameObject.SetActive(false);
        }
        asteroidField.Clear();
    }
    
    private IEnumerator GenerateAsteroidFieldCoroutine(int n)
    {
        asteroidField.Clear();
        
        for (int i = 0; i < n; i++)
        {
            yield return new WaitForSeconds(Random.Range(timeBetweenAsteroids.Lower,timeBetweenAsteroids.Upper));
            Asteroid asteroid = CreateAsteroid(AsteroidSize.Large);
            asteroidField.Add(asteroid);
            noAsteroids++;
        }
        yield return null;
    }
    
    public Vector2 GetAsteroidStartPosition()
    {
        Vector2 position = Vector2.zero;
        
        int side = (int) (Random.value * 4f);

        switch (side)
        {
            // arrives from the top of the screen
            case 0:
                position.x = Random.Range(ScreenBounds.Left, ScreenBounds.Right);
                position.y = ScreenBounds.Top+asteroidBeltSize;

                break;
            
            // arrives from the bottom of the screen
            case 1:
                position.x = Random.Range(ScreenBounds.Left, ScreenBounds.Right);
                position.y = ScreenBounds.Bottom-asteroidBeltSize;
                break;
            
            // arrives from the left side of the screen
            case 2:
                position.x = ScreenBounds.Left-asteroidBeltSize;
                position.y = Random.Range(ScreenBounds.Bottom, ScreenBounds.Top);
                break;

            // arrives from the right side of the screen
            case 3:
                position.x = ScreenBounds.Right+asteroidBeltSize;
                position.y = Random.Range(ScreenBounds.Bottom, ScreenBounds.Top);
                break;
        }

        return position;
    }

    Vector2 GetAsteroidTargetPosition()
    {
        Vector2 target = Random.insideUnitCircle * targetAreaRange * shorterSide;
        return target;
    }
    

    Asteroid CreateAsteroid(AsteroidSize size)
    {
        Vector2 startPosition = GetAsteroidStartPosition();
        Vector2 targetPosition = GetAsteroidTargetPosition();
        Vector2 direction = (targetPosition - startPosition).normalized;
        
        // Asteroid asteroid = Instantiate(asteroidPrefab, startPosition, Quaternion.identity);
        Asteroid asteroid = GetAsteroid(startPosition, Quaternion.identity);

        asteroid.SetAsteroidField(this);
        asteroid.SetDirection(direction);
        asteroid.SetSize(size);
        return asteroid;
    }
    
    #endregion
    
    

}
