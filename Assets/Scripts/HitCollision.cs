using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour {

    public AudioClip[] hitBodySounds;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D col) {
        var unit = col.GetComponent<Unit>();
        if (unit == null) {
            return;
        }

        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
            source.clip = hitBodySounds[Random.Range(0, hitBodySounds.Length)];
            source.Play();
        }
        float speed = GetComponent<Rigidbody2D>().velocity.magnitude;   
        unit.Hit(speed);
    }
}