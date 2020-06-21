using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private HopPlayer m_player;
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private AnimationCurve m_Curve;
    
    private  List<GameObject> platforms=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //генерация платформ
        platforms.Add(m_Platform);
        for (int i = 0; i < 25; i++)
        {
         
            GameObject obj = Instantiate(m_Platform, transform);
            Vector3 pos=Vector3.zero;
            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);
            obj.transform.position = pos;

            obj.name = $"Platform {i}";
            platforms.Add(obj);
        }
        FindObjectOfType<HopFeaturesPlatform>().AddFeatures(platforms);
    }

    public bool IsBallOnPlatform(Vector3 position)
    {
        position.y = 0f;
        GameObject nearestPlatform = platforms[0];
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].transform.position.z+0.5f<position.z)
            {
                continue;
            }
            if (platforms[i].transform.position.z-position.z>1f)
            {
                continue;
            }
            
            nearestPlatform = platforms[i];
            if (nearestPlatform.GetComponent<MeshRenderer>().material.color == Color.magenta)
            {
                m_player.ResetParametrs();
                m_player.ChangeBallSpeed(2f);
            }
            else if(nearestPlatform.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                m_player.ResetParametrs();
                m_player.ChangeBallSpeed(1f);
            }
            else if(nearestPlatform.GetComponent<MeshRenderer>().material.color == Color.yellow)
            {
                m_player.ResetParametrs();
                m_player.ChangeJumpHeight(2f);
            }
            else if(nearestPlatform.GetComponent<MeshRenderer>().material.color == Color.green)
            {
                m_player.ResetParametrs();
                m_player.ChangeJumpHeight(1f);
            }
            break;
            
        }

        
        //var clone = Instantiate(nearestPlatform, transform);
        // nearestPlatform.SetActive(false);
        // clone.transform.localScale = new Vector3(nearestPlatform.transform.position.x + 0.2f,
        //     nearestPlatform.transform.position.y + 0.2f, nearestPlatform.transform.position.z + 0.2f);
        // clone.GetComponent<MeshRenderer>().material.color=Color.cyan;
        //nearestPlatform.GetComponent<MeshRenderer>().material.color=Color.black;
       
        //nearestPlatform.transform.localScale.Set(m_Curve.Evaluate(2f),m_Curve.Evaluate(0.5f),1f);
        //nearestPlatform.SetActive(false);
        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;
        return position.x>minX && position.x<maxX;
    }
}
