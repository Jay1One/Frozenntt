using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopFeaturesPlatform : MonoBehaviour
{
    [SerializeField] private HopPlayer m_player;
    public void AddFeatures(List<GameObject> platforms)
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            int features = Random.Range(1, 5);
            switch (features)
            {
            
                case 1: platforms[i].GetComponent<MeshRenderer>().material.color=Color.magenta; break;
                case 2: platforms[i].GetComponent<MeshRenderer>().material.color=Color.yellow;  break;
                case 3: platforms[i].GetComponent<MeshRenderer>().material.color=Color.red; break;
                case 4: platforms[i].GetComponent<MeshRenderer>().material.color=Color.green; break;
                case 5: break;
                default: break;
            }
        }
       


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
