using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        var unit = col.GetComponent<Unit>();
        if (unit == null) {
            return;
        }
        float speed = GetComponent<Rigidbody2D>().velocity.magnitude;   
        unit.Hit(speed);
    }
}
