using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPosition;

    [Header("Настройки дробовика")]
    [SerializeField] private float angle = 45;
    [SerializeField] private int bulletCount = 5;

    bool left;
    bool right;

    public void Shoot() {
        Vector3 startingRotation = transform.rotation.eulerAngles;
        startingRotation.z -= angle / 2;
        for(int i = 0;i < bulletCount;i++) {
            Instantiate(bullet, spawnPosition.position, Quaternion.Euler(startingRotation));
            startingRotation.z += angle / bulletCount;
        }
    }

    private void Update() {
        if (transform.rotation.eulerAngles.z > 180 && transform.rotation.eulerAngles.z < 360) {
            if (!left) {
                left = true;
                right = false;
                Rotate();
            }
        }
        else if (!right){
            left = false;
            right = true;
            Rotate();
        }
    }

    private void Rotate() {
        Vector3 position = transform.localPosition;
        Vector3 scale = transform.localScale;
        position.x = -position.x;
        scale.x = -scale.x;
        transform.localPosition = position;
        transform.localScale = scale;
    }
}
