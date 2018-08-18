using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;

    public float speedMultiplicator;
    public float baseWalkingSpeed;
    public Transform transformParent;
   

    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
        baseWalkingSpeed = playerController.walkingSpeed;
        transformParent = transform.parent; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        name = collision.gameObject.name;
        Debug.Log(name);
        if ( name == "SpeedBoost")
        {
            playerController.walkingSpeed = baseWalkingSpeed * speedMultiplicator;
        }
        Destroy(collision.gameObject);
    }
       
}
