using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit {

    public enum PlayerNumbers {
        player1 = 0,
        player2 = 1
    }

    [SerializeField] private Rigidbody2D handRb;
    [SerializeField] private Transform handContainer;


    [SerializeField] private float force = 50;

    [SerializeField] public bool impulse;

    public PlayerNumbers playerNumber {
        get;
        private set;
    }

    private Rigidbody2D rb2;
    [SerializeField] float speed = 10f;
    private float currentSpeed;

    private Vector2 currentVelocity = new Vector2();
    private bool isMoving;


    public Transform PlayerTransform {
        get { return transform; }
    }


    [Header("Настройки энергии")]
    public float maxEnergy = 100;
    private float currentEnergy;
    [SerializeField] private float energyDecreaseSpeed = 10f;
    [SerializeField] private float energyHitCost = 15f;
    [SerializeField] private float energyRegen = 10f;
    [SerializeField] private float multEnergy = 4;
    [SerializeField] [Range(0, 1)] private float speedDebuf = 0.7f;

    private bool hitAvailable = true;
    private bool atacking = false;

    [Header("Настройки UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image energyBar;
    [SerializeField] private Color fullHealth;
    [SerializeField] private Color halfHealth;
    [SerializeField] private Color lowHealth;

    private Weapon currentWeapon;
    private ResourceManager.WeaponType currentWeaponType;

    [SerializeField] private Sprite imageLeft;
    [SerializeField] private Sprite imageRight;
    [SerializeField] private Sprite imageUp;
    [SerializeField] private Sprite imageDown;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator animation;
    [SerializeField] private GameObject pickupNotify;

    private PlayerControlsConfig controlls;

    private UsableItem itemOnFloor;


    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        SetWeapon(ResourceManager.WeaponType.Hammer);
        currentSpeed = speed;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        UpdatePickupUi();
        EventsManager.SendEvent(EventsManager.EventType.PlayerSpawn, this);
    }

    public void Init(PlayerNumbers number) {
        playerNumber = number;
        controlls = ResourceManager.instance.playersControlls[(int)playerNumber];
    }

    void FixedUpdate() {
        Movement();
        Attack();
    }

    private void Update() {
        CalculateUI();
        CheckEnergy();
        UseButton();
    }

    private void CheckEnergy() {
        if (currentEnergy < maxEnergy) {
            if (hitAvailable && !atacking) {
                if (isMoving) {
                    currentEnergy += Time.deltaTime * energyRegen;
                }
                else {
                    currentEnergy += Time.deltaTime * energyRegen * 2;
                }
            }
            else if (!hitAvailable) {
                currentEnergy += Time.deltaTime * energyRegen * multEnergy;
            }
        }

        if (currentEnergy <= 0) {
            hitAvailable = false;
            currentSpeed = speed * speedDebuf;
            StartCoroutine(WaitForRest());
        }
        maxEnergy = Mathf.Clamp(maxEnergy, 0, 100);
    }

    IEnumerator WaitForRest() {
        yield return new WaitForSeconds(2);
        hitAvailable = true;
        currentSpeed = speed;
    }

    private void SetWeapon(ResourceManager.WeaponType weapon) {
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = ResourceManager.instance.CreateWeapon(weapon, handContainer);
        currentWeaponType = weapon;
    }

    private void CalculateUI() {
        /*healthBar.fillAmount = currentHealth / maxHealth;
        energyBar.fillAmount = currentEnergy / maxEnergy;

        if (healthBar.fillAmount < 0.2) healthBar.color = lowHealth;
        else if (healthBar.fillAmount < 0.6) healthBar.color = halfHealth;
        else healthBar.color = fullHealth;*/
    }

    private void UseButton() {
        if (itemOnFloor != null && Input.GetKeyDown(controlls.keyUse)) {
            UseItem(itemOnFloor);
            return;
        }
        if (Input.GetKeyDown(controlls.keyUse)) {
            if (currentWeaponType == ResourceManager.WeaponType.Shotgun) {
                currentWeapon.gameObject.GetComponent<ShotGun>().Shoot();
            }
        }
    }

    private void UseItem(UsableItem item) {
        switch (item.itemType) {
            case UsableItem.ItemType.Weapon:
                ResourceManager.instance.CreatePickup(UsableItem.ItemType.Weapon, (int)currentWeapon.weaponType, transform.position);
                SetWeapon((ResourceManager.WeaponType)itemOnFloor.value);
                Destroy(itemOnFloor.gameObject);
                break;
            case UsableItem.ItemType.Player:
                ResourceManager.instance.SpawnPlayer((PlayerNumbers)item.value, transform.position);
                break;
            default:
                Debug.LogError("Unhandled item use: " + item.itemType);
                break;
        }

        itemOnFloor = null;
    }

    private void Attack() {
        if (currentWeaponType != ResourceManager.WeaponType.Shotgun) {
            if (hitAvailable) {
                if (Input.GetKeyDown(controlls.keyRotateLeft)) {
                    if (currentEnergy >= energyHitCost) {
                        handRb.AddTorque(force, ForceMode2D.Impulse);
                        currentEnergy -= energyHitCost;
                    }
                }
                else if (Input.GetKeyDown(controlls.keyRotateRight)) {
                    if (currentEnergy >= energyHitCost) {
                        handRb.AddTorque(-force, ForceMode2D.Impulse);
                        currentEnergy -= energyHitCost;
                    }
                }

                if (Input.GetKey(controlls.keyRotateLeft)) {
                    atacking = true;
                    handRb.AddTorque(force, ForceMode2D.Force);
                    currentEnergy -= Time.deltaTime * energyDecreaseSpeed;
                }
                else if (Input.GetKey(controlls.keyRotateRight)) {
                    atacking = true;
                    handRb.AddTorque(-force, ForceMode2D.Force);
                    currentEnergy -= Time.deltaTime * energyDecreaseSpeed;
                }
                else {
                    atacking = false;
                }
            }
        }
        else {
            if (Input.GetKey(controlls.keyRotateLeft)) {
                handRb.AddTorque(force / 5, ForceMode2D.Force);
            }
            else if (Input.GetKey(controlls.keyRotateRight)) {
                handRb.AddTorque(-force / 5, ForceMode2D.Force);
            }
        }
    }


    private void Movement() {

        if (Input.GetKey(controlls.keyRight)) {
            currentVelocity.x = currentSpeed;
            playerSprite.sprite = imageRight;
        }
        else if (Input.GetKey(controlls.keyLeft)) {
            currentVelocity.x = -currentSpeed;
            playerSprite.sprite = imageLeft;
        }
        else {
            currentVelocity.x = 0;
        }

        if (Input.GetKey(controlls.keyUp)) {
            currentVelocity.y = currentSpeed;
            playerSprite.sprite = imageUp;
        }
        else if (Input.GetKey(controlls.keyDown)) {
            currentVelocity.y = -currentSpeed;
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

    // Вызывается из Unit через SendMessage
    void OnDeath() {
        ResourceManager.instance.CreatePickup(UsableItem.ItemType.Player, (int)playerNumber, transform.position);
        EventsManager.SendEvent(EventsManager.EventType.PlayerDeath, this);
    }

	private void OnTriggerEnter2D(Collider2D col) {
        var item = col.GetComponent<UsableItem>();
        if (item == null) {
            return;
        }
        itemOnFloor = item;
        UpdatePickupUi();
	}

    private void OnTriggerExit2D(Collider2D col) {
        var item = col.GetComponent<UsableItem>();
        if (item == null) {
            return;
        }
        if (item == itemOnFloor) {
            itemOnFloor = null;
        }
        UpdatePickupUi();
    }

    private void UpdatePickupUi() {
        pickupNotify.SetActive(itemOnFloor != null);
    }

}
