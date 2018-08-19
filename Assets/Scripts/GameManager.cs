using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private Text textBox;
    private Canvas canvas;
    public bool[] playersDead;

    public GameObject[] players;

    private int nbPlayerDead;
    private int winner;
    private bool gameOver;
	// Use this for initialization
	void Start () {
        playersDead = new bool[] { false, false, false, false };
        textBox = GetComponentInChildren<Text>();
        Time.timeScale = 0;

        StartCoroutine(WaitToResumeGame());
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (gameOver) {
            textBox.fontSize = 25;
            textBox.text = "Le joueur " + winner  + " a gagné";

            if (Input.GetKeyDown("space")) {
                SceneManager.LoadScene(1);
            }
        } else {
            nbPlayerDead = 0;

            for (int i = 0; i < playersDead.Length; i++)
            {
                if (playersDead[i])
                {
                    nbPlayerDead++;
                }
            }

            if (nbPlayerDead >= 3)
            {
                gameOver = true;
                for (int i = 0; i < playersDead.Length; i++)
                {
                    if (!playersDead[i])
                    {
                        winner = i+1;
                    }
                }
            }

        }

    }

    public void InvertControls (int numPlayer) 
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i != numPlayer && players[i] != null) {
                players[i].GetComponent<DuckController>().controlsDirection = -1;
            }
        }
    }

    public void RestoreControls () 
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                players[i].GetComponent<DuckController>().controlsDirection = 1;
            }
        }
    }
    IEnumerator WaitForCountdown() 
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f) {
            yield return 0;
        }
    }

    IEnumerator WaitToResumeGame()
    {
        textBox.text = "3";
        yield return WaitForCountdown();

        textBox.text = "2";
        yield return WaitForCountdown();

        textBox.text = "1";
        yield return WaitForCountdown();

        textBox.text = "";
        Time.timeScale = 1;
    }
}
