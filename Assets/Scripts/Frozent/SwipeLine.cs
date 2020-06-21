using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLine : MonoBehaviour
{
    public Transform[] points;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float distance = 0.1f;
    private bool startCut;
    private static int SuccessCut = 0;
    private static int AllCut;
    private void Awake()
    {
        startCut = false;
        //Drawing();
        //lineRenderer.enabled = false;
        //var meshColl = gameObject.AddComponent<MeshCollider>();
    }

    public void Drawing()
    {
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
           
            lineRenderer.SetPosition(i,points[i].position);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

        }
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        Mesh mesh = new Mesh();
        // lineRenderer.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }
    private void OnMouseDown()
    {
        print("MOUSEDOWN LINE");
        if (camera != null)
        {
            print(camera.ScreenToWorldPoint(Input.mousePosition));
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            startPoint.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);

            print("success" + SuccessCut);
            print("all try" + AllCut);
        }
    }

    private void OnMouseUp()
    {
        //print(camera.ScreenToWorldPoint(Input.mousePosition));
    }

    private void OnMouseEnter()
    {
        if (Input.anyKey)
        {
            // Plane PlaneForPick = new Plane(Vector3.up, Vector3.zero);
            // print(); PlaneForPick.GetDistanceToPoint(startPoint.position);
            // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            // float rayDistance;
            // if (PlaneForPick.Raycast(ray, out rayDistance))
            // {
            //     Vector3 Test = ray.GetPoint(rayDistance);
            //     print(Test);
            // }
            //
            print("Я зашёл на новый меш");
            Vector3 MousePoint = camera.ScreenToWorldPoint(Input.mousePosition);
            

            if (MousePoint.x < startPoint.position.x + distance && MousePoint.x > startPoint.position.x - distance
                                                                && MousePoint.y < startPoint.position.y + distance &&
                                                                MousePoint.y > startPoint.position.y - distance)
            {
                AllCut++;
                startCut = true;
                print("старт нарезания");
            }
            else
            {

                print("не попали в начало");
            }
        }
    }

    private void OnMouseDrag()
    {
        if (startCut == true)
        {
            
        }
    }

    private void OnMouseExit()
    {
        if(Input.anyKey)
        {
        Vector3 MousePoint = camera.ScreenToWorldPoint(Input.mousePosition);
        if (startCut == true)
        {
            startCut = false;
            if (MousePoint.x < endPoint.position.x + distance && MousePoint.x > endPoint.position.x - distance
                                                              && MousePoint.y < endPoint.position.y + distance &&
                                                              MousePoint.y > endPoint.position.y - distance)
            {
                print("Mouse" + MousePoint);
                print("endpos" + endPoint.position);
                print("успешно отрезали");
                GetComponentInParent<Statue>().DestroyPiece(SuccessCut);
                SuccessCut++;
            }
            else
            {
                print("неудачная попытка");
            }
        }
        }
        
        // Vector3 MousePoint = camera.ScreenToViewportPoint(Input.mousePosition);
        // print("Mouse"+MousePoint);
        // print("start point"+endPoint.position);
        // print("Я выхожу");
    }
}
