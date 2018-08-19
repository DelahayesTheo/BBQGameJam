using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera m_Camera;
	// Use this for initialization
	void Start () {
        m_Camera = GetComponent<Camera>();
        float aspect = Mathf.Round(m_Camera.aspect * 100f) / 100f;
        if (Mathf.Approximately(aspect,1.77f)) // 16/9
        {
            m_Camera.orthographicSize = 5.4f;
        }
        else if (Mathf.Approximately(aspect, 1.6f)) // 16/10
        {
            m_Camera.orthographicSize = 5.8f;
        }
        else if (Mathf.Approximately(aspect, 1.33f)) // 4/3
        {
            m_Camera.orthographicSize = 7f;
        }
        else if (Mathf.Approximately(aspect, 1.25f)) // 5/4
        {
            m_Camera.orthographicSize = 7.5f;
        }
        else if (Mathf.Approximately(aspect, 1.5f)) // 3/2
        {
            m_Camera.orthographicSize = 6.4f;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
