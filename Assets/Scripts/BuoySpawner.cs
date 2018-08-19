using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoySpawner : MonoBehaviour {

    public GameObject buoy;

    private float xScale;
    private float yScale;
    private GameObject buoySpawned;
    private float posXLeft;
    private float posYTop;
    private float posXRight;
    private float posYDown;

    private Vector3 spawnPosition;
    private int willItSpawn;
	// Use this for initialization
	void Start () {
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;

        posXLeft = transform.position.x - xScale / 2;
        posXRight = posXLeft + xScale;

        posYDown = transform.position.y - yScale / 2;
        posYTop = transform.position.y + yScale;

        willItSpawn = UnityEngine.Random.Range(0, 2);
        Debug.Log(willItSpawn);
        if (willItSpawn == 1) {
            spawnPosition = new Vector3(UnityEngine.Random.Range(posXLeft, posXRight), UnityEngine.Random.Range(posYDown, posYTop), 1);
            buoySpawned = Instantiate(buoy, spawnPosition, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
	
}
