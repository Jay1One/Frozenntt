using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            GameManager.SetGameState(GameState.MainMenu);
        }

        public void LoadLevel(string level)
        {
            SceneLoader.LoadLevel(level);
        }
        
    }
