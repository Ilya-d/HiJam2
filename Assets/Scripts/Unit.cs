using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public float maxHealth = 100;
    protected float currentHealth;

    public bool isAlive {
        get { return (currentHealth > 0); }
    }

    public void Hit(float weaponSpeed) {
        if(weaponSpeed > 10) {
            currentHealth -= 15;
        }
        else if(weaponSpeed > 5) {
            currentHealth -= 10;
        }
        else if(weaponSpeed > 2) {
            currentHealth -= 5;
        }
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
        Debug.Log("currentHealth=" + currentHealth);
    }

	// Use this for initialization
    void Start() {
        currentHealth = maxHealth;

	}

	// Update is called once per frame
	void Update()
	{
			
	}
}
