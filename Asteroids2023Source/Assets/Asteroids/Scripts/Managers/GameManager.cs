using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("UI")]
    [SerializeField] private HeadsUpDisplay headsUpDisplay;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    
    [Header("Gameplay")]
    [SerializeField] private SpaceShip spaceShipPrefab;
    [SerializeField] private int noInitialSpaceShips = 1;

    [Header("Scoring")] 
    [SerializeField] private AsteroidScoring asteroidScoring;
    
    // [Header("Level Manager")]
    // [SerializeField] private LevelManager levelManager;
    private LevelManager levelManager;
    
    private AsteroidField asteroidField;
    private SpaceShip spaceShip;

    private int score = 0;
    private int highscore;
    private int level = 0;
    private int noSpaceShips = 0;
    private int nextShipAt;

    void Awake()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
    private void Start()
    {
        highscore = asteroidScoring.ExtraShipAfter;
        nextShipAt = asteroidScoring.ExtraShipAfter;
        
        // create the spaceship
        spaceShip = Instantiate(spaceShipPrefab, Vector3.zero, quaternion.identity);
        spaceShip.gameObject.SetActive(false);
        
        // find the asteroid field 
        asteroidField = GameObject.FindObjectOfType<AsteroidField>();

        // TODO
        // - we need a level manager to deal with level progression
        // - init the HUD with the press play

        StartGame();
    }
    
    public void StartGame()
    {
        print("START GAME");
        
        // set the initial number of spaceships
        noSpaceShips = noInitialSpaceShips;

        // get starting level
        level = 0;

        // clear remaining asteroids
        asteroidField.ClearAsteroidField();

        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        // start background music
        AudioManager.Instance.StartBackgroundMusic();
        
        ClearUI();
        
        yield return new WaitForSeconds(1f);

        // update the ui
        headsUpDisplay.gameObject.SetActive(true);
        headsUpDisplay.UpdateHighScore(highscore);
        headsUpDisplay.UpdateScore(score);
        headsUpDisplay.UpdateShips(noSpaceShips);
        startScreen.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        
        // reset position
        spaceShip.ResetPosition();
        spaceShip.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        SimpleAsteroidLevel simpleAsteroidLevel = 
            levelManager.GetLevel(level);        

        // create the asteroid field
        asteroidField.CreateAsteroidField(simpleAsteroidLevel.NoAsteroids);
    }

    private void ClearUI()
    {
        headsUpDisplay.Close();
        startScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
    }

    public void SpaceShipDestroyed()
    {
        // TODO
        // - deactivate the spaceship
        spaceShip.SetActive(false);
        
        // - decrease the number of spaceship
        noSpaceShips--;

        // - update the display
        UpdateUI();

        if (noSpaceShips == 0)
        {
            StartCoroutine(GameOverCoroutine());
        }
        else
        {
            StartCoroutine(NextSpaceShip());
        }
        
        // - if you have no space ships then game over
        //   else start with the next spaceship coroutine
    }

    IEnumerator GameOverCoroutine()
    {
        // TODO
        // - wait a little bit
        yield return new WaitForSeconds(3f);
        
        // - disable the HUD
        ClearUI();
        
        // - show the game over screen
        gameOverScreen.Open();
        
        yield return null;
    }
    
    IEnumerator NextSpaceShip()
    {
        // TODO 
        
        // - wait a little bit (the spaceship is already non active
        yield return new WaitForSeconds(1f);
        
        // - reset position
        spaceShip.ResetPosition();
        
        // - activate the spaceship
        spaceShip.SetActive(true);
        
        // - makes it invincible
        spaceShip.EnableIndestructible();
        
        yield return null;
    }

    public void AsteroidDestroyed(AsteroidSize size)
    {
        // TODO
        // - Play sound
        AudioManager.Instance.PlayAsteroidExplosion(size);
        
        // - Update score

        // score should depend on the type of asteroid
        int points;

        switch (size)
        {
            case AsteroidSize.Large:
                points = asteroidScoring.GetPoints(AsteroidSize.Large);
                break;
            case AsteroidSize.Medium:
                points = asteroidScoring.GetPoints(AsteroidSize.Medium);
                break;
            case AsteroidSize.Small:
            default:
                points = asteroidScoring.GetPoints(AsteroidSize.Small);
                break;
        }
        
        UpdateScore(points);

        UpdateUI();
    }
    
    public void UpdateScore(int points)
    {
        score += points;
        if (score > highscore)
        {
            highscore = score;
        }

        if (score >= nextShipAt)
        {
            noSpaceShips++;
            nextShipAt += 10000;
            
            // TODO 
            AudioManager.Instance.PlayExtraSpaceship();
        }
    }

    private void UpdateUI()
    {
        // set highscore, score, and noSpaceShips on the GUI
        // startScreen.SetHighScore(highscore);
        headsUpDisplay.UpdateScore(score);
        headsUpDisplay.UpdateHighScore(highscore);
        headsUpDisplay.UpdateShips(noSpaceShips);

    }

    public void ShowStartScreen()
    {
        // TODO
        // - clear all the UI
        // - activate the start screen
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    public IEnumerator NextLevelCoroutine()
    {
        level = level + 1;
        SimpleAsteroidLevel simpleAsteroidLevel = levelManager.GetLevel(level);
        
        yield return new WaitForSeconds(2f);
        
        DebugScreen.Instance.Show("NO ASTEROID IN LEVEL "+simpleAsteroidLevel.NoAsteroids.ToString());
        asteroidField.CreateAsteroidField(simpleAsteroidLevel.NoAsteroids);

        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateScore(5000);
            UpdateUI();
        }
    }
    
}
