using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSound : MonoBehaviour {

    public AudioClip[] sounds;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnWalkSound ()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.Play();
    }
}
