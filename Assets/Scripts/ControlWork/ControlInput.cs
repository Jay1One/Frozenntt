using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInput : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private Camera m_Camera;

    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            m_Camera.transform.Translate(Vector3.right*Time.deltaTime);
            m_Player.transform.Translate(Vector3.right * Time.deltaTime);
        }
        else
        {
            return;
        }
      
    }

    public void OnMouseDown()
    {
        if (!start)
        {
            start = true;
        }
    }
}
