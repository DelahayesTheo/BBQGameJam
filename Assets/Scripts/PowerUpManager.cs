using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private DuckController playerController;
    public GameObject shield;
    private GameObject spawnedShield;
    public float speedMultiplicator;
    public float dashMultiplicator;

    public bool shieldSpawned;
    private GameManager gameManagerScript;
    private float baseDashCooldown;
    private float baseCanMoveTime;
    public Transform transformParent;

    public AudioSource sizeAudio;
    public AudioSource speedAudio;
    public AudioSource confusionAudio;
    public AudioSource shieldAudio;
    public AudioSource dashBoostAudio;
   
    // Use this for initialization
    void Start()
    {
        shieldSpawned = false;
        playerController = transform.parent.gameObject.GetComponent<DuckController>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        baseDashCooldown = playerController.dashCooldown;
        baseCanMoveTime = playerController.canMoveCooldownTime;
        transformParent = transform.parent;
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldSpawned) {
            spawnedShield.transform.position = transform.parent.position;
        }
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
        spawnedShield = Instantiate(shield, transformParent);
        spawnedShield.gameObject.SetActive(true);
        spawnedShield.transform.parent = null;
        shieldSpawned = true;
        shieldAudio.Play();
        yield return new WaitForSeconds(5);
        shieldSpawned = false;
        Destroy(spawnedShield);   
    }

    IEnumerator InvertControls()
    {
        confusionAudio.Play();
        gameManagerScript.InvertControls(playerController.numPlayer - 1);
        yield return new WaitForSeconds(5);
        gameManagerScript.RestoreControls();
    }

    IEnumerator SpeedPowerUp()
    {
        playerController.walkingSpeed *= speedMultiplicator;
        playerController.dashSpeed *= dashMultiplicator;
        speedAudio.Play();
        yield return new WaitForSeconds(5);
        playerController.dashSpeed /= dashMultiplicator;
        playerController.walkingSpeed /= speedMultiplicator;
    }

    IEnumerator DashPowerUp()
    {
        dashBoostAudio.Play();
        playerController.dashCooldown = 0.1f;
        yield return new WaitForSeconds(5);
        playerController.dashCooldown = baseDashCooldown;

    }
    IEnumerator SizePowerUp()
    {
        transformParent.localScale += new Vector3(.5f, .5f, 0);
        transformParent.GetComponent<Rigidbody2D>().mass += 1;
        sizeAudio.Play();
        yield return new WaitForSeconds(5);
        transformParent.localScale -= new Vector3(.5f, .5f, 0);
        transformParent.GetComponent<Rigidbody2D>().mass -= 1;
    }



}
