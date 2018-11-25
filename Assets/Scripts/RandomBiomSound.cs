using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBiomSound : MonoBehaviour {

    private AudioSource randomSound;

    public AudioClip[] audioSources;

    // Use this for initialization
    void Start()
    {
        randomSound = GetComponent<AudioSource>();
        CallAudio();
    }


    void CallAudio()
    {
        Invoke("RandomSoundness", 10);
    }

    void RandomSoundness()
    {
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play();
        CallAudio();
    }
}