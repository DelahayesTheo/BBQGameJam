using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingHazard : MonoBehaviour {
    public Transform[] points;
    public int pointSelection;
    
    public float timer;
    public float moveSpeed;
    private Transform currentPoint;
	// Use this for initialization
	void Start () {
        currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        
        if(timer <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed);

            if (transform.position == currentPoint.position)
            {
                pointSelection++;

                if (pointSelection == points.Length)
                {
                    pointSelection = 0;
                }

                currentPoint = points[pointSelection];
            }
        }
	}
}
