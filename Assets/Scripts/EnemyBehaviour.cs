using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private Vector3 target;
    public float speed;

    public Player[] players;
	
	void Update () {
        target = FindNearestTarget();
        if (target == null) {
            return;
        }

        float move = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, target, move);
    }

    private Vector3 FindNearestTarget() {
        Vector3 nearestTarget = Vector3.zero;
        float closestDistanceSqr = Mathf.Infinity;

        for(int i = 0;i<players.Length;i++) {
            Vector3 directionToTarget = players[i].PlayerPosition - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                nearestTarget = players[i].PlayerPosition;
            }
        }
        return nearestTarget;
    }
}
