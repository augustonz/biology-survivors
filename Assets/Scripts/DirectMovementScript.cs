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

    // Start is called before the first frame update
    void Start()
    {
        _movementDir = (Player.instance.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (transform.position + _movementDir * _moveSpeed * Time.fixedDeltaTime);

        if(Vector3.Distance(transform.position, Player.instance.transform.position) > _maxDistance)
            Destroy(gameObject);
    }
}
