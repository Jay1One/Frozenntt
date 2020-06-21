using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None=-1,
    Left=0,
    Right=1,
    Up=2,
    Down=3,
}
        
public struct Neighbor
{
    public Direction Direction;
    public MatchThree_Cell Cell;
}

public class MatchThree_Cell : MonoBehaviour
{
    [NonSerialized] public MatchThree_Candy Candy;
    
    private readonly  Neighbor[] neighbors=new Neighbor[4];

    public MatchThree_Cell GetNeighbour(Direction direction)
    {
        if (direction < 0)
        {
            return null;
        }

        int nm = (int) direction;
        int dir = (int) neighbors[nm].Direction;
        return dir >= 0 ? neighbors[nm].Cell : null;
    }

    public void SetNeighbour(Direction direction, MatchThree_Cell cell)
    {
        if (GetNeighbour(direction))
        {
            return;
        }

        neighbors[(int) direction] = new Neighbor()
        {
            Direction = direction,
            Cell =cell
        };
    }
 
        
}
