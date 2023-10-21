using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float _cameraTilt;
    bool _enabled;

    void Start() {
        _enabled=true;
    }

    public void OnChange(GameState gameState) {
        if (gameState==GameState.INGAME) {
            _enabled = true;
        } else {
            _enabled = false;
        }
    }

    void Update() {
        if (_enabled) {
            Vector2 mouseDirection = (Vector2) Input.mousePosition - new Vector2(Screen.width/2,Screen.height/2);
            Vector2 finalPosition = (Vector2) transform.parent.position + mouseDirection * _cameraTilt;

            transform.position = new Vector3(finalPosition.x,finalPosition.y,-10);
        }
    }
}
