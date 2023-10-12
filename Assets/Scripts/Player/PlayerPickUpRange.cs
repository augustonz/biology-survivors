using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpRange:MonoBehaviour {

    [SerializeField] CircleCollider2D _pickUpCollider;
    void Start() {
    }

    public void SetPickUpRange(float value) {
        _pickUpCollider.radius = value;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("PickUp")) {
            PickUp pickUp = collider.GetComponent<PickUp>();
            pickUp.getPickUp(transform);
        }
    }
}