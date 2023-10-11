using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {

    [SerializeField] float _cameraTilt;
    [SerializeField] float _cameraDeadZoneRadius;
    [SerializeField] float _cameraSpeed;
    Camera _cam;
    Vector3 _targetPosition;
    GameObject _player;


    void Start() {
        _cam = GetComponent<Camera>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        Vector2 mouseDirection = (Vector2) Input.mousePosition - new Vector2(Screen.width/2,Screen.height/2);
        Vector2 moveAmount = mouseDirection * _cameraTilt;

        _targetPosition = _player.transform.position;

        if (moveAmount.magnitude>_cameraDeadZoneRadius) _targetPosition = (Vector2) _targetPosition + moveAmount  - Vector2.one * _cameraDeadZoneRadius;

        _targetPosition = new Vector3(_targetPosition.x,_targetPosition.y,-10);

        transform.position = Vector3.Lerp(transform.position,_targetPosition,_cameraSpeed*Time.deltaTime);
    }
}