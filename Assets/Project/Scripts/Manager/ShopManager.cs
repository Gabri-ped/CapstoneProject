using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] Button buyLifeButton;
    [SerializeField] private TextMeshProUGUI lifePriceText;
    [SerializeField] private TextMeshProUGUI extraLivesText;

    public int lifePrice = 5;
    public int extraLivesBought = 0;

    public static ShopManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (SaveSystem.Instance != null && SaveSystem.Instance.SaveData != null)
        {
            // Aggiorno solo le monete dal salvataggio
            if (CoinsManager.Instance != null)
                CoinsManager.Instance.SetCoins(SaveSystem.Instance.SaveData.coins);
            else
                Debug.Log("[ShopManager] CoinsManager non trovato, uso dati salvati.");
        }

        UpdateUI();
        if (SaveSystem.Instance != null && SaveSystem.Instance.SaveData != null)
            extraLivesBought = SaveSystem.Instance.SaveData.extraLives;

        buyLifeButton.onClick.AddListener(BuyExtraLife);
    }

    void UpdateUI()
    {
        int coinsToShow = SaveSystem.Instance?.SaveData?.coins ?? 0;

        // Se CoinsManager esiste, prendo i dati in tempo reale
        if (CoinsManager.Instance != null)
        {
            coinsToShow = CoinsManager.Instance.totalCoins;
        }
        // Altrimenti, prendo i dati salvati
        else if (SaveSystem.Instance != null && SaveSystem.Instance.SaveData != null)
        {
            coinsToShow = SaveSystem.Instance.SaveData.coins;
        }

        if (coinsText != null)

            coinsText.text = "Coins: " + coinsToShow;
            lifePriceText.text = " " + lifePrice;
            extraLivesText.text = " " + extraLivesBought;
    }

    public void BuyExtraLife()
    {
        if (SaveSystem.Instance == null) return;

        int currentCoins = SaveSystem.Instance.SaveData.coins;
        if (currentCoins >= lifePrice)
        {
            // scala monete
            SaveSystem.Instance.SaveData.coins -= lifePrice;

            // incrementa vite acquistate
            extraLivesBought++;
            SaveSystem.Instance.SaveData.extraLives = extraLivesBought;

            // salva
            SaveSystem.Instance.SaveShopData(SaveSystem.Instance.SaveData.coins, extraLivesBought);
            UpdateUI();

            Debug.Log($"Hai comprato una vita extra! Totale vite acquistate: {extraLivesBought}");
        }
        else
        {
            Debug.Log("Monete insufficienti!");
        }

        UpdateUI();
    }
}
