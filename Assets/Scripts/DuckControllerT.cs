using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckControllerT : MonoBehaviour
{
    public float walkingSpeed;

    Rigidbody2D m_Rigidbody2D;

    void Start()
    {

        m_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
    }

    public void PowerUp(string name)
    {
        Debug.Log(name);
    }
}
