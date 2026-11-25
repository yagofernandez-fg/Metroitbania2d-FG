using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource[] sfxSorce;
    private AudioSource ambientSorce;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();
        ambientSorce = gameObject.AddComponent<AudioSource>();
        sfxSorce = new AudioSource[15];

        for (int i = 0; i < sfxSorce.Length; i++)
        {
            sfxSorce[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayMusic(AudioClip clip,bool fade=false,float fadeTime=1)
    {
        if (fade)
        {
            if (musicSource.isPlaying)
            {
                musicSource2.clip = clip;
                musicSource2.loop = true;
                musicSource2.Play();
                StartCoroutine(FadeOut(musicSource, musicSource2, fadeTime));
            }
            else
            {
                musicSource.clip = clip;
                musicSource.loop = false;
                musicSource.Play();
                StartCoroutine(FadeOut(musicSource2,musicSource , fadeTime));
            }
        }
        else
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
            musicSource2.Stop();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

    public void PlayAmbient(AudioClip clip)
    {
        ambientSorce.clip = clip;
        ambientSorce.loop = true;
        ambientSorce.Play();
    }

    public void StopAmbient()
    {
        ambientSorce.Stop();
        ambientSorce.clip = null;
    }

    public void PlaySfx(AudioClip clip)
    {
        for (int i = 0; i < sfxSorce.Length; i++)
        {
            if (sfxSorce[i].isPlaying)
            {
                sfxSorce[i].clip = clip;
                sfxSorce[i].loop = false;
                sfxSorce[i].Play();
                break;
            }
        }
    }

    IEnumerator FadeOut(AudioSource currentAudio,AudioSource nextAudo,float time)
    {
        float elapse = 0;
        while (elapse<time)
        {
            elapse += Time.deltaTime;
            
            currentAudio.volume = Mathf.Lerp(1, 0, elapse);
            nextAudo.volume = Mathf.Lerp(0, 1, elapse);
            
            yield return null;
        }
    }
}
