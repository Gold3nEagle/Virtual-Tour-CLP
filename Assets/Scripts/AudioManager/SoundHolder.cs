using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    void Start()
    {
        AudioSource thisAudio = GetComponent<AudioSource>();
        thisAudio.Play();
        if(!thisAudio.loop) Destroy(gameObject, thisAudio.clip.length+0.1f);
    }

}
