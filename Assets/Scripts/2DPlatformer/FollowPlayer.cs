using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Transform cameraPosition;
    private float distance = 0f;

    private void Start()
    {
        cameraPosition = transform;
        distance = cameraPosition.position.x - player.position.x;
    }

    void Update()
    {
        cameraPosition.position= new Vector3(transform.position.x,player.transform.position.y,cameraPosition.position.z);
        
    }
}
