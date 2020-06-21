using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class FitInTheHole_LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_CubePrefab;
    [SerializeField] private float m_BaseSpeed = 8f;
    [SerializeField] private float m_WallDistance = 20f;
    [SerializeField] private bool m_prompt=true;
    private float speed=6;
    private FitInTheHole_Wall wall;

    [SerializeField] private FitInTheHole_Template[] m_Templates;
    [SerializeField] private Transform m_FigurePoint;
    
    private FitInTheHole_Template[] templates;
    private FitInTheHole_Template figure;
    private void Start()
    {
        
        templates = new FitInTheHole_Template[m_Templates.Length];
        for (int i = 0; i < m_Templates.Length; i++)
        {
            templates[i] = Instantiate(m_Templates[i]);
            templates[i].gameObject.SetActive(false);
            templates[i].transform.position = m_FigurePoint.position;
        }
        
        wall=new FitInTheHole_Wall(5,5,m_CubePrefab);
        speed = m_BaseSpeed;
        SetupTemplate();
        wall.SetupWall(figure,m_WallDistance);
       
    }
    /// <summary>
    /// Настройка фигуры из шаблона
    /// </summary>
    private void SetupTemplate()
    {
        
        if (figure != null)
        {
            figure.gameObject.SetActive(false);
        }

        var rand = 0;
        figure = templates[rand];
        figure.gameObject.SetActive(true);
        figure.SetupRandomFigure();

    }

    public bool GetPrompt()
    {
        return m_prompt;
    }
    private void Update()
    {
      
        wall.Parent.transform.Translate(Time.deltaTime*speed*Vector3.back);
        if (wall.Parent.transform.position.z > figure.transform.position.z)
        {
            return;
        }
        //корявое увеличение скорости
        if (speed < 7.5f)
        {
         
            speed += 0.5f;
        }

        if (!figure.CheckCoincedence())
        {
            print("You lose");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        SetupTemplate();
        wall.SetupWall(figure,m_WallDistance);
    }
}
