using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : Singletone<WeaponsManager> {

    public enum WeaponType {
        Hammer,
        ChainedHammer,
        Shotgun
    }

    [SerializeField] private Weapon[] weaponsList;

    public Weapon CreateWeapon(WeaponType wich, Transform parent) {
        return Instantiate(weaponsList[(int)wich], parent);
    }
}
