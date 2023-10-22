using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float _speed;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, _speed * Time.time);
    }
}
