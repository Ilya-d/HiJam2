﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float livingTime = 1.5f;

    // Update is called once per frame
    void Update () {
        transform.position += transform.up * Time.deltaTime * movementSpeed;
        livingTime -= Time.deltaTime;
        if(livingTime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player1") {
            collision.gameObject.GetComponent<Player>().Hit(10);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Unit>().Hit(10);
            Destroy(gameObject);
        }
    }
}
