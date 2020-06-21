using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake_LevelGenerator : MonoBehaviour
{
    [SerializeField] private ColorSnake_Types m_Types;

    [SerializeField] private ColorSnake_GameController m_Controller;

    private int line = 1;// номер генерируемого ченка
    
    private List<GameObject> obstacles=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        var upBorder = m_Controller.Bounds.Up;

        while (line*2<upBorder+2f)
        {
            GenerateObstacle();    
        }
    }

    // Update is called once per frame
    void Update()
    {
        var upBorder = m_Controller.Bounds.Up + m_Controller.Camera.transform.position.y;
        if (line * 2 > upBorder + 2f)
        {
            return;
        }

        GenerateObstacle();
        
        //TODO уничтожение нижних объектов написать самостоятельно
        //удаление в методе generateObstackle через Destroy
    }

    private void GenerateObstacle()
    {
        var template = m_Types.GetRandomTemplate();
        var obstackle=new GameObject($"Obstacle_{line}");
        foreach (var point in template.Points)
        {
            var objType = m_Types.GetRandomObjectType();
            
            var colorType = m_Types.GetRandomColorType();
            var obj = Instantiate(objType.Object, point.position, point.rotation);
            obj.transform.parent = obstackle.transform;
            Destroy(obj,20f);
                //если это стена то добавим ей тег Wall 
            if (objType.Id == 4)
            {
                obj.tag = "Wall";
            }
            //если сейчас линия кратная 4 и это не стнеа то будем вращать это препятствие, очень плохое решение на скорую руку
            if (line % 4 == 0 && objType.Id!=4)
            {
                obj.AddComponent<ColorSnake_RotateObstackle>();
            }
            obj.GetComponent<SpriteRenderer>().color = colorType.Color;

            var obstacleComponent = obj.AddComponent<ColorSnacke_Obstacle>();
            obstacleComponent.ColorId = colorType.Id;
            
        }

        Vector3 pos = obstackle.transform.position;
        pos.y = line * 2.2f;
        obstackle.transform.position = pos;
        line++;
        obstacles.Add(obstackle);
            //удаляем корневой элемент
        Destroy(obstackle, 20f);

    }
}
