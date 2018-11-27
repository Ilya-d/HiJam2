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
    [SerializeField] AudioClip shotGunReload;
    private AudioSource source;

    private int bulletCount = 2;
    [SerializeField] private float cooldown = 2;
    private float cooldownLeft = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(shotGunReload);
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
        yield return new WaitForSeconds(cooldown - shotGunReload.length);
        source.PlayOneShot(shotGunReload);
        yield return new WaitForSeconds(shotGunReload.length);
        bulletCount = 2;
    }


}
