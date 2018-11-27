using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private Transform currentTarget;
    private Player currentPlayer;
    public float speed;

    private List<Player> players = new List<Player>();
    public AudioClip[] walkSounds;

    private bool isAttacking = false;

    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Sprite imageLeft;
    [SerializeField] private Sprite imageRight;
    [SerializeField] private Sprite imageUp;
    [SerializeField] private Sprite imageDown;

    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource source;


    [SerializeField] float damage = 10f;

    [SerializeField] private float attackCooldown = 0.5f;
    private float attackTimeLeft;
    float distanceToTarget;

    private void Start() {
        players.AddRange(FindObjectsOfType<Player>());

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

    private void FixedUpdate() {
        currentTarget = FindNearestTarget();
        if (currentTarget == null) {
            return;
        }

        distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);

        if (distanceToTarget > 2.3f && attackTimeLeft <= 0) {
            Move(currentTarget);
            isAttacking = false;
        }
        else if (attackTimeLeft >= 0) {
            attackTimeLeft -= Time.deltaTime;
        }
        else {
            currentPlayer.Hit(damage);
            attackTimeLeft = attackCooldown;
            isAttacking = true;
        }

        animator.SetFloat("distanceToTarget", distanceToTarget);
        animator.SetBool("isAtacking", isAttacking);
    }

    private Transform FindNearestTarget() {
        Transform nearestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < players.Count; i++) {
            if (players[i] == null) {
                continue;
            }

            if (players[i].isAlive) {
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

        Vector3 dir = (target.position - transform.position).normalized;
        rb.MovePosition(rb.transform.position + dir * speed * Time.deltaTime);

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) {
            sp.sprite = dir.x > 0 ? imageRight : imageLeft;
        }
        else {
            sp.sprite = dir.y > 0 ? imageUp : imageDown;
        }
    }

    // Не доделал. Идея в том чтобы прыгнуть в игрока и при Collider Collision нанести урон.
    // Не уверен как это будет работать. Если будет много мобов то будет каша
    private void HitPLayer(Vector3 targetPosition) {
        Vector2 dir = (transform.position - targetPosition).normalized;
    }

    void OnDeath() {

    }

    public void OnWalkSound() {
        source.clip = walkSounds[Random.Range(0, walkSounds.Length - 1)];
        source.Play();
    }
}
