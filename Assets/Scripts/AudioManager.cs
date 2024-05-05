using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChuongCustom;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public Sound[] musicSounds, sfxSound;
    public AudioSource musicSource, sfxSource;

    public bool CheckSound => !musicSource.mute;
    public bool CheckSFX => !sfxSource.mute;
    
    private void Start()
    {
        if (PlayerPrefs.GetFloat("firsttime") == 0)
        {
            PlayerPrefs.SetFloat("SfxVolume", 1);
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("firsttime", 1);
        }

        float _sound = PlayerPrefs.GetFloat("SfxVolume");
        float _music = PlayerPrefs.GetFloat("MusicVolume");
        musicSource.volume = _music;
        sfxSource.volume = _sound;
        musicSource.mute = _music <= 0.1f;
        sfxSource.mute = _sound <= 0.1f;
        PlayMusic("_background");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        MusicVolume(musicSource.mute ? 0f : 1f);
    }

    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
        SfxVolume(sfxSource.mute ? 0f : 1f);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }
}