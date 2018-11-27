using UnityEngine;

public class BulletMove : MonoBehaviour {

    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float livingTime = 1.5f;

    private void Update() {
        transform.position += transform.up * Time.deltaTime * movementSpeed;
        livingTime -= Time.deltaTime;
        if (livingTime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.gameObject.tag) {
            case "Player1":
            case "Enemy":
                collision.gameObject.GetComponent<Unit>().Hit(10);
                Destroy(gameObject);
                break;
        }
    }
}
