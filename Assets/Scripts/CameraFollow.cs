using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float _cameraTilt;

    void Start() {
    }

    void Update() {

        Vector2 mouseDirection = (Vector2) Input.mousePosition - new Vector2(Screen.width/2,Screen.height/2);
        Vector2 finalPosition = (Vector2) transform.parent.position + mouseDirection * _cameraTilt;

        transform.position = new Vector3(finalPosition.x,finalPosition.y,-10);
    }
}
