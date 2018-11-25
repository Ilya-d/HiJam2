using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public enum Players {
        player1 = 0,
        player2 = 1
    }

    [SerializeField] private Rigidbody2D handRb;
    [SerializeField] private Transform handContainer;

    [SerializeField] private float force = 50;

    [SerializeField] public bool impulse;

    [SerializeField] Players playerNo;

    private Rigidbody2D rb2;
    [SerializeField] float speed = 10f;

    private Vector2 currentVelocity = new Vector2();
    private bool isMoving;

    public int hp = 100;
    [SerializeField] private Text hp_text;
    private Weapon currentWeapon;
    private WeaponsManager.WeaponType currentWeaponType;

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
    [SerializeField] private GameObject pickupNotify;

    private UsableItem itemOnFloor;


    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        SetWeapon(WeaponsManager.WeaponType.Hammer);
    }

    void FixedUpdate() {
        Movement();
        Attack();
        UseButton();
        UpdateHP();
    }

    private void SetWeapon(WeaponsManager.WeaponType weapon) {
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = WeaponsManager.instance.CreateWeapon(weapon, handContainer);
        currentWeaponType = weapon;
    }


    public void Hit(float weaponSpeed) {
        hp -= (int)weaponSpeed;

    }

    private void UpdateHP() {
        if (hp_text == null) {
            return;
        }
        if (hp > 0) {
            hp_text.text = hp.ToString();
        } else {
            hp_text.text = "DEAD";
        }   
    }

    private void UseButton() {
        if (itemOnFloor != null && Input.GetKeyDown(keyUse)) {
            UseItem(itemOnFloor);
        }
        if (Input.GetKeyDown(keyUse)) {
            if (currentWeaponType == WeaponsManager.WeaponType.Shotgun) {
                currentWeapon.gameObject.GetComponent<ShotGun>().Shoot();
            }
        }
    }

    private void UseItem(UsableItem item) {
        if (item.itemType == UsableItem.ItemType.Weapon) {
            SetWeapon((WeaponsManager.WeaponType)itemOnFloor.value);
            Destroy(itemOnFloor);    
        }

        itemOnFloor = null;
    }

    private void Attack() {
        if (currentWeaponType != WeaponsManager.WeaponType.Shotgun) {
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
        }
        else {
            if (Input.GetKey(keyRotateLeft)) {
                handRb.AddTorque(force / 5, ForceMode2D.Force);
            }
            else if (Input.GetKey(keyRotateRight)) {
                handRb.AddTorque(-force / 5, ForceMode2D.Force);
            }
        }
    }

    private void Movement() {

        if (Input.GetKey(keyRight)) {
            currentVelocity.x = speed;
            playerSprite.sprite = imageRight;
        }
        else if (Input.GetKey(keyLeft)) {
            currentVelocity.x = -speed;
            playerSprite.sprite = imageLeft;
        }
        else {
            currentVelocity.x = 0;
        }

        if (Input.GetKey(keyUp)) {
            currentVelocity.y = speed;
            playerSprite.sprite = imageUp;
        }
        else if (Input.GetKey(keyDown)) {
            currentVelocity.y = -speed;
            playerSprite.sprite = imageDown;
        }
        else {
            currentVelocity.y = 0;
        }

        if (currentVelocity != Vector2.zero) {
            rb2.velocity = currentVelocity;
            isMoving = true;
        }
        else {
            if (isMoving) {
                rb2.velocity = Vector2.zero;
                isMoving = false;
            }
        }
        animation.enabled = isMoving;


    }
}
