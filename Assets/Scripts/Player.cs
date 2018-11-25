using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public enum Players
    {
        player1 = 0,
        player2 = 1
    }

    public enum WeaponType {
        melee = 0,
        range = 1
    }

    [SerializeField] private Rigidbody2D handRb;
    [SerializeField] private Transform handContainer;

    [SerializeField] private float force = 50;

    [SerializeField] public bool impulse;

    [SerializeField] Players playerNo;

    private Rigidbody2D rb2;
    [SerializeField] float speed = 10f;

    [SerializeField] Vector2 currentVelocity = new Vector2();
    [SerializeField] bool isMoving;

    public int hp = 100;
    [SerializeField] private Text hp_text;
    private Weapon currentWeapon;

    [SerializeField] private KeyCode keyLeft = KeyCode.A;
    [SerializeField] private KeyCode keyRight = KeyCode.D;
    [SerializeField] private KeyCode keyUp = KeyCode.W;
    [SerializeField] private KeyCode keyDown = KeyCode.S;
    [SerializeField] private KeyCode keyRotateLeft = KeyCode.C;
    [SerializeField] private KeyCode keyRotateRight = KeyCode.V;
    [SerializeField] private KeyCode keyUse = KeyCode.B;

    [SerializeField] private Sprite imageLeft;
    [SerializeField] private Sprite imageRight;
    [SerializeField] private Sprite imageUp;
    [SerializeField] private Sprite imageDown;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator animation;

    private bool inItemSpawner;
    private GetItemScript itemSpawner;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        currentWeapon = WeaponsManager.instance.CreateWeapon(WeaponsManager.WeaponType.Hammer, handContainer);
    }

    void FixedUpdate()
    {
        Movement();
        Attack();
        UseButton();
    }

    private void Update()
    {
        hp_text.text = hp.ToString();
        if (hp < 0)
        {
            hp_text.text = "DEAD";
        }
    }

    public void Hit(float weaponSpeed)
    {
        hp -= (int)weaponSpeed;
    }

    private void Attack()
    {
       // if (currentWeapon == WeaponType.melee) {
            if (Input.GetKeyDown(keyRotateLeft)) {
                handRb.AddTorque(force, ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown(keyRotateRight)) {
                handRb.AddTorque(-force, ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyRotateLeft)) {
                handRb.AddTorque(force, ForceMode2D.Force);
            }
            else if (Input.GetKey(keyRotateRight)) {
                handRb.AddTorque(-force, ForceMode2D.Force);
            }
     //   }

     //   if (currentWeapon == WeaponType.range) {
            if (Input.GetKey(keyRotateLeft)) {
                handRb.AddTorque(force / 5, ForceMode2D.Force);
            }
            else if (Input.GetKey(keyRotateRight)) {
                handRb.AddTorque(-force / 5, ForceMode2D.Force);
            }
     //   }
       
    }

    private void Movement()
    {

        if (Input.GetKey(keyRight))
        {
            currentVelocity.x = speed;
            playerSprite.sprite = imageRight;
        }
        else if (Input.GetKey(keyLeft))
        {
            currentVelocity.x = -speed;
            playerSprite.sprite = imageLeft;
        }
        else
        {
            currentVelocity.x = 0;
        }

        if (Input.GetKey(keyUp))
        {
            currentVelocity.y = speed;
            playerSprite.sprite = imageUp;
        }
        else if (Input.GetKey(keyDown))
        {
            currentVelocity.y = -speed;
            playerSprite.sprite = imageDown;
        }
        else
        {
            currentVelocity.y = 0;
        }

        /*if (playerNo == Players.player2) {
            if (Input.GetKey(KeyCode.RightArrow)) {
                currentVelocity.x = speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                currentVelocity.x = -speed;
            }
            else {
                currentVelocity.x = 0;
            }

            if (Input.GetKey(KeyCode.UpArrow)) {
                currentVelocity.y = speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                currentVelocity.y = -speed;
            }
            else {
                currentVelocity.y = 0;
            }
        }*/

        if (currentVelocity != Vector2.zero)
        {
            rb2.velocity = currentVelocity;
            isMoving = true;
        }
        else
        {
            if (isMoving)
            {
                rb2.velocity = Vector2.zero;
                isMoving = false;
            }
        }
        animation.enabled = isMoving;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Item") {
            inItemSpawner = true;
            itemSpawner = collision.gameObject.GetComponent<GetItemScript>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Item") {
            inItemSpawner = false;
            itemSpawner = null;
        }
    }

    private void UseButton() {
        if (Input.GetKeyDown(keyUse)) {
            if (inItemSpawner) {
                if (itemSpawner.ItemAvailable()) {
                    itemSpawner.GetItem();
                }
            }
            else {
                
            }
        }
    }
}
