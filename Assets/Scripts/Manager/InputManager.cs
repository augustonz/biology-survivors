using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;


    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public UIVirtualJoystick moveJoystick;

    public InputAction moveAction;
    Vector2 _moveDir;

    public InputAction clickAction;

    public InputAction reloadAction;
    public InputAction pauseAction;

    GameState _gameState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnable()
    {
        moveAction.Enable();
        clickAction.Enable();
        reloadAction.Enable();
        pauseAction.Enable();
    }

    public void OnDisable()
    {
        moveAction.Disable();
        clickAction.Disable();
        reloadAction.Disable();
    }

    public void SetMoveDir(Vector2 moveDir)
    {
        _moveDir = moveDir.normalized;
    }
    public Vector2 GetMovementInput()
    {
        if (moveJoystick.gameObject.activeInHierarchy)
            return _moveDir;
        else
            return moveAction.ReadValue<Vector2>();
    }

    public bool GetClickInput()
    {
        return Mathf.Approximately(clickAction.ReadValue<float>(), 1f);
    }

    public bool GetReloadInput()
    {
        return Mathf.Approximately(reloadAction.ReadValue<float>(), 1f);
    }

    public bool GetPauseInput()
    {
        return pauseAction.triggered;
    }

    public void ChangeState(GameState gameState)
    {
        _gameState = gameState;
        if (gameState==GameState.INGAME) OnEnable();
        if (gameState!=GameState.INGAME) OnDisable();
    }
}
