using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField] private Player.PlayerNumbers playerNumber;

    void Start () {
        ResourceManager.instance.SpawnPlayer(playerNumber, transform.position);
    }

}
