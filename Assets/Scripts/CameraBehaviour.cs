using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    public float speed;
    private Camera camera;
    private float cameraSize;

    // Use this for initialization
    void Start () {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        camera = gameObject.GetComponent<Camera>();
        cameraSize = 10f;
    }
	
	// Update is called once per frame
	void Update () {
        float move = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(((player1.transform.position.x + player2.transform.position.x)/2), ((player1.transform.position.y + player2.transform.position.y)/2), -10f), move);

        if (Vector3.Distance(player1.transform.position, player2.transform.position) <= 18f)
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
