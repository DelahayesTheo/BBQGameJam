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
