using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour

{
    [Header("Impostazioni Vite")]
    public int maxLives = 3;
    public int currentLives;

    [Header("UI Vita Singola + Contatore")]
    [SerializeField] private Image heartIcon;
    [SerializeField] private TextMeshProUGUI livesCounterText;
    public Sprite fullHeart;

    [Header("Player e Respawn")]
    public Transform player;
    public Transform respawnPoint;
    public Animator _anim;
    public GameObject playerAnimator;

    [Header("Game Over")]
    public GameObject gameOverCanvas;

    private bool isRespawning = false;
    public static LifeController Instance { get; private set; }

    void Start()
    {
        _anim = playerAnimator.GetComponent<Animator>();
        Instance = this;

        // Attendo un frame per assicurarmi che il SaveSystem sia pronto
        StartCoroutine(InitializeLivesDelayed());
    }

    private IEnumerator InitializeLivesDelayed()
    {
        yield return null; // aspetta un frame
        int savedLives = maxLives;
        int extraLives = 0;

        if (SaveSystem.Instance != null && SaveSystem.Instance.SaveData != null)
        {
            savedLives = SaveSystem.Instance.SaveData.lifes;
            if(savedLives <= 0)
            {
                savedLives = maxLives;
            }
            extraLives = SaveSystem.Instance.SaveData.extraLives;
        }

        SetLives(savedLives);

        Debug.Log($"[LifeController] Vite iniziali: {savedLives}, vite extra: {extraLives}");
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateHeartsUI();

        if (currentLives > 0)
        {
            StartCoroutine(DeathAndRespawnCoroutine());
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator DeathAndRespawnCoroutine()
    {
        isRespawning = true;

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = false;

        _anim.SetTrigger("Die");

        yield return new WaitForSeconds(1.5f);

        if (player != null)
            player.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.gameObject.SetActive(true);
        }

        controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = true;

        isRespawning = false;
    }

    // 🔥 Mostra il numero di vite accanto al cuore
    void UpdateHeartsUI()
    {
        if (livesCounterText == null) return;

        livesCounterText.text = "x " + currentLives.ToString();

        if (heartIcon != null)
            heartIcon.sprite = fullHeart;
    }
    public void SetLives(int baseLives)
    {
        int extraLives = SaveSystem.Instance?.SaveData?.extraLives ?? 0;
        int totalLives = Mathf.Clamp(baseLives + extraLives, 0, maxLives + extraLives);

        currentLives = totalLives;
        UpdateHeartsUI();
    }

    void GameOver()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        AudioManager.Instance.PlayLoseSound();
        Time.timeScale = 0f;
    }
}
