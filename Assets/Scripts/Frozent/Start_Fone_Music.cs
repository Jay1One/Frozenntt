using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Fone_Music : MonoBehaviour {
    //надо было это всё через менеджер делать
    private void Awake()
    {
      
        if(!PlayerPrefs.HasKey("Music_Volume"))
            {
            
            PlayerPrefs.SetFloat("Music_Volume", 1f);
            PlayerPrefs.Save();
            }
    }
    // Use this for initialization
    void Update ()
    {
    
        if (PlayerPrefs.GetFloat("Music_Volume") != 0f && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music_Volume");
            GetComponent<AudioSource>().PlayDelayed(2f);
        }
        if(PlayerPrefs.GetFloat("Music_Volume") == 0 && GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
