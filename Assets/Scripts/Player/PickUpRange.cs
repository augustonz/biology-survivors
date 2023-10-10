using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRange:MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("PickUp")) {
            PickUp pickUp = collider.GetComponent<PickUp>();
            pickUp.getPickUp(transform);
        }
    }
}