using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FingerDriverTrack : MonoBehaviour
{
   public class TrackSegment
   {
       public Vector3[] points=new Vector3[3];

       public bool IsPointInSegment(Vector3 point)
       {
           return MathfTriangles.IsPointInTriangleXY(point,points[0],points[1],points[2]);
       }
   }

   [SerializeField] private LineRenderer m_lineRenderer;

   [SerializeField] private bool m_viewDebug;
    
   private Vector3[] corners;

   private TrackSegment[] segments;
    //Счётчик результата
    
   public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //заполняем массив опорных точек трассы
        corners=new Vector3[transform.childCount];
        for (int i  = 0; i  < corners.Length; i ++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            corners[i] = obj.transform.position;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        
        //задаём значения linerenderer
        m_lineRenderer.positionCount = corners.Length;
        m_lineRenderer.SetPositions(corners);
        
        //из полученного LineRenderer запекаем mesh
        Mesh mesh=new Mesh();
        m_lineRenderer.BakeMesh(mesh,true);
        
        //создаём массив сегментов трассы
        //каждый треугольник уписываем треугольниками из массива вершин
        segments = new TrackSegment[mesh.triangles.Length / 3];
        int segmentsCounter = 0;

        for (int i = 0; i < mesh.triangles.Length; i+=3)
        {
            segments[segmentsCounter]=new TrackSegment();
            segments[segmentsCounter].points=new Vector3[3];
            int nm = mesh.triangles[i];
            segments[segmentsCounter].points[0] = mesh.vertices[nm];
            nm = mesh.triangles[i + 1];
            segments[segmentsCounter].points[1] = mesh.vertices[nm];
            nm = mesh.triangles[i + 2];
            segments[segmentsCounter].points[2] = mesh.vertices[nm];
            segmentsCounter++;

        }

        if (!m_viewDebug)
        {
            return;
        }
        else
        {
            foreach (var segment in segments)
            {
                foreach (var point in segment.points)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = point;
                    sphere.transform.localScale=Vector3.one*0.1f;
                }
                
            }
        }
    }
/// <summary>
/// Определяем находится ли точка в пределах трассы
/// </summary>
/// <param name="point"></param>
/// <returns></returns>
    public bool IsPointInTrack(Vector3 point)
    {
        //переделал цикл foreach на for  и в качестве начала цикла используем score
        
        for (int i=score;i<segments.Length;i++)
        {
            if(segments[i].IsPointInSegment(point))
            {
                score = i;
                return true;
            }
        }
        
        return false;
    }
}
