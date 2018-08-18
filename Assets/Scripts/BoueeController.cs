using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoueeController : MonoBehaviour
{
    Rigidbody2D p_rigidbody;
    private int horizontale;
    private int verticale;
    private float velocite_h;
    private float velocite_v;
    public float pousser;

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            Debug.Log("je tape");
            p_rigidbody = collision.transform.GetComponent<Rigidbody2D>();
            velocite_h = p_rigidbody.velocity.x;
            velocite_v = p_rigidbody.velocity.y;
            if (velocite_h > 0.05)
            {
                horizontale = -1;

            }
            else if (velocite_h < -0.05)
            {
                horizontale = 1;
            }
            else
            {
                horizontale = 0;
            }
            if (velocite_v > 0.05)
            {
                verticale = -1;

            }
            else if (velocite_v < -0.05)
            {
                verticale = 1;
            }
            else
            {
                verticale = 0;
            }
            
            p_rigidbody.AddForce(new Vector2(pousser*-velocite_h,pousser*-velocite_v)); 
        }
    }
}
