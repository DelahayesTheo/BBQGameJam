using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;
    public GameObject shield;

    public float speedMultiplicator;
    public float dashMultiplicator;

    private GameManager gameManagerScript;
    private float baseDashCooldown;
    public Transform transformParent;

    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
            if(name == null)
            {
                return;
            }
            if (name == "SpeedBoost")
            {
                StartCoroutine(SpeedPowerUp());
            }

            if (name == "DashBoost")
            {
                StartCoroutine(DashPowerUp());
            }

            if (name == "InvertControls")
            {
                StartCoroutine(InvertControls());
            }

            if (name == "SizeBoost")
            {
                StartCoroutine(SizePowerUp());
            }

            if (name == "ShieldBoost") {
                StartCoroutine(ShieldBoost());
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ShieldBoost()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
    }

    IEnumerator InvertControls()
    {
        gameManagerScript.InvertControls(playerController.numPlayer - 1);
        yield return new WaitForSeconds(5);
        gameManagerScript.RestoreControls();
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
