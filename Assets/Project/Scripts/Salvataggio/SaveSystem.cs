using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class SaveSystem : MonoBehaviour
{
    [SerializeField] private Transform _player;
    public SaveData SaveData { get; set; }
    public bool _isLoad { get; private set; }
    private string _path;
    private string _data;


    public static SaveSystem Instance { get; private set;}
    void Awake()
    {
        SaveData = new SaveData();
        _path = Application.dataPath + "/savefile2.txt";
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            _path = Path.Combine(Application.persistentDataPath, "save.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("qui");
        if (scene.buildIndex != 0)
        {
            StartCoroutine(LoadPlayerDelayed());
        }

    }

    private IEnumerator LoadPlayerDelayed()
    {
        yield return null; // aspetta un frame
        FoundPlayer();
        if (_isLoad) LoadPlayerInfo();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        float[] position = new float[3];
        position[0] = _player.position.x;
        position[1] = _player.position.y;
        position[2] = _player.position.z;
        float[] rotation = new float[4];
        rotation[0] = _player.rotation.x;
        rotation[1] = _player.rotation.y;
        rotation[2] = _player.rotation.z;
        rotation[3] = _player.rotation.w;
        int coins = CoinsManager.Instance.totalCoins;
        int lifes = LifeController.Instance.currentLives;
        int extraLives = SaveData.extraLives;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;


        SaveData = new SaveData(position, rotation, coins, lifes, SaveData.collectedCoins,extraLives,sceneIndex);
        _data = JsonConvert.SerializeObject(SaveData, Formatting.Indented);
        try
        {
            File.WriteAllText(_path, _data);
        }
        catch
        {
            Debug.Log("Error saving file");
        }
    }

    public void LoadGame()
    {
        if (File.Exists(_path))
        {
            _data = File.ReadAllText(_path);
            SaveData = JsonConvert.DeserializeObject<SaveData>(_data);
            int sceneLoaded = SaveData.sceneIndex;
            SceneManager.LoadScene(sceneLoaded);
            _isLoad = true;
        }
        else
        {
            Debug.Log("No save file found");
          
        }
    }

    public void SaveShopData(int coins, int extraLives)
    {
        if (SaveData == null)
            SaveData = new SaveData();

        // Aggiorna solo i dati globali
        SaveData.coins = coins;
        SaveData.extraLives = extraLives;

        // Mantiene le altre info già salvate
        string json = JsonConvert.SerializeObject(SaveData, Formatting.Indented);

        try
        {
            File.WriteAllText(_path, json);
            Debug.Log("Shop data salvati correttamente.");
        }
        catch
        {
            Debug.LogError("Errore durante il salvataggio dei dati shop!");
        }
    }

    public void LoadPlayerInfo()
    {
        if (_player == null)
            FoundPlayer();

        if (_isLoad && _player != null)
        {
            // ✅ Posizione e rotazione solo se esistono
            if (SaveData.position != null && SaveData.position.Length == 3)
                _player.position = new Vector3(SaveData.position[0], SaveData.position[1], SaveData.position[2]);

            if (SaveData.rotation != null && SaveData.rotation.Length == 4)
                _player.rotation = new Quaternion(SaveData.rotation[0], SaveData.rotation[1], SaveData.rotation[2], SaveData.rotation[3]);

            // ✅ LifeController
            if (LifeController.Instance != null)
                LifeController.Instance.SetLives(SaveData.lifes);

            // ✅ CoinsManager
            if (CoinsManager.Instance != null)
                CoinsManager.Instance.SetCoins(SaveData.coins);

            // ✅ ShopManager (solo se esiste nella scena)
            if (ShopManager.Instance != null)
                ShopManager.Instance.extraLivesBought = SaveData.extraLives;

            // ✅ CoinRegistry (solo se esiste e ci sono monete registrate)
            if (CoinRegistry.Instance != null && SaveData.collectedCoins != null)
                CoinRegistry.Instance.DisableCollectedCoins(SaveData.collectedCoins);
            else
                Debug.Log("[SaveSystem] CoinRegistry o collectedCoins non trovati, skip disattivazione.");

            _isLoad = false;

            Debug.Log("[SaveSystem] Dati caricati correttamente.");
        }

    }

    public void FoundPlayer()
    {
        _player = PlayerController.instance.transform;
    }
}


