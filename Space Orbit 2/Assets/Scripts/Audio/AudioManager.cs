using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSourceLayer01;
    public AudioSource musicSourceLayer02;
    public AudioSource musicSourceLayer03;
    public AudioSource musicSourceLayer04;

    public AudioClip musicLayer01;
    public AudioClip musicLayer02;
    public AudioClip musicLayer03;
    public AudioClip musicLayer04;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicSourceLayer01 = gameObject.AddComponent<AudioSource>();
        musicSourceLayer02 = gameObject.AddComponent<AudioSource>();
        musicSourceLayer03 = gameObject.AddComponent<AudioSource>();
        musicSourceLayer04 = gameObject.AddComponent<AudioSource>();

        musicSourceLayer01.clip = musicLayer01;
        musicSourceLayer02.clip = musicLayer02;
        musicSourceLayer03.clip = musicLayer03;
        musicSourceLayer04.clip = musicLayer04;

        musicSourceLayer01.loop = true;
        musicSourceLayer02.loop = true;
        musicSourceLayer03.loop = true;
        musicSourceLayer04.loop = true;

        musicSourceLayer01.Play();
        musicSourceLayer02.Play();
        musicSourceLayer03.Play();
        musicSourceLayer04.Play();

        musicSourceLayer03.mute = true;
        musicSourceLayer04.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerEnterOrbit()
    {
        if (musicSourceLayer01 != null && musicSourceLayer02 != null) 
        {
            musicSourceLayer02.mute = true;
        }
    }

    public void PlayerExitOrbit()
    {
        if (musicSourceLayer01 != null && musicSourceLayer02 != null)
        {
            musicSourceLayer02.mute = false;
        }
    }

    public void GoingFast()
    {
        musicSourceLayer03.mute = true;
        musicSourceLayer04.mute = false;
    }

    public void GoingSlow()
    {
        musicSourceLayer03.mute = false;
        musicSourceLayer04.mute = true;
    }
}
