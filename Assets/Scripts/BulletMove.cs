using System.Collections;
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
}
