using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    
    public GameObject[] enemies;
    public GameObject spawner;

    public int enemiesCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);

        while (true) {

            for (int i = 0; i < enemiesCount; i++) {
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                Instantiate(enemy, spawner.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
        }
    }
}
