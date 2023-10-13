using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    Animator _anim;
    InputManager _input;
    bool _isVisible;
    void Start() {
        _anim = GetComponent<Animator>();
        _input = InputManager.instance;
    }

    void Update() {
        if (_input.GetPauseInput()) ToggleVisibility();
    }

    void ToggleVisibility() {
        if (GameHandler.instance._GameState==GameState.MENU) return;
        if (_isVisible) DisappearPauseScreen();
        else AppearPauseScreen();
    }

    void AppearPauseScreen() {
        _anim.SetTrigger("appear");
        _isVisible=true;
        GameHandler.instance.ChangeState(GameState.PAUSED);
    }

    public void DisappearPauseScreen() {
        _anim.SetTrigger("disappear");
        _isVisible=false;
        GameHandler.instance.ChangeState(GameState.INGAME);
    }

}
