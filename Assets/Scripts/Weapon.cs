using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public WeaponsManager.WeaponType weaponType;
    private Joint2D joint;

	// Use this for initialization
	void Start () {
        joint = GetComponent<Joint2D>();
        AttachToHand();
	}

    private void AttachToHand() {
        var parent = transform.parent;
        if (parent == null) {
            return;
        }
        var rigidBody = parent.GetComponent<Rigidbody2D>();
        if (rigidBody == null) {
            return;
        }
        joint.connectedBody = rigidBody;
    }

    // Update is called once per frame
	void Update () {
		
	}
}
