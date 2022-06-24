using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject instance;

    public Sound[] sounds;

    public List<Sound> SFX = new List<Sound>();


    float currentSongVolume;
    float currentSFXVolume;

    private void Awake()
    {
        if (instance == null)
            instance = this.gameObject;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        
        
        foreach(Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            if (s.name.EndsWith("SFX")) SFX.Add(s);
        }

       
    }

    private void Start()
    {
        PlaySound("BGMSong");
    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
        if (name == "BGMSong") s.source.loop = true;
    }

    public void ChangeBGMAudioVolume()
    {
        Slider songSlider = FindObjectOfType<BGMSlider>().GetComponentInChildren<Slider>();
        Sound s = Array.Find(sounds, sound => sound.name == "BGMSong");
        s.source.volume = songSlider.value;
        currentSongVolume = songSlider.value;
    }

    public void ChangeSFXVolume()
    {
        Slider sfxSlider = FindObjectOfType<SFXSlider>().GetComponentInChildren<Slider>();
        foreach (Sound s in SFX)
        {
            s.source.volume = sfxSlider.value;
            currentSFXVolume = sfxSlider.value;
        }
    }

    public void ToogleButtonSong(bool temp)
    {
        Slider songSlider = FindObjectOfType<BGMSlider>().GetComponentInChildren<Slider>();
        Sound s = Array.Find(sounds, sound => sound.name == "BGMSong");
        if(temp)
        {
            s.source.Play();
            songSlider.interactable = true;
        }
        else
        {
            s.source.Pause();
            songSlider.interactable = false;
        }
    }



    public void ToogleSFXButton(bool temp)
    {
        Slider sfxSlider = FindObjectOfType<SFXSlider>().GetComponentInChildren<Slider>();
        if (temp)
        {
            foreach(Sound s in SFX)
            {
                s.source.Play();
            }
            sfxSlider.interactable = true;
        }
        else
        {
            foreach (Sound s in SFX)
            {
                s.source.Pause();
            }
            sfxSlider.interactable = false;
        }
    }
    
    
}
