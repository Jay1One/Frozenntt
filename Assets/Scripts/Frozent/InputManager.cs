using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform[] points;
    [SerializeField] private Camera Camera;
    [SerializeField] private float distance;

    private GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        sphere.SetActive(true);
        print("Нажал");
        if (Camera != null)
        {
           
            print( Camera.ScreenToWorldPoint(startPoint.position));
            print(Input.mousePosition);
            Vector3 MousePoint = Camera.ScreenToWorldPoint(Input.mousePosition);
            print(MousePoint);

            sphere = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), MousePoint, Quaternion.identity);
            
            if (MousePoint.x < startPoint.position.x + distance && MousePoint.x > startPoint.position.x - distance &&
                MousePoint.y < startPoint.position.y + distance && MousePoint.x > startPoint.position.y - distance && FrozentGameManager.currentState==FrozenTGameState.CuttingStatue)
            {
                print("старт нарезания");
            }
            else
            {
                 print("не туда");
            }
        }
               
    }

    private void OnMouseDrag()
    {
        sphere.SetActive(true);
        sphere.transform.position=Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        sphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown("0"))
    }
}
