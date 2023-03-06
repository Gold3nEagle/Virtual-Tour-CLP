using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float masterTestV = 1;
    [Range(0f, 1f)]
    public float masterVolume = 1;
    [Range(0f, 1f)]
    public float musicVolume = 1;
    [Range(0f, 1f)]
    public float SFXVolume = 1;
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
            if (!s.playFromObj) //if sound not meant to play outside the audioManager object
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
            volumes[IND] = s.volume;
            IND++;
        }
        //TODO: Check with saving manager if the game is muted when started, if so call MuteAll()
    }
    void Update()
    {
        //if(Input.GetKey(KeyCode.P))
        //{
            MasterVolumeChanged(masterTestV);
        //}
    }
    /// <summary>
    /// Play sound by its name.
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)  //this function will play any sound by its name, usage: AudioManager.instance.Play("soundName");
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
        s.source.Play();

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
        s.source.Stop();
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
            s.source.volume = max;
        }
        else if (s.volume + amount <= 0f)
        {
            s.volume = 0f;
            s.source.volume = 0f;
        }
        else
        {
            s.volume += amount;
            s.source.volume += amount;
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
        return s.source.volume;
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
            if (s.source != null)
            {
                s.volume = volumes[IND];
                s.source.volume = volumes[IND];
            }
            IND++;
        }

    }


    /// <summary>
    /// Adds a sound source to the given game object with the provided sound name.
    /// </summary>
    /// <param name="addAudioTo"></param>
    /// <param name="name"></param>
    public void SetAudioSourceOnGameObject(GameObject addAudioTo, string name)    //Assign audio to specefic gameobject instead of playing them from
    {                                                          //AudioManager object (for 3d sounds)
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if (!s.playFromObj)
        {
            Debug.LogWarning("Sound " + name + " is not meant to play outside the audio manager!");
            return;
        }
        if (s.source == null)
        {
            s.source = addAudioTo.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 1.0f;
            s.source.spread = 360;
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
                    s.volume = musicVolume;//newVolume;
                    s.source.volume = musicVolume;//newVolume;
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
                    s.source.volume = s.volume;
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
                    s.source.volume = newVolume;
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
                    s.source.volume = s.volume;
                }
            }
        }
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
