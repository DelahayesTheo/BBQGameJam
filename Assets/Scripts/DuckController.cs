using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckController : MonoBehaviour
{
    public float walkingSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float dashSpeed;
    public int numPlayer;
    public float transformParent;

    Rigidbody2D m_Rigidbody2D;
    float m_CurrentDashCooldown;
    float m_CurrentDashDuration;
    Vector2 lastVelocity;

    Animator m_Animator;
    public ParticleSystem dashParticles;
    void Start()
    {
        m_Animator = transform.GetComponentInChildren<Animator>();
        m_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        m_CurrentDashCooldown = 0f;
        lastVelocity = new Vector2(1, 0f);
    }
    private void Update()
    {
        bool dash = CrossPlatformInputManager.GetButtonDown(GetControl(numPlayer, "Dash"));
        if (dash && m_CurrentDashCooldown <= 0f)
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
        m_Rigidbody2D.AddForce(direction * dashSpeed);
    }

    void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis(GetControl(numPlayer, "Horizontal"));
        float v = CrossPlatformInputManager.GetAxis(GetControl(numPlayer, "Vertical"));

        // Movement freeze after dash activation
        if (m_CurrentDashDuration > 0f)
        {
            m_CurrentDashDuration -= Time.deltaTime;
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
            m_Rigidbody2D.velocity = Vector2.Lerp(m_Rigidbody2D.velocity, Vector2.zero, Time.deltaTime * 2);
            return;
        }

        m_Animator.SetFloat("horizontal", h);
        m_Animator.SetFloat("vertical", v);

        // Save last velocity
        if (!Mathf.Approximately(h, 0f) || !Mathf.Approximately(v, 0f))
        {
            // Actual movement
            m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
            lastVelocity = new Vector2(h, v);
        }
    }

    private string GetControl(int numPlayer, string action)
    {
        return action + (numPlayer > 0 ? numPlayer : 1);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(m_CurrentDashDuration <= 0f)
            {
//                Vector2 otherPlayerVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
//                collision.gameObject.GetComponent<DuckController>().Dash(otherPlayerVelocity);
            }
        }
    }
}
