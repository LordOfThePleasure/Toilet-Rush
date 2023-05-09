using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Runner[] currentRunners;
    private int finishedRunnersAmount;

    public static GameManager instance;


    [SerializeField] private UnityEvent onVictory;
    [SerializeField] private UnityEvent onGameOver;

    private void Awake()
    {
        instance = this;
    }

    public void GetToGate()
    {
        finishedRunnersAmount++;

        if (finishedRunnersAmount >= currentRunners.Length)
        {
            onVictory?.Invoke();
            finishedRunnersAmount = 0;
        }
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
        finishedRunnersAmount = 0;
    }

    public void CheckPathsComplition()
    {
        foreach (var runner in currentRunners)
        {
            if (!runner.hasPath)
            {
                return;
            }
        }

        foreach (var runner in currentRunners)
        {
            runner.moving = true;
        }
    }
}
