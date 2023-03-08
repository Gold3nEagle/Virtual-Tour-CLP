using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float masterVolume = 1;
    [Range(0f, 1f)]
    public float musicVolume = 1;
    [Range(0f, 1f)]
    public float SFXVolume = 1;
    public List<GameObject> soundsObjectsList;
    public Sound[] sounds;
    private float[] volumes;
    public static AudioManager instance;
    private bool Muted = false;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)    //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);  //this will make the gameobject to move to all scenes not only main menu

        volumes = new float[sounds.Length];

        int IND = 0;
        foreach (Sound s in sounds)     //adding audio sources to audioManager object
        {
            if (s.type == Sound.SoundType.Music)
            {
                s.volume = s.volume * musicVolume;
            }
            else if (s.type == Sound.SoundType.SFX)
            {
                s.volume = s.volume * SFXVolume;
            }
            volumes[IND] = s.volume;
            IND++;
            
        }
        soundsObjectsList = new List<GameObject>();
        //TODO: Check with saving manager if the game is muted when started, if so call MuteAll()
    }
    public GameObject Play(string name, Vector3 position = default, GameObject followObject = null)
    {
        if (Muted) return null;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound {name} not found!");
            return null;
        }

        if (s.singleInstance && s.source != null)
        {
            Debug.Log($"Sound {name} is already playing!");
            return s.source.gameObject;
        }

        GameObject sound = new GameObject(name);
        if(position == default) sound.transform.parent = followObject?.transform ?? transform;
        sound.transform.position = position;
        s.source = sound.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        if(followObject != null || position != default)
        {
            s.source.spatialBlend = 1;
        }
        else
        {
            s.source.spatialBlend = 0;
        }
        //s.source.spatialBlend = followObject != null ? 1 : 0;

        sound.AddComponent<SoundHolder>();
        soundsObjectsList.Add(sound);
        return sound;
    }
    /// <summary>
    /// Stop the sound by its name.
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)   //this function will stop any sound by its name, usage: AudioManager.instance.Stop("soundName"); 
    {
        if (Muted)
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        foreach (GameObject sndObject in soundsObjectsList)
        {
            if (sndObject.name == name) Destroy(sndObject);
        }
    }
    /// <summary>
    /// Add volume to the current sound (you can use - to for decrement).
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    public void ModifySoundVolume(string name, float amount)    //this function will add volume to any sound by its name
    {                                                       //usage: AudioManager.instance.AddVolume("soundName", 0.5f);
        if (Muted)
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        float max = 0;
        if(s.type == Sound.SoundType.Music)
        {
            max = musicVolume;
        }
        else if(s.type == Sound.SoundType.SFX)
        {
            max = SFXVolume;
        }

        if (s.volume + amount >= max)
        {
            s.volume = max;
        }
        else if (s.volume + amount <= 0f)
        {
            s.volume = 0f;
        }
        else
        {
            s.volume += amount;
        }
        UpdateSoundsLevel(s);
    }

    private void UpdateSoundsLevel(Sound s)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject sndObj = transform.GetChild(i).gameObject;
            if (sndObj.name == s.name)
            {
                sndObj.GetComponent<AudioSource>().volume = s.volume;
            }

        }
    }
    /// <summary>
    /// Check if the sound has a max volume (1f).
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsVolumeMax(string name)    //used to check the volume of a sound if it is max
    {
        if (Muted)
        {
            return false;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        if (s.volume >= 1f)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Check whether the sound is playing or not.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsPlaying(string name)  //used to check whether the sound is playing or not
    {
        if (Muted)
        {
            return false;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        if(s.source == null)
        {
            return false;
        }
        return s.source.isPlaying;
    }
    /// <summary>
    /// Get the volume level of the sound by its name (between 0f and 1f).
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetVolume(string name) //get volume of certain sound (between 0 and 1)
    {
        if (Muted)
        {
            return 0;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return 0;
        }
        return s.volume;
    }
    /// <summary>
    /// Mute the game.
    /// </summary>
    public void MuteAll()   //used to mute the game.
    {
        Muted = true;
        //TODO: Notify the saving manager that the game is muted 
        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                s.volume = 0f;
                s.source.volume = 0f;
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<AudioSource>().volume = 0;
        }
    }

    /// <summary>
    /// Unmute the game.
    /// </summary>
    public void UnMuteAll() //used to unmute the game.
    {
        Muted = false;
        //TODO: Notify the saving manager that the game is unmuted 
        int IND = 0;
        foreach (Sound s in sounds)
        {
            s.volume = volumes[IND];
            UpdateSoundsLevel(s);
            IND++;
        }

    }

    /// <summary>
    /// Changes the master volume and updates the music and SFX volumes accordingly.
    /// </summary>
    /// <param name="volume">The new value for the master volume.</param>
    public void MasterVolumeChanged(float volume)
    {
        if (masterVolume == volume) return;

        float previousMasterVolume = masterVolume;
        masterVolume = volume;
        float newMusicVolume = musicVolume / previousMasterVolume * masterVolume;
        float newSFXVolume = SFXVolume / previousMasterVolume * masterVolume;
        if (float.IsNaN(newMusicVolume)) newMusicVolume = masterVolume;
        if (float.IsNaN(newSFXVolume)) newSFXVolume = masterVolume;
        MusicVolumeChanged(newMusicVolume);
        SFXVolumeChanged(newSFXVolume);
    }

    /// <summary>
    /// Changes the music volume and updates the volume of all sound effects of type music.
    /// </summary>
    /// <param name="volume">The new value for the music volume.</param>
    public void MusicVolumeChanged(float volume)
    {
        if (musicVolume == volume) return;

        float previousMusicVolume = musicVolume;
        musicVolume = volume;

        if (previousMusicVolume == 0f)
        {
            //float newVolume = 0f;
            //if (musicVolume != 0) newVolume = musicVolume;
            foreach (Sound s in sounds)
            {
                if (s.type == Sound.SoundType.Music)
                {
                    s.volume = musicVolume;
                    UpdateSoundsLevel(s);
                }
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                if (s.type == Sound.SoundType.Music)
                {
                    s.volume = s.volume / previousMusicVolume * musicVolume;
                    UpdateSoundsLevel(s);
                }
            }
        }
    }

    /// <summary>
    /// Changes the SFX volume and updates the volume of all sound effects of type SFX.
    /// </summary>
    /// <param name="volume">The new value for the SFX volume.</param>
    public void SFXVolumeChanged(float volume)
    {
        if (SFXVolume == volume) return;

        float previousSFXVolume = SFXVolume;
        SFXVolume = volume;

        if (previousSFXVolume == 0f)
        {
            float newVolume = 0f;
            if (SFXVolume != 0) newVolume = SFXVolume;
            foreach (Sound s in sounds)
            {
                if (s.type == Sound.SoundType.SFX)
                {
                    s.volume = newVolume;
                    UpdateSoundsLevel(s);
                }
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                if (s.type == Sound.SoundType.SFX)
                {
                    s.volume = s.volume / previousSFXVolume * SFXVolume;
                    UpdateSoundsLevel(s);
                }
            }
        }
    }

    public void SoundDestroyed(GameObject sndObj)
    {
        soundsObjectsList.Remove(sndObj);
    }
    /// <summary>
    /// Returns whether the sound in muted or not.
    /// </summary>
    /// <returns></returns>
    public bool IsMuted()
    {
        return Muted;
    }
}