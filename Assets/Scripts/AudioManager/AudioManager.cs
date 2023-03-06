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
    /// <param name="name">The sound name to play</param>
    /// <returns>Returns the gameobject that holds the played sound</returns>
    public GameObject Play(string name)  //this function will play any sound by its name, usage: AudioManager.instance.Play("soundName");
    {
        if (Muted)
        {
            return null;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return null;
        }      
        GameObject sound = new GameObject(name);
        sound.transform.parent = transform;
        sound.transform.localPosition = Vector3.zero;
        s.source = sound.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        sound.AddComponent<SoundHolder>();
        return sound;
    }
    /// <summary>
    /// Play sound by its name in a specific position.
    /// </summary>
    /// <param name="name">The sound name to play</param>
    /// <param name="position">The position which the sound will play at</param>
    /// <returns>Returns the gameobject that holds the played sound</returns>
    public GameObject Play(string name, Vector3 position)  //this function will play any sound by its name in a specific position, usage: AudioManager.instance.Play("soundName", Vector3.one);
    {
        if (Muted)
        {
            return null;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return null;
        }
        GameObject sound = new GameObject(name);
        sound.transform.parent = transform;
        sound.transform.position = position;
        s.source = sound.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = 1;
        sound.AddComponent<SoundHolder>();
        return sound;
    }
    /// <summary>
    /// Play sound by its name and follows a gameobject.
    /// </summary>
    /// <param name="name">The sound name to play</param>
    /// <param name="Obj">The gameObject which the sound will follow</param>
    /// <returns>Returns the gameobject that holds the played sound</returns>
    public GameObject Play(string name, GameObject Obj)  //this function will play any sound by its name in a specific position, usage: AudioManager.instance.Play("soundName", Vector3.one);
    {
        if (Muted)
        {
            return null;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return null;
        }
        GameObject sound = new GameObject(name);
        sound.transform.parent = transform;
        sound.transform.position = Obj.transform.position;
        s.source = sound.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = 1;
        sound.AddComponent<SoundHolder>();
        FollowingSound followScript = sound.AddComponent<FollowingSound>();
        followScript.SetFollowObject(Obj);
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
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject snd = transform.GetChild(i).gameObject;
            if(snd.name == name)
            {
                Destroy(snd);
            }

        }
        //s.source.Stop();
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

    /// <summary>
    /// Returns whether the sound in muted or not.
    /// </summary>
    /// <returns></returns>
    public bool IsMuted()
    {
        return Muted;
    }
}
