using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public bool isSolved = false;
    [SerializeField] private GameObject[] DestructablePieces;
    [SerializeField] private Bubble bubblePrefab;
    [SerializeField] private float BubbleSpawnInterval = 3f;
    [SerializeField] private GameObject NotCuting;
    private Vector3[] BubblePositions;
    private bool firstStatue = true;
    private int piecesDestroyed = 0;

    public void StartSolving()
    {
        StartCoroutine(SpawnBubbles());

    }

    public void ShowSwipeLine(int index)
    {
        DestructablePieces[index].GetComponent<LineRenderer>().enabled=true;
        FrozentGameManager.SwipeLine = DestructablePieces[piecesDestroyed].GetComponent<SwipeLine>();
    }

    public void DestroyPiece(int index)
    {
        piecesDestroyed++;
        //DestructablePieces[index].GetComponent<Rigidbody>().useGravity = true;
       // DestructablePieces[index].GetComponent<Rigidbody>().AddForce(Vector3.up*20f,ForceMode.Impulse);
        Destroy(DestructablePieces[index]);
        print(piecesDestroyed + "pd");
        print(DestructablePieces.Length-1);
        print(piecesDestroyed>=DestructablePieces.Length-1);
        if (piecesDestroyed>=DestructablePieces.Length)
        {
            isSolved = true;
            print("Solved");
          
                FrozentGameManager.currentState = FrozenTGameState.StartMovingCamera;
                //NotCuting.GetComponent<Rigidbody>().useGravity = true;
                Destroy(NotCuting);

        }
    }

    private IEnumerator SpawnBubbles()
    {
        print(DestructablePieces.Length);
        for (int i = 0; i < DestructablePieces.Length; i++)
        {
            var bubble = (Bubble)Instantiate(bubblePrefab, BubblePositions[i], Quaternion.identity);
            bubble.CurrentStatue = this;
            bubble.pieceNumber = i;
            DestructablePieces[i].GetComponent<SwipeLine>().Drawing();
            yield return new WaitForSeconds(BubbleSpawnInterval);
            
        }

        yield return null;

    }

    private void Start()
    {
        BubblePositions=new Vector3[DestructablePieces.Length];
        for (int i = 0; i < DestructablePieces.Length; i++)
        {
            // BubblePositions[i] = transform.GetChild(i).GetChild(0).position;
                 // print("1"+BubblePositions[i]);
           BubblePositions[i]= DestructablePieces[i].GetComponent<SwipeLine>().points[0].transform.position;
           
        }
           
        
    }

    private bool IsFinished()
    {
        return true;
        bool result = true;

        foreach (var piece in DestructablePieces)
        {
            if (piece!=null)
            {
                result = false;
                return result;
            }
        }

        return result;
    }
}
