﻿using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    // Use this for initialization
    void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.priority = s.priority;

        }

    }

    void Start() {

        Play("GameplayTheme");
        Play("ForestAmb");
        Play("RoundStart");
        Play("Crows");

    }

    // Update is called once per frame
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
        s.source.enabled = true;
    }
}
