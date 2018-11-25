using UnityEngine;
using System.Collections;

public class Singletone<T> : MonoBehaviour where T : MonoBehaviour {

    public static T instance;

    void Awake() {
        instance = gameObject.GetComponent<T>();
    }

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
			
	}
}
