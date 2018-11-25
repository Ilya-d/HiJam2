using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private Transform player1;
    private Transform player2;
    public float speed;
    private Camera camera;
    private float cameraSize;

    void Awake() {
        EventsManager.Subscribe(EventsManager.EventType.PlayerSpawn, OnPlayerSpawn);
        EventsManager.Subscribe(EventsManager.EventType.PlayerDeath, OnPlayerDeath);
    }

    void Start () {
        /*var players = FindObjectsOfType<Player>();
        foreach (var player in players) {
            OnPlayerSpawn(player);
        }*/
        camera = gameObject.GetComponent<Camera>();
        cameraSize = 10f;
    }

    private void OnPlayerSpawn(object o) {
        var player = (Player)o;
        Debug.Log("PlayerSpawn: " + player.playerNumber);
        var number = player.playerNumber;
        if (number == Player.PlayerNumbers.player1) {
            player1 = player.transform;
        }
        if (number == Player.PlayerNumbers.player2) {
            player2 = player.transform;
        }
    }

    private void OnPlayerDeath(object o) {
        var player = (Player)o;
        var number = player.playerNumber;
        if (number == Player.PlayerNumbers.player1) {
            player1 = null;
        }
        if (number == Player.PlayerNumbers.player1) {
            player2 = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        float move = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(((player1.transform.position.x + player2.transform.position.x)/2), ((player1.transform.position.y + player2.transform.position.y)/2), -10f), move);

        if (player1 == null || player2 == null || Vector3.Distance(player1.transform.position, player2.transform.position) <= 18f)
        {
            cameraSize = 10f;
        }
        else if (Vector3.Distance(player1.transform.position, player2.transform.position) > 18f && Vector3.Distance(player1.transform.position, player2.transform.position) < 36f)
        {
            cameraSize = (Vector3.Distance(player1.transform.position, player2.transform.position)) / 1.8f;
        }
        else {
            cameraSize = 20f;
        }


        camera.orthographicSize = cameraSize;
    }
}
