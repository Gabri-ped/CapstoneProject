using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] position = new float[3];
    public float[] rotation = new float[4];
    public int coins;
    public int lifes;
    public List <string> collectedCoins = new List<string>();
    public int extraLives;
    public int sceneIndex;
    public bool SuperJumpActive;

    public SaveData()
    {
    }

    public SaveData(float[] position, float[] rotation, int coins, int lifes, List<string> collectedCoins,int extraLives = 0,int sceneIndex = 1,bool superJumpActivate = false)
    {
        this.position = position;
        this.rotation = rotation;
        this.coins = coins;
        this.lifes = lifes;
        this.collectedCoins = collectedCoins ?? new List<string>();
        this.extraLives = extraLives;
        this.sceneIndex = sceneIndex;
        this.SuperJumpActive = superJumpActivate;
    }
}
