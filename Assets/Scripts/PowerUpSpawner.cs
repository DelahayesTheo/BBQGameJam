using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    public GameObject[] powerUps;
    public Transform[] spawnPoints;
    public float spawnTimer;

	private int tries;
    private GameObject spawnedPowerUp;
    private float timer;
    private Transform randomPos;
	// Use this for initialization
	void Start () {
        timer = spawnTimer;
		tries = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0f) {
            randomPos = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            while (randomPos.childCount > 0 && tries <= 4) {
                randomPos = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
				tries++;
            }
			if (randomPos.childCount == 0 ){
				spawnedPowerUp = Instantiate(powerUps[UnityEngine.Random.Range(0, powerUps.Length)], randomPos.position, randomPos.rotation);
				spawnedPowerUp.transform.parent = randomPos;
			}
			tries = 0;
            timer = spawnTimer;
        }
	}
}
