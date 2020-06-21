using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public enum FrozenTGameState
{
    StartPause,
    Paused,
    StartCuttingStatue,
    CuttingStatue,
    StartMovingCamera,
    MovingCamera,
    StartDefeat,
    Defeat,
    StartWinning,
    Winning,

}

public class FrozentGameManager : MonoBehaviour
{
    public static FrozenTGameState currentState = FrozenTGameState.Paused;

    [SerializeField] private float InitialSolvingTimer;
    [SerializeField] private Statue[] statues;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float cameraMoveSpeed;
    
    [SerializeField] private AudioSource loseMusic;
    [SerializeField] private AudioSource winMusic;
    [SerializeField] private AudioSource movingCameraMusic;
    [SerializeField] private GameObject CanvasLose;
    
    public static int fails=0;

    public static SwipeLine SwipeLine;

    static float CurrentSolvingTimer;
    private int currentStatueNumber;
    private Vector3 previousCameraPosition;
    private Vector3 nextCameraPosition;
    private float moveCameraTimer;
    

    private void Update()
    {
        switch (currentState)
        {
            case FrozenTGameState.StartPause:
                StartPause();
                break;
            case FrozenTGameState.Paused:
                Pausing();
                break;
            case FrozenTGameState.StartCuttingStatue:
                StartCuttingStatue();
                break;
            case FrozenTGameState.CuttingStatue:
                CuttingStatue();
                break;
            case FrozenTGameState.StartMovingCamera:
                StartMovingCamera();
                break;
            case FrozenTGameState.MovingCamera:
                MovingCamera();
                break;
            case FrozenTGameState.StartDefeat:
                StartDefeat();
                break;
            case FrozenTGameState.Defeat:
                Defeat();
                break;
            case FrozenTGameState.StartWinning:
                StartWinning();
                break;
            case FrozenTGameState.Winning:
                Winning();
                break;
        }
    }

    private void Start()
    {
        currentStatueNumber = 0;
        previousCameraPosition = mainCamera.transform.position;
    }

    private void StartPause()
    {
        
    }

    private void Pausing()
    {
        
    }

    private void StartCuttingStatue()
    {
        print("StartCut");
        CurrentSolvingTimer = InitialSolvingTimer;
        currentState = FrozenTGameState.CuttingStatue;
        statues[currentStatueNumber].StartSolving();
    }

    private void CuttingStatue()
    {
        if (fails > 3)
        {
            currentState = FrozenTGameState.StartDefeat;
            fails = 0;
        }
        else
        {
            currentState = FrozenTGameState.StartWinning;
            fails = 0;
        }
   //     if (statues[currentStatueNumber].isSolved)
     //   {
       //     if (currentStatueNumber+1==statues.Length)
         //   {
           //     currentState = FrozenTGameState.StartWinning;
             //   return;
            //}
            //else
           // {
             //   currentState = FrozenTGameState.StartMovingCamera;
            //}
       // }
        CurrentSolvingTimer -= Time.deltaTime;
        if (CurrentSolvingTimer<0)
        {
            currentState = FrozenTGameState.StartDefeat;
        }
    }

    private void StartMovingCamera()
    {
        movingCameraMusic.Play();
        Vector3 cameraOffset = statues[currentStatueNumber+1].transform.position -
                               statues[currentStatueNumber].transform.position;
        currentStatueNumber++;
        nextCameraPosition = mainCamera.transform.position + cameraOffset;
        moveCameraTimer = 0f;
        currentState = FrozenTGameState.MovingCamera;
    }

    private void MovingCamera()
    {
      
        mainCamera.transform.position =
            Vector3.Lerp(previousCameraPosition, nextCameraPosition, moveCameraTimer * cameraMoveSpeed);
        moveCameraTimer += Time.deltaTime;
        if (moveCameraTimer>1)
        {
            mainCamera.transform.position = nextCameraPosition;
            previousCameraPosition = nextCameraPosition;
            currentState = FrozenTGameState.StartCuttingStatue;
        }
    }

    private void Defeat()
    {

    }

    private void StartDefeat()
    {
        currentState = FrozenTGameState.Defeat;
        loseMusic.Play();
            CanvasLose.SetActive(true);
            
    }

    private void StartWinning()
    { 
        winMusic.Play();
        
    }

    private void Winning()
    {
        
    }

    public void AddSolvingTime(float time)
    {
        CurrentSolvingTimer += time;
    }
    
}
