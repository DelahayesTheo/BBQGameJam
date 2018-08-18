using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;

    public float speedMultiplicator;
    private float baseWalkingSpeed;

    public float dashMultiplicator;
    private float baseDashSpeed;

    public Transform transformParent;

    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
        baseWalkingSpeed = playerController.walkingSpeed;
        baseDashSpeed = playerController.dashSpeed;
        transformParent = transform.parent; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) {
            name = collision.gameObject.tag;
            if (name == "SpeedBoost")
            {
                StartCoroutine(SpeedPowerUp());
            }


            if (name == "SizeBoost")
            {
                StartCoroutine(SizePowerUp());
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator SpeedPowerUp()
    {
        playerController.walkingSpeed *= speedMultiplicator;
        playerController.dashSpeed *= dashMultiplicator;
        yield return new WaitForSeconds(5);
        playerController.walkingSpeed /= speedMultiplicator;
        playerController.dashSpeed /= dashMultiplicator;
    }

    IEnumerator SizePowerUp()
    {
        transformParent.localScale += new Vector3(.5f, .5f, 0);
        yield return new WaitForSeconds(5);
        transformParent.localScale -= new Vector3(.5f, .5f, 0);
    }



}
