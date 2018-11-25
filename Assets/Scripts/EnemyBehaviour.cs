﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private Transform currentTarget;
    private Player currentPlayer;
    public float speed;

    private List<Player> players = new List<Player>();
    public AudioClip[] walkSounds;
    private AudioSource source;

    private bool isMoving;
    private bool isAttacking = false;

    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite imageLeft;
    [SerializeField] private Sprite imageRight;
    [SerializeField] private Sprite imageUp;
    [SerializeField] private Sprite imageDown;

    private Animator animator;

    [SerializeField] float damage = 10f;

    [SerializeField] private float attackCooldown = .5f;
    float distanceToTarget;

    private void Start() {
        players.AddRange(FindObjectsOfType<Player>());
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        EventsManager.Subscribe(EventsManager.EventType.PlayerSpawn, OnPlayerSpawn);
        EventsManager.Subscribe(EventsManager.EventType.PlayerSpawn, OnPlayerDeath);
    }

	private void OnDestroy() {
        EventsManager.Unsubscribe(EventsManager.EventType.PlayerSpawn, OnPlayerSpawn);
        EventsManager.Unsubscribe(EventsManager.EventType.PlayerSpawn, OnPlayerDeath);
	}

	private void OnPlayerSpawn(object o) {
        var player = (Player)o;
        players.Add(player);
    }

    private void OnPlayerDeath(object o) {
        var player = (Player)o;
        players.Remove(player);
        
    }

    void Update () {
        currentTarget = FindNearestTarget();
        if (currentTarget == null) {
            return;
        }

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        
        if (distanceToTarget > 1.2f && !isAttacking) {
            Move(target);
        }
        else if (!isAttacking) {
            Hit();
        }
    }

    private void Hit() {
        animator.SetBool("Walk", false);
        StartCoroutine(waitForHit());
    }

    IEnumerator waitForHit() {
        isAttacking = true;
        while (distanceToTarget < 1.2f) {

            yield return new WaitForSeconds(attackCooldown);
        }
        isAttacking = false;
        animator.SetBool("Walk", true);
    }

    private Transform FindNearestTarget() {
        Transform nearestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        for(int i = 0;i<players.Count;i++) {
            if(players[i] == null) {
                continue;
            }

            if(players[i].isAlive) {
                Vector3 directionToTarget = players[i].PlayerTransform.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr) {
                    closestDistanceSqr = dSqrToTarget;
                    nearestTarget = players[i].PlayerTransform;
                    currentPlayer = players[i];
                }
            }
        }
        return nearestTarget;
    }

    private void Move(Transform target) {
        animator.SetBool("Walk", true);
        float move = speed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(transform.position, target.position, move);

        Vector3 dir = (transform.position - target.position).normalized;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) {
            sp.sprite = dir.x > 0 ? imageLeft: imageRight;
        }
        else {
            sp.sprite = dir.y > 0 ? imageDown : imageUp;
        }

    }


    public void OnWalkSound()
    {
        source.clip = walkSounds[Random.Range(0, walkSounds.Length)];
        source.Play();
    }

    public void OnEnemyHit() {
        currentPlayer.Hit(damage);
    }
}
