using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public float speed = 3f;
    private CoinID coinID;

    private void Awake()
    {
        coinID = GetComponent<CoinID>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayCoinSound();
            CoinsManager.Instance.AddCoins(coinValue);
            if (SaveSystem.Instance != null && SaveSystem.Instance.SaveData != null && coinID != null)
            {
                if (!SaveSystem.Instance.SaveData.collectedCoins.Contains(coinID.coinID))
                {
                    SaveSystem.Instance.SaveData.collectedCoins.Add(coinID.coinID);
                }
            }

            gameObject.SetActive(false);
             
        }
    }
    void Update()
    {
        transform.Rotate(0f, speed, 0f * Time.deltaTime / 0.01f, Space.Self);
    }
}
