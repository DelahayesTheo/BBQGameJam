using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckControllerT : MonoBehaviour
{
    public float walkingSpeed;
<<<<<<< HEAD
    public float speedBoost;

    public bool isFaster;
=======
    public float sizeMultiplicator;

    private bool isBigger;
>>>>>>> 4cf844b653924cceb712e643fa93bc7a94552c21
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
<<<<<<< HEAD
        if (isFaster){
            m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed * speedBoost, v * walkingSpeed * speedBoost);
            SleepTimeout * 10;
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(h * walkingSpeed, v * walkingSpeed);
          
=======

        if (isBigger) {
            this.transform.localScale = new Vector3(1 * sizeMultiplicator, 1 * sizeMultiplicator, 1);
>>>>>>> 4cf844b653924cceb712e643fa93bc7a94552c21
        }
    }

    public void PowerUp(string name)
    {
<<<<<<< HEAD
        if (name == "SpeedBoost"){
            isFaster = true;
=======
        if (name == "SizeBoost") {
            isBigger = true;
>>>>>>> 4cf844b653924cceb712e643fa93bc7a94552c21
        }
    }
}
