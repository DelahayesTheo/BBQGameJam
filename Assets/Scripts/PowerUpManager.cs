using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;

    public float speedMultiplicator;
    public float baseWalkingSpeed;

    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
        baseWalkingSpeed = playerController.walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
<<<<<<< HEAD
    {/*
<<<<<<< HEAD
=======
        playerController.PowerUp(collision.gameObject.name);
        Destroy(collision.gameObject);
>>>>>>> f31c1babe34b9a832051ef8c23bf37c5b5a11e8c*/
=======
    {
        name = collision.gameObject.name;
        Debug.Log(name);
        if ( name == "SpeedBoost")
        {
            playerController.walkingSpeed = baseWalkingSpeed * speedMultiplicator;
        }
        Destroy(collision.gameObject);
>>>>>>> 849e291f86b1a0cbe3d8eff4eafe1e7a9063eac8
    }
}
