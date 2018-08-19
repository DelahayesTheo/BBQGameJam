using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private Text textBox;
    private Canvas canvas;
	// Use this for initialization
	void Start () {
        textBox = GetComponentInChildren<Text>();
        Time.timeScale = 0;

        StartCoroutine(WaitToResumeGame());


	}
	
	// Update is called once per frame
	void Update () {
		
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
