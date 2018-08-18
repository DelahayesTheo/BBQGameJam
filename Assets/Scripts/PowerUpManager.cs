using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;
    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
=======
        playerController.PowerUp(collision.gameObject.name);
        Destroy(collision.gameObject);
>>>>>>> f31c1babe34b9a832051ef8c23bf37c5b5a11e8c
    }
}
