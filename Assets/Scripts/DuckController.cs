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
            m_CurrentDashDuration = dashDuration;
            m_CurrentDashCooldown = dashCooldown;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(lastVelocity.normalized * dashSpeed);
            dashParticles.transform.rotation = Quaternion.LookRotation(-lastVelocity.normalized);
            dashParticles.Play();
        }
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
            m_Rigidbody2D.velocity = Vector2.Lerp(m_Rigidbody2D.velocity, Vector2.zero, Time.deltaTime * (1 / dashCooldown));
            return;
        }

        // Actual movement
        m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
        m_Animator.SetFloat("horizontal", h);
        m_Animator.SetFloat("vertical", v);

        // Save last velocity
        if (!Mathf.Approximately(h, 0f) || !Mathf.Approximately(v, 0f))
        {
            lastVelocity = new Vector2(h, v);
        }
    }

    private string GetControl(int numPlayer, string action)
    {
        return action + numPlayer;
    }
}
