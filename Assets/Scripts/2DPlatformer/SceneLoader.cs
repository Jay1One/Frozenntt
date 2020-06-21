using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{
    private static string nextLevel;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;
    [SerializeField] private GameObject prompt;
    private float timer = 0;
    public static void LoadLevel(string level)
    {
        nextLevel = level;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator Start()
    {
        GameManager.SetGameState(GameState.Loading	);
        StartCoroutine(TimeLeft());
       
    
        yield return new WaitForSeconds	(2f);
        
        if ((string.IsNullOrEmpty(nextLevel)))
        {
            SceneManager.LoadScene("MainMenuScene");
            yield break;
        }
        
        AsyncOperation loading = null;

        loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

        while (!loading.isDone)
        {
            Debug.Log(loading.progress);
            //откладываем действие на 1 кадр
            yield return null;
        }
       
        nextLevel = null;
        SceneManager.UnloadSceneAsync("LoadingScene");
    }

    private IEnumerator TimeLeft()
    {
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            slider.SetValueWithoutNotify(timer);
            progressText.text = (int)(timer / 2 * 100) + "%";
            if (timer > 1f && !prompt.activeSelf)
            {
                prompt.SetActive(true);
            }
            yield return null;
        }

        yield return null;
    }
}
