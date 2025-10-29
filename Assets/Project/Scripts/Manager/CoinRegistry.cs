using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRegistry : MonoBehaviour
{
    public static CoinRegistry Instance { get; private set; }

    public List<CoinID> allCoins = new List<CoinID>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DisableCollectedCoins(List<string> collectedIDs)
    {
        Debug.Log(allCoins.Count);
        foreach (var coin in allCoins)
        {
            if (collectedIDs.Contains(coin.coinID))
            {
                coin.gameObject.SetActive(false);
                Debug.Log("Moneta presente" + coin.coinID);
            }
        }
    }
}
