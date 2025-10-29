using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZoneLock : MonoBehaviour

{
    public int totalCoinsRequired = 10;
    public GameObject warningCanvas;
    public float canvasDuration = 2f;

    private void Start()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            int collectedCount = 0;

            // ✅ Controlla quante monete sono state raccolte
            if (SaveSystem.Instance != null && SaveSystem.Instance.SaveData != null)
                collectedCount = SaveSystem.Instance.SaveData.collectedCoins.Count;

            if (collectedCount < totalCoinsRequired)
            {
                StartCoroutine(ShowCanvas());
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("Zona di vittoria sbloccata!");
            }
        }
    }

    private IEnumerator ShowCanvas()
    {
        warningCanvas.SetActive(true);
        yield return new WaitForSeconds(canvasDuration);
        warningCanvas.SetActive(false);
    }
}
