using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private Transform player1;
    private Transform player2;
    public float speed;
    private Camera cam;
    private float cameraSize;
    private float originalCameraSize;

    void Awake() {
        EventsManager.Subscribe(EventsManager.EventType.PlayerSpawn, OnPlayerSpawn);
        EventsManager.Subscribe(EventsManager.EventType.PlayerDeath, OnPlayerDeath);
    }

    void Start () {
        /*var players = FindObjectsOfType<Player>();
        foreach (var player in players) {
            OnPlayerSpawn(player);
        }*/
        cam = gameObject.GetComponent<Camera>();
        cameraSize = originalCameraSize = cam.orthographicSize;
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
        if (number == Player.PlayerNumbers.player2) {
            player2 = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (player1 == null && player2 == null) {
            return;
        }

        float move = speed * Time.deltaTime;
        var targetCameraPos = transform.position;
        if (player1 != null && player2 != null) {
            targetCameraPos = (player1.position + player2.position) / 2;
        } else if (player1 != null) {
            targetCameraPos = player1.position;
        } else if (player2 != null) {
            targetCameraPos = player2.position;
        }
        targetCameraPos.z = transform.position.z;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, targetCameraPos, move);

        var lowerDistance = 30f;
        var higherDistance = lowerDistance * 1.5f;
        if (player1 == null || player2 == null || Vector3.Distance(player1.position, player2.position) <= lowerDistance) {
            cameraSize = originalCameraSize;
        } else if (Vector3.Distance(player1.position, player2.position) > lowerDistance && Vector3.Distance(player1.position, player2.position) < higherDistance) {
            cameraSize = (Vector3.Distance(player1.position, player2.position)) / 2f;
        } else {
            cameraSize = originalCameraSize*1.5f;
        }
        cam.orthographicSize = cameraSize;
    }
}
