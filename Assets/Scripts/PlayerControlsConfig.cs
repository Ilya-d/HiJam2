using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data", order = 1)]
public class PlayerControlsConfig : ScriptableObject {

    public KeyCode keyLeft = KeyCode.A;
    public KeyCode keyRight = KeyCode.D;
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public KeyCode keyRotateLeft = KeyCode.C;
    public KeyCode keyRotateRight = KeyCode.V;
    public KeyCode keyUse = KeyCode.B;

}
