using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Buttons : MonoBehaviour {

    public Sprite current, changeSprite;
    void OnMouseDown()
    {
        if (PlayerPrefs.GetFloat("Music_Volume") != 0f && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music_Volume");
            GetComponent<AudioSource>().Play();
        }
       switch(gameObject.name)
        {
            case "Exit":        gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Confirm":     gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Return":      gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Apply":       gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Store":       gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Play":        gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "MusicOn":     gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "MusicOff":    gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Help":        gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "LidersTable": gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            case "Rate":        gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite; break;
            default:    break;
        }
    }
    void OnMouseUp()
    {
        switch (gameObject.name)
        {
            case "Exit":        gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Confirm":     gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Return":      gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Apply":       gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Store":       gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Play":        gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "MusicOn":     gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "MusicOff":    gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Help":        gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "LidersTable": gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            case "Rate":        gameObject.GetComponent<SpriteRenderer>().sprite = current; break;
            default: break;
        }
    }
}
