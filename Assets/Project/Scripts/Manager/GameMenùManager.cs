using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenùManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(2);
        AudioManager.Instance.StopVictorySound();
    }

    public void Retry2Level()
    {
        SceneManager.LoadScene(2);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic2);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        AudioManager.Instance.StopVictorySound();
        Time.timeScale = 1f;
    }
}
