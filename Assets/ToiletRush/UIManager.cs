using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;

    public void Victory()
    {
        victoryPanel.SetActive(true);
        Pause(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Pause(true);
    }

    public void ClosePanels()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        Pause(false);
    }

    private void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }
}
