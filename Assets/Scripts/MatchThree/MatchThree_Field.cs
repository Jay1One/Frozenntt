using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchThree_Field : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private GameObject m_CellPrefab;
    [SerializeField] private float m_CellSize=0.25f;
    [SerializeField] private int m_FieldWidth = 6;
    [SerializeField] private int m_FieldHeight = 8;
    
    private static  readonly List<List<MatchThree_Cell>> GameField=new List<List<MatchThree_Cell>>();
    public static float CurrentCellSize;

    public void Init()
    {
        GenerateField(m_FieldWidth,m_FieldHeight);
        CurrentCellSize = m_CellSize;
    }

  

    private void GenerateField(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            GameField.Add(new List<MatchThree_Cell>());
            for (int y = 0; y < height; y++)
            {
                var pos=new Vector3(x*m_CellSize,y*m_CellSize,0f);
                var obj = Instantiate(m_CellPrefab, pos, Quaternion.identity);

                obj.name = $"Cell {x} {y}";

                var cell = obj.AddComponent<MatchThree_Cell>();
                
                GameField[x].Add(cell);
                
                //настройка соседей по горизонтали
                
                if (x > 0) 
                {
                    cell.SetNeighbour(Direction.Left,GameField[x-1][y]); //предыдущая левая клетка
                    GameField[x-1][y].SetNeighbour(Direction.Right,cell);   
                }
                else
                {
                    cell.SetNeighbour(Direction.Left,null);//крайняя левая клетка x==0
                    
                }

                if (x == width - 1) //крайняя правая
                {
                    cell.SetNeighbour(Direction.Right,null);
                }
                //настройка соседей по вертикали
                if (y > 0)
                {
                    cell.SetNeighbour(Direction.Down,GameField[x][y-1]);
                    GameField[x][y-1].SetNeighbour(Direction.Up,cell);
                }
                else
                {
                    cell.SetNeighbour(Direction.Down,null);
                }
                //крайняя верхняя клетка
                if (y == height - 1)
                {
                    cell.SetNeighbour(Direction.Up,null);
                }
            }
        }
        
        m_Camera.transform.position=new Vector3(width*m_CellSize*0.5f,height*m_CellSize*0.5f,-1f);
    }

    public static MatchThree_Cell GetCell(MatchThree_Candy candy)
    {
        foreach (var row in GameField)
        {
            foreach (var cell in row.Where(cell => cell.Candy == candy))
            {
                return cell;
            }
        }

        return null;
    }

    public static MatchThree_Cell GetCell(int x, int y)
    {
        return GameField[x][y];
    }
}

