﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerDriverInput : MonoBehaviour
{
    [SerializeField] private Transform m_sterWheelTransform;

    [SerializeField] [Range(0f, 180f)] private float m_MaxSteerAngle=90f;

    [SerializeField] [Range(0f, 1f)] private float m_StearAcceleration=0.25f;

    private float steerAxis;

    public float SteerAxis
    {
        get => steerAxis;
        set
        {
            steerAxis = Mathf.Lerp(steerAxis, value, m_StearAcceleration);
        }
    }

    private Vector2 startSteerWheelPoint;

    private Camera mainCamera;
    //запоминаем координаты руля в экранной системе координат
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        startSteerWheelPoint = mainCamera.WorldToScreenPoint(m_sterWheelTransform.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //мереем угол между рулём и точкой косания экрана
            Vector2 dir = (Vector2) Input.mousePosition - startSteerWheelPoint;
            float angle = Vector2.Angle(Vector2.up, dir);
            angle /= m_MaxSteerAngle;
            angle = Mathf.Clamp01(angle);
            if (Input.mousePosition.x > startSteerWheelPoint.x)
            {
                angle *= -1f;
            }
            SteerAxis = angle;
        }
        else
        {
            steerAxis = 0;
        }
        m_sterWheelTransform.localEulerAngles=new Vector3(0f,0f,steerAxis*m_MaxSteerAngle);
        Vector3 pos = mainCamera.ScreenToWorldPoint(startSteerWheelPoint);
        pos.z = -3;
        m_sterWheelTransform.position = pos;
    }
}
