using UnityEngine;
using System.Collections;

public class UsableItem : MonoBehaviour {

    public enum ItemType {
        Weapon,
        Health,
        Stamina
    }

    public ItemType itemType;
    public int value;

}
