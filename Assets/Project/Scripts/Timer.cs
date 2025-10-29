using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;
    [SerializeField] private GameObject _canvas;
    

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime <= 0)
        {
            remainingTime = 0;
            _canvas.SetActive(true);
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
