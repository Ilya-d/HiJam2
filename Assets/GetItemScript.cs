using UnityEngine;

public class GetItemScript : MonoBehaviour {

    [SerializeField] private GameObject item;

    bool itemAvailable = true;

    public bool ItemAvailable() {
        return itemAvailable;
    }

    public void GetItem() {
        itemAvailable = false;
    }
}
