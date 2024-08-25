using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreDisplay;

    public void OnPressPlay()
    {
        GameManager.Instance.StartGame();
    }

    public void SetHighScore(int score)
    {
        highscoreDisplay.text = string.Format("{0:D6}", score);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO
            // - Start Game
        }
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
