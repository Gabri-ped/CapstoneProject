using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private int maxCoins = 10;
    public int totalCoins = 0;

    public static CoinsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int value)
    {
        totalCoins += value;
        UpdateUI();
    }
    public void UpdateUI()
    {
        coinsText.text = "Coins " + totalCoins + "/ " + maxCoins;
    }
   
    public void SetCoins(int value)
    {
        totalCoins = value;
        UpdateUI();
    }
}
