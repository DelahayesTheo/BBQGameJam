using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckController : MonoBehaviour
{
    public float walkingSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float dashSpeed;

    Rigidbody2D m_Rigidbody2D;
    float m_CurrentDashCooldown;
    float m_CurrentDashDuration;
    Vector2 lastVelocity;

    void Start()
    {

        m_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        m_CurrentDashCooldown = 0f;
        lastVelocity = new Vector2(1, 0f);
    }
    private void Update()
    {
        bool dash = CrossPlatformInputManager.GetButtonDown("Fire1");
        if (dash && m_CurrentDashCooldown <= 0f)
        {
            m_CurrentDashDuration = dashDuration;
            m_CurrentDashCooldown = dashCooldown;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(lastVelocity.normalized * dashSpeed);
        }
    }

    void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // Movement freeze after dash activation
        if (m_CurrentDashDuration > 0f)
        {
            m_CurrentDashDuration -= Time.deltaTime;
            Debug.Log("Dash");
            return;
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


        // Save last velocity
        if (!Mathf.Approximately(h, 0f) || !Mathf.Approximately(v, 0f))
        {
            lastVelocity = new Vector2(h, v);
        }
    }
}
