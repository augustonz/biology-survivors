using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed;
    [SerializeField] private float smoothTime = .2f;

    [SerializeField] private bool canMove = true;

    private Vector3 moveDirection;
    public Vector3 MoveDirection { get => moveDirection; }
    private Vector3 refVal = Vector3.zero;

    private InputManager input;
    private Rigidbody2D rb;

    private float _timerBurst;
    private float refBurst;

    private Player _player;

    void Awake()
    {
        _player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        input = InputManager.instance;
    }

    void Update()
    {
        if (_timerBurst > 0)
        {
            _timerBurst -= Time.deltaTime;
            if (_timerBurst < 0)
            {
                playerSpeed = _player.GetStat(TypeStats.SPEED);
            }
            else
            {
                playerSpeed = Mathf.SmoothDamp(playerSpeed, _player.GetStat(TypeStats.SPEED), ref refBurst, _timerBurst);
            }
        }

        if (!canMove) return;

        handlePlayerUpdate();
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        handlePlayerMovement();
    }

    public void Enable()
    {
        canMove = true;
    }

    public void Disable()
    {
        canMove = false;
    }

    void handlePlayerUpdate()
    {
        moveDirection = input.GetMovementInput();
    }

    void handlePlayerMovement()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, moveDirection * playerSpeed * Time.fixedDeltaTime * 50, ref refVal, smoothTime);
    }

    public void SetSpeed(float sp)
    {
        playerSpeed = sp;
    }

    public void SpeedBurst(float timerBurst)
    {
        _timerBurst = timerBurst;
        playerSpeed = _player.GetStat(TypeStats.SPEED_BURST);
    }
}
