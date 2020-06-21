using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake_RotateObstackle : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        //вращение
        gameObject.transform.Rotate(new Vector3(0f, 0f, 12f*Time.deltaTime));
    }
}
