using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPosition;

    [Header("Настройки дробовика")]
    [SerializeField] private float angle = 45;
    [SerializeField] private int bulletCount = 5;
   
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)) {
            Shoot();
        }
	}

    private void Shoot() {
        Vector3 startingRotation = transform.rotation.eulerAngles;
        startingRotation.z -= angle / 2;
        for(int i = 0;i < bulletCount;i++) {
            Instantiate(bullet, spawnPosition.position, Quaternion.Euler(startingRotation));
            startingRotation.z += angle / bulletCount;
        }
        
    }
}
