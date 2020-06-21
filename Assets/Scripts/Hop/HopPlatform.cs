using System;
using UnityEngine;


public class HopPlatform : MonoBehaviour
{
    [SerializeField] private GameObject m_BasePlatform;
    [SerializeField] private GameObject m_DonePlatform;

    private void Start()
    {
        m_BasePlatform.SetActive(true);
        m_DonePlatform.SetActive(false);
    }

    public void SetupDone()
    {
        m_BasePlatform.SetActive(false);
        m_DonePlatform.SetActive(true);
    }
}
