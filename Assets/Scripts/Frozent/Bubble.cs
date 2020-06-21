using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float compressionTime = 3f;
    [SerializeField] private GameObject sprite;
    public Statue CurrentStatue;
    public int pieceNumber;

    private void OnMouseDown()
    {
        
        Destroy(gameObject);
    }

    private IEnumerator Start()
    {
        float currentTime=compressionTime;
        Vector3 beginScale = sprite.transform.localScale;
        while (currentTime-Time.deltaTime>0)
        {
            currentTime -= Time.deltaTime;
            transform.localScale=new Vector3(Mathf.Lerp(0f,beginScale.x,currentTime/compressionTime),
                Mathf.Lerp(0f,beginScale.y,currentTime/compressionTime),
                Mathf.Lerp(0f,beginScale.z,currentTime/compressionTime));
            yield return null; 

        }
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        print("bubble destroyed");
        CurrentStatue.DestroyPiece(pieceNumber);
    }
}
