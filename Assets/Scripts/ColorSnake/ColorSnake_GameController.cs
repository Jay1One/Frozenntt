using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class ColorSnake_GameController : MonoBehaviour
{
    public class CameraBounds
    {
        public float Left;
        public float Right;
        public float Up;
        public float Down;
    }
    [SerializeField] private Camera m_Camera;
    public Camera Camera => m_Camera;
    [SerializeField] private ColorSnake_Types m_Types;
    
    public ColorSnake_Types Types => m_Types;
    
    [SerializeField] private CameraBounds m_Bounds;
    public CameraBounds Bounds => m_Bounds;

    [SerializeField] private ColorSnake_Snake m_Snake;

    private void Awake()
    {
        Vector2 minScreen = m_Camera.ScreenToWorldPoint(Vector3.zero);
        m_Bounds = new CameraBounds()
        {
            Left = minScreen.x,
            Right=Mathf.Abs(minScreen.x),
            Up = Mathf.Abs(minScreen.y),
            Down =minScreen.y
        };
    }

    private void Update()
    {
        m_Camera.transform.Translate(Vector3.up*Time.deltaTime);
        m_Snake.transform.Translate(Vector3.up*Time.deltaTime);
    }
}
