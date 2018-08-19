using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	public void GoToScene(int scene) 
    {
        SceneManager.LoadScene(scene);
    }
    public void Update()
    {
        bool dash = CrossPlatformInputManager.GetButtonDown("Dash1") || CrossPlatformInputManager.GetButtonDown("Dash2") || CrossPlatformInputManager.GetButtonDown("Dash3") || CrossPlatformInputManager.GetButtonDown("Dash4");
        if (dash)
        {
            SceneManager.LoadScene(1);
        }
    }

}
