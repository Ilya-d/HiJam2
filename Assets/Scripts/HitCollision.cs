using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
            float speed = GetComponent<Rigidbody2D>().velocity.magnitude;   
            col.gameObject.GetComponent<Player>().Hit(speed);
        }
    }
}
