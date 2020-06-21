using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed=5f;

    [SerializeField] private float distance = 5f;

    private float startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        while (transform.position.z<=startPoint+distance)
        {
            transform.Translate(Vector3.back * (Time.deltaTime * speed));
        }

        if (transform.position.z >= startPoint + distance)
        {
            Debug.Log("Stop");
            //Destroy(gameObject.GetComponent<MoveCamera>()); 
        }
    }
}
