using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCompression : MonoBehaviour
{
    [SerializeField] private GameObject[] bubbles;
    [SerializeField] private GameObject[] icePeaces;
    [SerializeField] private float compressionTime = 5f;

    [SerializeField] private GameObject lastPeaces;
    private Vector3 beginScale;
    private SphereCollider collider;
    private int index = 0;
    private float currentTime;
    
    
    public  IEnumerator Compress()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            icePeaces[i].GetComponent<SwipeLine>().enabled = true;
            if (i < bubbles.Length - 1)
            {
                bubbles[i+1].SetActive(true);
            }
            StartCoroutine(Compression(i));
            
            yield return new WaitForSecondsRealtime(compressionTime);
        }
        Destroy(lastPeaces);
        
    }


    private IEnumerator Compression(int index)
    {
        collider =bubbles[index].GetComponent<SphereCollider>();
        float currentTime=compressionTime;
        while (currentTime-Time.deltaTime>0)
        {
         
            // print(currentTime);
            currentTime -= Time.deltaTime;
            //print(currentTime/compressionTime);
            //StartCoroutine(SetScale(currentTime,index));
            collider.radius = Mathf.Lerp(0, collider.radius, currentTime / compressionTime);
            
             bubbles[index].transform.localScale=new Vector3(Mathf.Lerp(0f,beginScale.x,currentTime/compressionTime),
                 Mathf.Lerp(0f,beginScale.y,currentTime/compressionTime),
                 Mathf.Lerp(0f,beginScale.z,currentTime/compressionTime));
            // bubbles[index].transform.localScale-=new Vector3(bubbles[index].transform.localScale.x/Time.deltaTime,bubbles[index].transform.localScale.y/5f,bubbles[index].transform.localScale.z/5f);
            yield return null; 

        }

        icePeaces[index].GetComponent<Rigidbody>().useGravity = true;
        Destroy(icePeaces[index].GetComponent<LineRenderer>());
        Destroy(icePeaces[index].GetComponent<MeshCollider>());
        yield return null; 
        
    }
    // Start is called before the first frame update
    void Start()
    {
        beginScale = bubbles[0].transform.localScale;
        currentTime = compressionTime;
        index = 0;
        //StartCoroutine( Compress());
    }

}
