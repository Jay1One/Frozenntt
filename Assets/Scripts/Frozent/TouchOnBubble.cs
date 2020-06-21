using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOnBubble : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        StartCoroutine(gameObject.GetComponentInParent<BubbleCompression>().Compress());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
