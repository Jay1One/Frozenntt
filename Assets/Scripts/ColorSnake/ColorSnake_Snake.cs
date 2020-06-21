using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSnake_Snake : MonoBehaviour
{
    [SerializeField] private ColorSnake_GameController m_GameController;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private int currentType;
        //счётчики для определения победы или поражения
    private static int commonScore = 0;
    private static int score = 0;

    private Vector3 position;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        position = transform.position;

        var cololrType = m_GameController.Types.GetRandomColorType();
        currentType = cololrType.Id;
        m_SpriteRenderer.color = cololrType.Color;
    }

    // Update is called once per frame
    private void Update()
    {
        position = transform.position;
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        position.x = m_GameController.Camera.ScreenToWorldPoint(Input.mousePosition).x;

        position.x = Mathf.Clamp(position.x, m_GameController.Bounds.Left, m_GameController.Bounds.Right);
        transform.position = position;
    }

    private void SetupColor(int id)
    {
        var cololrType = m_GameController.Types.GetColorType(id);
        currentType = cololrType.Id;
        m_SpriteRenderer.color = cololrType.Color;
    }

 
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        var obstacle = other.gameObject.GetComponent<ColorSnacke_Obstacle>();
        if (obstacle == null)
        {
            return;
        }
        //проверки на совпадение цвета и проверка с кем мы столкнулись
        if(m_SpriteRenderer.color!=obstacle.GetComponent<SpriteRenderer>().color && !other.gameObject.CompareTag("Wall"))
        {
            commonScore++;
            print("не тот цвет");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(m_SpriteRenderer.color!=obstacle.GetComponent<SpriteRenderer>().color && other.gameObject.CompareTag("Wall"))
        {
            SetupColor(obstacle.ColorId);
        }
        
        if(m_SpriteRenderer.color==obstacle.GetComponent<SpriteRenderer>().color)
        {
            commonScore++;
            score++;
                //если разница между всеми столкнувшимися объектами и приемлемыми >3 поражение иначе победа
            if(score==10 && commonScore<13)
            {
                print("You win");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if(score==10 && commonScore>=13)
            {
                print("You lose");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
       SetupColor(obstacle.ColorId);
       Destroy(obstacle.gameObject);
        
    }
}
