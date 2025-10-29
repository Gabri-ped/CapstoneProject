using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;  
    public AudioSource sfxSource;     
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public AudioClip backgroundMusic2;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip knifeSound;
    [SerializeField] private AudioClip laserSound;

    [SerializeField] private AudioMixer audioMixer;
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            SceneManager.sceneLoaded += OnSceneLoaded;

            LoadVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) 
            PlayMusic(menuMusic);

        else if (scene.buildIndex == 1) 
            PlayMusic(backgroundMusic);

        else if (scene.buildIndex == 2)
            PlayMusic(backgroundMusic2);
    }

    public void PlayMusic(AudioClip clip)
    {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }
    public void StopVictorySound()
    {
       if (sfxSource.isPlaying)
           sfxSource.Stop();
    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MUSIC_VOLUME_PARAM, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAM, value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(SFX_VOLUME_PARAM, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat(SFX_VOLUME_PARAM, value);
    }

    public void LoadVolumes()
    {
        float music = PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM, 0.75f);
        float sfx = PlayerPrefs.GetFloat(SFX_VOLUME_PARAM, 0.75f);

        SetMusicVolume(music);
        SetSFXVolume(sfx);
    }
    public void PlayCoinSound()
    {
        if (coinSound != null)
            sfxSource.PlayOneShot(coinSound);
    }

    public void PlayWinSound()
    {
        StopBackgroundMusic();
        if (winSound != null)
            sfxSource.PlayOneShot(winSound);   
    }

    public void PlayLoseSound()
    {
        StopBackgroundMusic();
        if (loseSound != null)
            sfxSource.PlayOneShot(loseSound);
    }

    public void PlayButtonSound()
    {
        if (buttonSound != null)
            sfxSource.PlayOneShot(buttonSound);
    }
    public void PlayBombSound()
    {
        if (loseSound != null)
            sfxSource.PlayOneShot(bombSound);
    }

    public void PlayKnifeSound()
    {
        if (knifeSound != null)
            sfxSource.PlayOneShot(knifeSound);
    }
    public void PlayLaserSound()
    {
        if (laserSound != null)
            sfxSource.PlayOneShot(laserSound);
    }
}

