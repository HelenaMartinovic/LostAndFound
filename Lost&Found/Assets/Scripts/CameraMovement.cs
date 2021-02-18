using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 offset;//TODO staviti nekak Vector2s

    // Use this for initialization
    void Start() {
        offset = transform.position - GameManager.instance.player.transform.position;
    }

    void LateUpdate() {
        transform.position = GameManager.instance.player.transform.position + offset;
    }
}
