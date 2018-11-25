using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPosition;

    [Header("Настройки дробовика")]
    [SerializeField] private float angle = 45;
    [SerializeField] private int bulletSpray = 5;

    public AudioClip[] shotgunSounds;
    private AudioSource source;

    private int bulletCount = 2;
    [SerializeField] private float cooldown = 2;

    void Start()
    {
        // Play Reload Sound

        source = GetComponent<AudioSource>();
    }

    public void Shoot() {
        if (bulletCount > 0) {
            Vector3 startingRotation = transform.rotation.eulerAngles;
            startingRotation.z -= angle / 2;
            for (int i = 0; i < bulletSpray; i++) {
                Instantiate(bullet, spawnPosition.position, Quaternion.Euler(startingRotation));
                startingRotation.z += angle / bulletSpray;
            }
            source.clip = shotgunSounds[Random.Range(0, shotgunSounds.Length)];
            source.Play();
            bulletCount--;
            if(bulletCount<=0) {
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload() {
        // Play Reload Sound
        yield return new WaitForSeconds(cooldown);
        bulletCount = 2;
    }


}
