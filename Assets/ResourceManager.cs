using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singletone<ResourceManager> {

    public enum WeaponType {
        Hammer,
        ChainedHammer,
        Shotgun
    }

    [SerializeField] private Weapon[] weaponsList;
    [SerializeField] private UsableItem[] weaponsPickups;
    [SerializeField] private Player playerPrefab;


    public void SpawnPlayer(Player.PlayerNumbers playerNumber, Vector3 position) {
        var player = Instantiate(playerPrefab, position, Quaternion.identity);
        player.Init(playerNumber);
    }

    public Weapon CreateWeapon(WeaponType wich, Transform parent) {
        return Instantiate(weaponsList[(int)wich], parent);
    }

    public void CreatePickup(UsableItem.ItemType itemType, int value, Vector3 position) {
        switch (itemType) {
            case UsableItem.ItemType.Weapon:
                Instantiate(weaponsPickups[value], position, Quaternion.identity);
                break;
            /*case UsableItem.ItemType.Health:
                break;
            case UsableItem.ItemType.Stamina:
                break;*/
            default:
                Debug.LogError("ItemType not handled: " + itemType);
                break;
        }
    }
}
