using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private float smoothTime = .2f;

    [SerializeField] private bool canMove = true;

    private Vector3 moveDirection;
    private Vector3 refVal = Vector3.zero;

    private InputManager input;
    private Canvas canvas;
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        input = InputManager.instance;
        // canvas = GetComponentInChildren<Canvas>();
    }

    void Update()
    {
        if (!canMove) return;

        handlePlayerUpdate();
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        Debug.Log("OI");
        handlePlayerMovement();
    }

    void Enable()
    {
        canMove = true;
    }

    void Disable()
    {
        canMove = false;
    }

    void handlePlayerUpdate()
    {
        AdjustHealthBarLocation();
        moveDirection = input.GetMovementInput();
    }

    void handlePlayerMovement()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, moveDirection * playerSpeed * Time.fixedDeltaTime * 50, ref refVal, smoothTime);
    }

    void AdjustHealthBarLocation()
    {
        // canvas.transform.rotation = Quaternion.Euler(0, 0, -this.transform.rotation.z);
    }
}
