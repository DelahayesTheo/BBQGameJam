using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckControllerT : MonoBehaviour
{
    public float walkingSpeed;
    public float speedBoost;

    public bool isFaster;
    Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        isFaster = false;
        m_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
        if (isFaster){
            m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed * speedBoost, v * walkingSpeed * speedBoost);
            SleepTimeout * 10;
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
          
        }
    }

    public void PowerUp(string name)
    {
        if (name == "SpeedBoost"){
            isFaster = true;
        }
    }
}
