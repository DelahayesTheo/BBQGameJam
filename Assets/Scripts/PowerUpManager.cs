using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    private DuckControllerT playerController;
	// Use this for initialization
	void Start () {
        playerController = transform.parent.gameObject.GetComponent<DuckControllerT>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerController.PowerUp(collision.gameObject.name);
    }
}
