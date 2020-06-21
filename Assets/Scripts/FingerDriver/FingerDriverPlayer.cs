using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FingerDriverPlayer : MonoBehaviour
    {
        [SerializeField] private FingerDriverTrack m_Track;
        [SerializeField] private FingerDriverInput m_input;
        //точка по которой будет проверяться нахождение на трассе 
        //смотрим по носу машины
        [SerializeField] private Transform m_trackPoint;
        [SerializeField] private float m_CarSpeed = 2f;
        [SerializeField] private float m_MaxSteer = 90f;
        
            
        private void Update()
        {
            if (m_Track.IsPointInTrack(m_trackPoint.position))
            {
                transform.Translate(transform.up*Time.deltaTime*m_CarSpeed,Space.World);
                transform.Rotate(0f,0f,m_MaxSteer*m_input.SteerAxis*Time.deltaTime);
            }
            else
            {
                print("Score:\t"+ FingerDriverTrack.score);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
