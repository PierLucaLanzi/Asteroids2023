using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : Singleton<GameOverScreen>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }

    public void OnEnable()
    {
        StartCoroutine(RestartScreen());
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    IEnumerator RestartScreen()
    {
        yield return new WaitForSeconds(10f);
        GameManager.Instance.ShowStartScreen();
    }
}
