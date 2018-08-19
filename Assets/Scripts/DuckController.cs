using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckController : MonoBehaviour
{
    public float maxMagnitudeInMovements = 15f;
    public float bounceControl = 0.5f;
    public float dashControl = 0.5f;
    public float walkingSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float dashSpeed;
    public int numPlayer;
    public float transformParent;
    public float canMoveCooldownTime;
    public Vector2 maxVelocity;
    Rigidbody2D m_Rigidbody2D;
    float m_CurrentDashCooldown;
    float m_CurrentDashDuration;
    Vector2 lastVelocity;
    float m_CanMoveCooldown;

    public GameObject gameManager;
    private GameManager gameManagerScript;
    Animator m_Animator;
    public ParticleSystem dashParticles;
    void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        m_Animator = transform.GetComponentInChildren<Animator>();
        m_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        m_CurrentDashCooldown = 0f;
        lastVelocity = new Vector2(1, 0f);
    }
    
    private void Update()
    {
        bool dash = CrossPlatformInputManager.GetButtonDown(GetControl(numPlayer, "Dash"));
        if (dash && m_CurrentDashCooldown <= 0f && m_CanMoveCooldown <= 0f)
        {
            Dash(lastVelocity.normalized);
            dashParticles.transform.rotation = Quaternion.LookRotation(-lastVelocity.normalized);
            dashParticles.Play();
        }
    }

    public void Dash(Vector2 direction)
    {
        m_CurrentDashDuration = dashDuration;
        m_CurrentDashCooldown = dashCooldown;
        m_Rigidbody2D.velocity = Vector2.zero;
        m_Rigidbody2D.AddForce(direction * dashSpeed * m_Rigidbody2D.mass);
        m_Rigidbody2D.velocity = Vector2.ClampMagnitude(m_Rigidbody2D.velocity, maxMagnitudeInMovements);
    }

    void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis(GetControl(numPlayer, "Horizontal"));
        float v = CrossPlatformInputManager.GetAxis(GetControl(numPlayer, "Vertical"));

        if (m_CanMoveCooldown > 0f)
        {
            m_CanMoveCooldown -= Time.deltaTime;
            if (m_CanMoveCooldown <= 0f)
            {
                m_Rigidbody2D.drag = 1f;
            }
            else
            {
                // Bounce control
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + h * bounceControl, m_Rigidbody2D.velocity.y + v * bounceControl);
                m_Rigidbody2D.velocity = Vector2.ClampMagnitude(m_Rigidbody2D.velocity, maxMagnitudeInMovements);
                return;
            }

        }

        // Movement freeze after dash activation
        if (m_CurrentDashDuration > 0f)
        {
            m_CurrentDashDuration -= Time.deltaTime;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + h * dashControl, m_Rigidbody2D.velocity.y + v * dashControl);
            m_Rigidbody2D.velocity = Vector2.ClampMagnitude(m_Rigidbody2D.velocity, maxMagnitudeInMovements);
            return;
        }

        if (!dashParticles.isStopped)
        {
            dashParticles.Stop();
        }

        // Dash cooldown
        if (m_CurrentDashCooldown > 0f)
        {
            m_CurrentDashCooldown -= Time.deltaTime;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + h * dashControl, m_Rigidbody2D.velocity.y + v * dashControl);
            m_Rigidbody2D.velocity = Vector2.Lerp(m_Rigidbody2D.velocity, Vector2.zero, Time.deltaTime * 2);
            return;
        }

        m_Animator.SetFloat("horizontal", h);
        m_Animator.SetFloat("vertical", v);
        
        if (!Mathf.Approximately(h, 0f) || !Mathf.Approximately(v, 0f))
        {
            // Actual movement
            m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
            // Save last velocity
            lastVelocity = new Vector2(h, v);
        }
        m_Rigidbody2D.velocity = Vector2.ClampMagnitude(m_Rigidbody2D.velocity, maxMagnitudeInMovements);
    }

    IEnumerator DeathAnimation ()
    {
        gameManagerScript.playersDead[numPlayer - 1] = true;
        m_Animator.SetTrigger("die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private string GetControl(int numPlayer, string action)
    {
        return action + (numPlayer > 0 ? numPlayer : 1);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bouee")
        {
            m_Rigidbody2D.drag = 5f;
            m_CanMoveCooldown = canMoveCooldownTime;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArenaBox") {
            m_CanMoveCooldown = 99999f;
            StartCoroutine(DeathAnimation());
        }
    }
}
