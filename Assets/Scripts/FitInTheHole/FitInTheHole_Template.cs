using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class FitInTheHole_Template : MonoBehaviour
{
    //сгенеренные кубики
    [SerializeField] private Transform[] m_Cubes;
    //трансформы для кубика игрока
    [SerializeField] private Transform[] m_PositionVariants;
        //позиции кубиков и позиции кубика игрока, скорее всего не нужны просто изначально не получилось хранить сразу в трансформах можно оптимизировать 
    [SerializeField] private List<Vector3> m_ListPositionVariants;
    [SerializeField] private List<Vector3> m_ListCubesPosition;
        //позиция игрока
    [SerializeField] private GameObject m_PlayerPosition;
    //подсказка
   
    private int currentPosition;
    private FitInTheHole_FigureTweener tweener; //экземпляр класса, отвечающий за повороты кубика
    private bool left = false;
    private bool right = true;
    public Transform CurrentTarget { get; private set; }
    //public Transform TempTransform { get; private set; }

    /// <summary>
    /// Получение массива со всеми блоками фигуры
    /// </summary>
    /// <returns></returns>
    public Transform[] GetFigure()
    {
        var figure = new Transform[m_Cubes.Length + 1];
        m_Cubes.CopyTo(figure, 0);
        figure[figure.Length - 1]= CurrentTarget;
        return figure;
    }

    
    /// <summary>
    /// Постройка случайной фигуры
    /// </summary>
    public void SetupRandomFigure()
    {
        //попробуем сделать генаратор фигур просто ограничив количеством кубиков
        
        //уничтожаем оставшиеся объекты от прошлой фигуры   
        if (m_Cubes.Length!=0)
        {
            foreach (var cube in m_Cubes)
            {
                Destroy(cube.gameObject);
            }
          //  m_Cubes = null;
            
        }

        if (m_PositionVariants.Length!=0)
        {
            foreach (var position in m_PositionVariants)
            {
                Destroy(position.gameObject);
            }
            // m_PositionVariants = null;
        }

        if (m_PlayerPosition!=null)
        {
            Destroy(m_PlayerPosition.gameObject);
        }
    
        int numberOfBaseCubes = Random.Range(3, 8);
        m_ListCubesPosition=new List<Vector3>();
        m_ListPositionVariants=new List<Vector3>();
        int numberOfOccupiedColumn =0;
        if (numberOfBaseCubes <= 7)
        {
          
            numberOfOccupiedColumn = Random.Range(1 + Mathf.RoundToInt(f: numberOfBaseCubes / 5),numberOfOccupiedColumn);
        }
        else
        {
            numberOfOccupiedColumn = Random.Range(1 + Mathf.RoundToInt(f: numberOfBaseCubes / 5),7);
        }
        List<string> occupiedColumn=new List<string>(numberOfOccupiedColumn);
        
        
        //найдём столбцы которые будем заполнять 
        for (int i = 0; i < numberOfOccupiedColumn; i++)
        {
            int columnIndex = Random.Range(1, 8);
            while (occupiedColumn.Contains(columnIndex.ToString()))
            {
                columnIndex = Random.Range(1, 8);
            }
            occupiedColumn.Add(columnIndex.ToString()); 
            
        }
        occupiedColumn.Sort();
        //создаём фигуру
        
        int takingCubes = 0;
        
       
        m_Cubes=new Transform[numberOfBaseCubes];
        while (takingCubes!=numberOfBaseCubes)
        {
            int indexColumn = Random.Range(0, occupiedColumn.Count);
            var cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),
                    new Vector3(Int64.Parse(occupiedColumn[indexColumn])-4f, 0.5f, -20f), Quaternion.identity);
                m_Cubes[takingCubes] =cube.transform;
                cube.name = takingCubes.ToString();
                
                for (int i = 0; i < takingCubes; i++)
                {
                    if (m_ListCubesPosition[i].y == 3.5)
                    {
                        continue;  
                    }
                    if (m_ListCubesPosition[i]==cube.transform.position )
                    {
                        
                        cube.transform.position+=new Vector3(0f,1f,0f);
                    }
                }
              
                Destroy(cube);
                m_Cubes[takingCubes] = Instantiate(cube, cube.transform.position, Quaternion.identity).transform;
                m_ListCubesPosition.Add(cube.transform.position);
                
                takingCubes++;
        }
        //заполняем варианты позиций
        foreach (var cube in m_ListCubesPosition)
        {
            if (!m_ListCubesPosition.Contains(new Vector3(cube.x - 1, cube.y, cube.z)))
            {
                m_ListPositionVariants.Add(new Vector3(cube.x - 1, cube.y, cube.z));
            }
            if(!m_ListCubesPosition.Contains(new Vector3(cube.x + 1, cube.y, cube.z)))
            {
                m_ListPositionVariants.Add(new Vector3(cube.x + 1, cube.y, cube.z));
            }
            if(!m_ListCubesPosition.Contains(new Vector3(cube.x , cube.y+1, cube.z)))
            {
                m_ListPositionVariants.Add(new Vector3(cube.x , cube.y+1, cube.z));
            }
        }
        //пройдёмся по нижней линии и заполним если таковые варианты отсутствуют
        for (float i = -4f; i <=4 ; i++)
        {
        
            if (!m_ListPositionVariants.Contains(new Vector3(i, 0.5f, -20f)) && !m_ListCubesPosition.Contains(new Vector3(i, 0.5f, -20f)))
            {
                
                m_ListPositionVariants.Add(new Vector3(i,0.5f,-20f));
            }
            
        }
        //создали массив position variant
        int counter = 0;
        m_PositionVariants=new Transform[m_ListPositionVariants.Count];
        foreach (var pos in m_ListPositionVariants)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var positionVariant = Instantiate(cube, pos, Quaternion.identity);
            positionVariant.name = $"positionVariant({counter})";
            positionVariant.GetComponent<MeshRenderer>().material.color=Color.red;
            m_PositionVariants[counter] = Instantiate(positionVariant).transform;
            Destroy(positionVariant);
            Destroy(cube);
            counter++;
        }
        //ставим игрока в рандомную позицию
        int rand= Random.Range(0, m_PositionVariants.Length);
        m_PlayerPosition = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),
            m_PositionVariants[rand].transform.position,
            Quaternion.identity);
        m_PlayerPosition.GetComponent<MeshRenderer>().material.color=Color.magenta;
        rand = Random.Range(0, m_PositionVariants.Length);
        for (int i = 0; i < m_PositionVariants.Length; i++)
        {
            if (i == rand)
            {
                //проверка на подсказку
                CurrentTarget = m_PositionVariants[i].transform;
                if (FindObjectOfType<FitInTheHole_LevelGenerator>().GetPrompt())
                {
                    
                    m_PositionVariants[i].gameObject.SetActive(true);
                    continue;
                }

               
                
            }
        
            m_PositionVariants[i].gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (tweener)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
    }
    
    //методы передвижения на костылях, как быстро сделать по другому не придумал
    //для определения вращения передаём дополнительно направление в какую сторону мы двигаемся
    private void MoveLeft()
    {
        Vector3 pos = m_PlayerPosition.transform.position;
        if (m_PlayerPosition.transform.position.x == -5f)
        {
            return;
        }

        //проверим находится ли слева кубик, если нет то просто подвинем влево иначе  вверх
         tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
         if(pos.y>0.5f && !m_ListCubesPosition.Contains(new Vector3(pos.x - 1f, pos.y,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x , pos.y-1f,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x -1, pos.y-1f,
             pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(-1f,0f,0f) ,left);
         }
         else if (pos.y > 0.5f && !m_ListCubesPosition.Contains(new Vector3(pos.x - 1f, pos.y,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x, pos.y - 1f,
             pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(-1f,0f,0f),left);
             tweener.Tween(pos,pos+=new Vector3(0f,-1f,0f),left);
         }
         else if(pos.y>0.5f && !m_ListCubesPosition.Contains(new Vector3(pos.x - 1f, pos.y,
             pos.z)) && !m_ListCubesPosition.Contains(new Vector3(pos.x , pos.y-1f,
             pos.z))) 
         {
             tweener.Tween(pos,pos+=new Vector3(0f,-1f,0f),left);
         }
         else if (m_ListCubesPosition.Contains(new Vector3(pos.x - 1f, pos.y,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x-1f,pos.y+1f, pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(0f,1f,0f),left);
         }
         else if (m_ListCubesPosition.Contains(new Vector3(pos.x - 1f, pos.y,
             pos.z)) && !m_ListCubesPosition.Contains(new Vector3(pos.x-1f,pos.y+1f, pos.z)))
         {
            
             tweener.Tween(pos,pos+=new Vector3(-1f,0f,0f) ,left);
             tweener.Tween(pos,pos+=new Vector3(0f,1f,0f),left);
         }
         else
         {
             tweener.Tween(pos,pos+=new Vector3(-1f,0f,0f) ,left);
         }
         
         
        
        
    }


    private void MoveRight()
    {
        if (m_PlayerPosition.transform.position.x == 5f)
        {
            return;
        }
        Vector3 pos = m_PlayerPosition.transform.position;
        // currentPosition -= 1;
         tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
         if(pos.y>0.5f && !m_ListCubesPosition.Contains(new Vector3(pos.x + 1f, pos.y,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x , pos.y-1f,
             pos.z))&& m_ListCubesPosition.Contains(new Vector3(pos.x +1f, pos.y-1f,
             pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(+1f,0f,0f),right);
   
         }
         else if(pos.y>0.5f && !m_ListCubesPosition.Contains(new Vector3(pos.x + 1f, pos.y,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x , pos.y-1f,
             pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(+1f,0f,0f),right);
             tweener.Tween(pos,pos+=new Vector3(0f,-1f,0f),right);
         }
         else if(pos.y>0.5f && !m_ListCubesPosition.Contains(new Vector3(pos.x + 1f, pos.y,
             pos.z)) && !m_ListCubesPosition.Contains(new Vector3(pos.x , pos.y-1f,
             pos.z))) 
         {
             tweener.Tween(pos,pos+=new Vector3(0f,-1f,0f),right);
         }
         else if (m_ListCubesPosition.Contains(new Vector3(pos.x + 1f, pos.y,
             pos.z)) && m_ListCubesPosition.Contains(new Vector3(pos.x+1f,pos.y+1f, pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(0f,1f,0f),right);
         }
         else if (m_ListCubesPosition.Contains(new Vector3(pos.x + 1f, pos.y,
             pos.z)) && !m_ListCubesPosition.Contains(new Vector3(pos.x+1f,pos.y+1f, pos.z)))
         {
             tweener.Tween(pos,pos+=new Vector3(+1f,0f,0f) ,right);
             tweener.Tween(pos,pos+=new Vector3(0f,1f,0f),right);
         }
         else
         {
             tweener.Tween(pos,pos+=new Vector3(+1f,0f,0f) ,right);
         }
    }

    public bool CheckCoincedence()
    {
        if (CurrentTarget.transform.position != m_PlayerPosition.transform.position)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }
    // private bool IsMovementPossible(int dir)
    // {
    //     return currentPosition + dir >= 0 && currentPosition + dir < m_PositionVariantsRandomGenerated.Length;
    // }
}