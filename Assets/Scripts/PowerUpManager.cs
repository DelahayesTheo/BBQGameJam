using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;

    public float speedMultiplicator;
    public float dashMultiplicator;

    private float baseDashCooldown;
    public Transform transformParent;

    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
        baseDashCooldown = playerController.dashCooldown;
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

            if (name == "DashBoost")
            {
                StartCoroutine(DashPowerUp());
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
        playerController.dashSpeed /= dashMultiplicator;
        playerController.walkingSpeed /= speedMultiplicator;

    }

    IEnumerator DashPowerUp()
    {
        playerController.dashCooldown = 0.1f;
        yield return new WaitForSeconds(5);
        playerController.dashCooldown = baseDashCooldown;

    }
    IEnumerator SizePowerUp()
    {
        transformParent.localScale += new Vector3(.5f, .5f, 0);
        transformParent.GetComponent<Rigidbody2D>().mass += 1;
        yield return new WaitForSeconds(5);
        transformParent.localScale -= new Vector3(.5f, .5f, 0);
        transformParent.GetComponent<Rigidbody2D>().mass -= 1;
    }



}
