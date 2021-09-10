using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public SettingsMenu settingsMenu;

    public static AudioManager instance;
    public float soundDecrease = 0.1f;
    public string musicPlayingName;

    public float generaleVolume;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * generaleVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }
    
    private void Start()
    {
        Play("MenuMusic");
    }

    public void Update()
    {
        if(settingsMenu != null)
        { 
            generaleVolume = settingsMenu.volumeGenerale;
            foreach (Sound s in sounds)
            {
                s.source.volume = s.volume * generaleVolume;
            }
        }
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();

        if(s.CouldCrossfade)
            musicPlayingName = name;
    }

    public IEnumerator FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        while (s.source.volume > 0)
        {
            s.source.volume -= soundDecrease;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
