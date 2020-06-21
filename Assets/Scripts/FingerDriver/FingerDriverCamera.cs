using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerDriverCamera : MonoBehaviour
{
    [SerializeField] private Transform m_carTransform;
    private float cam2;
        
    // Start is called before the first frame update
    void Start()
    {
        cam2 = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = m_carTransform.position;
        pos.z = cam2;
        transform.position = pos;
    }
}
