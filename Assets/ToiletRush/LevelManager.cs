using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allLevels;

    public Level currentLevel;
    private int currentLevelIndex;

    private void Start()
    {
        NextLevel();
    }

    public void OpenLevel(int levelIndex)
    {
        if (levelIndex >= allLevels.Length)
        {
            Debug.LogWarning("Level index is greater then levels amount!");
            return;
        }

        DestroyCurrentLevel();
        currentLevel = Instantiate(allLevels[levelIndex]).GetComponent<Level>();
        GameManager.instance.currentRunners = currentLevel.allRunners;
    }

    public void RestartLevel()
    {
        OpenLevel(currentLevelIndex - 1);
    }

    public void NextLevel()
    {
        OpenLevel(currentLevelIndex++);
    }

    private void DestroyCurrentLevel()
    {
        if (currentLevel != null)
            Destroy(currentLevel.gameObject);
    }
}
