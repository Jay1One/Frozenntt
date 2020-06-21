using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    [SerializeField] private Bubble[] bubbles;
    [SerializeField] private float baseSpeed;
    private float speed;
    private bool isMoving = true;
    
    void Start()
    {
        foreach (var bubble in bubbles)
        {
            bubble.gameObject.SetActive(false);
        }

        speed = baseSpeed;
    }
    

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(new Vector3(0,0, -1)*speed);
        }

        if (transform.position.z<0)
        {
                    foreach (var bubble in bubbles)
                    {
                        bubble.gameObject.SetActive(true);
                        bubble.gameObject.transform.parent = null;
                    }

                    isMoving = false;
        }
    }
}
