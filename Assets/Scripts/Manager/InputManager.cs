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

    public InputAction moveAction;

    public InputAction clickAction;

    public InputAction reloadAction;

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
    }

    public void OnDisable()
    {
        moveAction.Disable();
        clickAction.Disable();
        reloadAction.Disable();
    }

    public Vector2 GetMovementInput()
    {
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

    public void ChangeState(GameState gameState)
    {
        _gameState = gameState;
        if (gameState==GameState.INGAME) OnEnable();
        if (gameState!=GameState.INGAME) OnDisable();
    }
}
