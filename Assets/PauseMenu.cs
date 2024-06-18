using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject leaderboardManger;
    private LeaderboardManger _leaderboardManger;

    public void Start()
    {
        _leaderboardManger = leaderboardManger.GetComponent<LeaderboardManger>();
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home() {
        SceneManager.LoadScene(0);
        _leaderboardManger.SaveLeaderboard();
        Time.timeScale = 1;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
