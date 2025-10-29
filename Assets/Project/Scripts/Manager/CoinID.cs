using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinID : MonoBehaviour
{
    public string coinID;

    private void Awake()
    {
        if (string.IsNullOrEmpty(coinID))
        {
            coinID = transform.position.ToString();
        }
    }
}
