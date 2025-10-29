using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenùManager : MonoBehaviour
{
    [SerializeField] private AudioSource maxAudio;
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _shop;

    private void Start()
    {
        _panelOptions.SetActive(false);
    }

    public void PlayButtonSound()
    {
        maxAudio.PlayOneShot(_buttonSound);
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        int extraLives = SaveSystem.Instance?.SaveData?.extraLives ?? 0;
        SaveSystem.Instance.SaveShopData(0, extraLives);
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        SaveSystem.Instance.LoadGame();
    }
   
    public void Settings()
    {
        _panelOptions.SetActive(true);
    }

    public void Shop()
    {
        _shop.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseOptions()
    {
        _panelOptions.SetActive(false);
    }

    public void CloseShop()
    {
        _shop.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
