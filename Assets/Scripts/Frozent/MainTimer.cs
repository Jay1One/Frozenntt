using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTimer : MonoBehaviour
{
    private Text timerText;

    static float time;
    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<Text>();
        time = Int32.Parse(timerText.text);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerText.text =Mathf.RoundToInt(time).ToString();
        if (time <= 0)
        {
            print("You lose");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
