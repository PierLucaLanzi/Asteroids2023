using System;
using TMPro;
using UnityEngine;

public class HeadsUpDisplay : Singleton<HeadsUpDisplay>
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI highscoreDisplay;
    [SerializeField] private TextMeshProUGUI shipsDisplay;

    
    public void UpdateScore(int score)
    {
        scoreDisplay.text = string.Format("{0:D6}", score);
    }
    
    public void UpdateHighScore(int score)
    {
        highscoreDisplay.text = string.Format("{0:D6}", score);
    }

    public void UpdateShips(int ships)
    {
        shipsDisplay.text = string.Format("{0:D2}", ships);
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

}

//
// private int score = 1000;
// private int highscore = 10000;
// private int ships = 3;
//
// void Start()
// {
//     UpdateScore(score);
//     UpdateHighScore(highscore);
//     UpdateShips(ships);
// }
//
// private void Update()
// {
//     if (Input.GetKeyDown(KeyCode.P))
//     {
//         score += 100;
//         highscore += 100;
//         ships++;
//         UpdateScore(score);
//         UpdateHighScore(highscore);
//         UpdateShips(ships);
//     }
//     
//     if (Input.GetKeyDown(KeyCode.O))
//     {
//         score -= 100;
//         highscore -= 100;
//         ships--;
//
//         score = Mathf.Max(0, score);
//         highscore = Mathf.Max(0, highscore);
//         ships = Mathf.Max(0, ships);
//         
//         UpdateScore(score);
//         UpdateHighScore(highscore);
//         UpdateShips(ships);
//     }
// }
