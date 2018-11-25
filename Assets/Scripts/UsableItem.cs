using UnityEngine;
using System.Collections;

public class UsableItem : MonoBehaviour {

    public enum ItemType {
        Weapon,
        Player,
        Health,
        Stamina
    }

    public ItemType itemType;
    public int value;

}
