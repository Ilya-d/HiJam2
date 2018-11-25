using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour {

    
    public GameObject[] enemies;
    public GameObject spawner;

    private bool gameOver;
    private bool restart;

    public int enemiesCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Use this for initialization
    void Start () {

        gameOver = false;
        restart = false;
        StartCoroutine(SpawnWaves());
    }
	
	// Update is called once per frame
	void Update () {


		
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {

            for (int i = 0; i < enemiesCount; i++)
            {
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                Instantiate(enemy, spawner.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
               // restartText.text = "Press Spacebar to Restart";
                restart = true;
                break;
            }
        }
    }
}
