using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectMovementScript : MonoBehaviour
{
    [SerializeField] Vector3 _movementDir;
    Rigidbody2D _rb;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _maxDistance;

    void Start()
    {
        _movementDir = (Player.instance.transform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position = (transform.position + _movementDir * _moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, Player.instance.transform.position) > _maxDistance)
            Destroy(gameObject);
    }
}
