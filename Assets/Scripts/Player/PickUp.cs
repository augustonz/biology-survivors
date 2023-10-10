using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour {
    public Transform target;
    public float pickUpSpeed = 5f;
    abstract public void onPick(Player player);
    public void Update() {
        if (target!=null) {
            target = FindTarget();
            Move();
        }
    }

    public void getPickUp(Transform target) {
        this.target = target;
    }

    public void Move() {
        Vector3 moveDirection = (target.position - transform.position).normalized;
        transform.position += moveDirection * Time.deltaTime * pickUpSpeed;
    }

    Transform FindTarget() {
        Player player = FindObjectOfType<Player>();
        if (player==null) return null;
        return player.transform;
    }
}