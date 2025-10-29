using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    
    private void Awake()
    {
        victoryPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victoryPanel.SetActive(true);
            AudioManager.Instance.PlayWinSound();
            Time.timeScale = 0f;
        }
    }
}
