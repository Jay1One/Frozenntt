using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class OnPlayClick : MonoBehaviour
{

    [SerializeField] private GameObject[] startObjects;
    [SerializeField] private GameObject[] objectsForInclude;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource startMusic;
    [SerializeField] private AudioSource firstStateMusic;
    private void Awake()
    {
      
    }

    public void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color=Color.red;
        startMusic.Stop();
        _audioSource.Play();
    }

    public void OnMouseUp()
    {
        firstStateMusic.Play();
        GetComponent<SpriteRenderer>().color=Color.white;
        FrozentGameManager.currentState = FrozenTGameState.StartCuttingStatue;
        foreach (var el in objectsForInclude)
        {
            el.SetActive(true);
        }
        foreach (var VARIABLE in startObjects)
        {
            VARIABLE.SetActive(false);
        }

  
        // if (PlayerPrefs.GetInt("Energy")>0)
        // {
        //     
        //     PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") - 1);
        //     SceneManager.LoadScene("BeginCoding1");
        // }
        // else if (PlayerPrefs.GetInt("Money") > 100)
        // {
        //     PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 100);
        //     SceneManager.LoadSceneAsync("BeginCoding1");
        // }
        // else
        // {
        //     if (Advertisement.IsReady())
        //     {
        //
        //         Advertisement.Show();
        //         SceneManager.LoadScene("BeginCoding1");
        //     }
        //
    }
  
}
