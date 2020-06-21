using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_stick;
    [SerializeField] private StickHeroPlayer m_player;
    [SerializeField] private StickHeroPlatform[] m_platforms;

    private int counterScore; //счётчик результата
    
    //создаём переменную которая будет хранить максимальную длинну стика
    public double maxDistanceOfStick;
    // сделаем бесконечную генерацию платформ за счёт переставления уже существующих на сцене, просто добавив один счётчик контретно для платформ
    //и переставлять будем при помощи индекса самой дальней платформы
    private int counterPlatform;
    private int indexDistantPlatform;
    public enum GameState
    {
        wait,
        scaling,
        rotate,
        movement,
        defeat,
    }

    private GameState currentGameState;

   
    void Start()
    {
        //установим дефолтное значение стика
        maxDistanceOfStick = m_platforms[1].transform.position.x + m_platforms[1].GetPlatformSize()*0.5f-m_platforms[0].GetStickPosition().x;
        print(m_platforms[1].transform.position.x);
        indexDistantPlatform = 0;
        currentGameState = GameState.wait;
        counterScore = 0;
        counterPlatform = 0;
        m_stick.ResetStick(m_platforms[0].GetStickPosition());
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) ==false)
        {
            return;
        }
        
        switch (currentGameState)
        {
            case GameState.wait: //если не осуществлён старт игры
                currentGameState = GameState.scaling;
                m_stick.StartScaling();
                break;
            //если stick увеличивается прерываем увелечение , и запускаем оборот
            case GameState.scaling:
                currentGameState = GameState.rotate;
                m_stick.StopScaling();
                break;
                    //ничего не делаем
            case GameState.rotate:
                break;
                    //ничего не делаем
            case GameState.movement:
                break;
                
            case GameState.defeat:
                print("defeat");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

  
    public void StopStickScale()
    {
        currentGameState = GameState.rotate;
        m_stick.StartRotate();
    }

    public void StopStickRotate()
    {
        currentGameState = GameState.movement;
    }

    public void StartPlayerMovement(float leangth)
    {
        currentGameState = GameState.movement;
       
        StickHeroPlatform nextPlatform;
        if (counterPlatform == 4)
        {
            
            nextPlatform = m_platforms[0];
        }
        else
        {
            nextPlatform = m_platforms[counterPlatform + 1];
        }
        
        //мы будем искать минимальную длинну стика которая нужна для дохода
        
        float targetLength = nextPlatform.transform.position.x;
        float platformSize = nextPlatform.GetPlatformSize();
        float min = targetLength - platformSize * 0.5f;
        min -= m_player.transform.localScale.x * 0.9f;
        //ищем максимальную длинну для перехода
        float max = targetLength + platformSize * 0.5f;
        
            //для удобства тестирования каждый раз считаем успешным
        
        // //при успехе переходим в центр следующей платформы иначе падаем
        // if (leangth < min || leangth > max)
        // {
        //     //будем падать
        //     float targetPosition = m_stick.transform.position.x + leangth + m_player.transform.localScale.x;
        //     m_player.StartMovement(targetPosition,true);
        // }
        // else
        // {
            float targetPosition = nextPlatform.transform.position.x;
            m_player.StartMovement(targetPosition,false);
        //     
        // }
    }
    
    //изменим метод остановки
    public void StopPlayerMovement()
    {
        currentGameState = GameState.wait;
       
        counterScore++;
        
            //пробегаемся по всем платформам и меняем индекс
        for (int i=-1;i<m_platforms.Length-1;i++ )
        {
            if (m_platforms[indexDistantPlatform].transform.position.x < m_platforms[i+1].transform.position.x)
            {
                indexDistantPlatform = i + 1;
            }
           
            
        }
      
        //переставляем платформу и меняем её scale
        m_platforms[counterPlatform].transform.position =new Vector3( Random.Range(m_platforms[indexDistantPlatform].transform.position.x+0.7f, m_platforms[indexDistantPlatform].transform.position.x+1.0f),m_platforms[indexDistantPlatform].transform.position.y);
        m_platforms[counterPlatform].transform.localScale=new Vector3(Random.Range(0.3f, 0.6f),m_platforms[indexDistantPlatform].transform.localScale.y,1f);

        print("позиция платформы:" + m_platforms[counterPlatform].transform.position.x);
        if (counterPlatform == 3)
        {
            maxDistanceOfStick = m_platforms[0].transform.position.x +
                m_platforms[0].GetPlatformSize() * 0.5f - m_platforms[4].GetStickPosition().x;
        }
        else if(counterPlatform==4)
        {
            maxDistanceOfStick = m_platforms[1].transform.position.x +
                m_platforms[1].GetPlatformSize() * 0.5f - m_platforms[0].GetStickPosition().x;
        }
        else
        {
            maxDistanceOfStick = m_platforms[counterPlatform+2].transform.position.x +
                m_platforms[counterPlatform+2].GetPlatformSize() * 0.5f - m_platforms[counterPlatform+1].GetStickPosition().x;
        }
     
        print(maxDistanceOfStick);
        counterPlatform++;
        //если дошли до последней платформы сбрасываем счётчик так как следующая платформа в массиве будет нулевой по номеру
        
       
        if (counterPlatform == 5)
        {
            counterPlatform = 0;
           
        }
    
        
        print("Мы находимся на платформе" +counterPlatform);
       
        m_stick.ResetStick(m_platforms[counterPlatform].GetStickPosition());
        

       
    }

    public void ShowScores()
    {
        currentGameState = GameState.defeat;
        print($"Game over : {counterScore}");
    }
}
