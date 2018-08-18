using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DuckControllerT : MonoBehaviour
{
    public float walkingSpeed;
    public float sizeMultiplicator;

    private bool isBigger;
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

        if (isBigger) {
            this.transform.localScale = new Vector3(1 * sizeMultiplicator, 1 * sizeMultiplicator, 1);
        }
    }

    public void PowerUp(string name)
    {
        if (name == "SizeBoost") {
            isBigger = true;
        }
    }
}
