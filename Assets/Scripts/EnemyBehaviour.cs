using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private GameObject player;
    public float speed;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player1");
	}
	
	// Update is called once per frame
	void Update () {
        float move = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, move);
    }
}
