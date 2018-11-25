using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
            float speed = GetComponent<Rigidbody2D>().velocity.magnitude;   
            col.gameObject.GetComponent<Player>().Hit(speed);
            FindObjectOfType<AudioManager>().Play("HitBodyWithWeapon");
        }
    }
}
