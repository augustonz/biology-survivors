using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class UpgradeScreen : MonoBehaviour
{
    Animator _anim;
    bool _isVisible;
    void Start() {
        _anim = GetComponent<Animator>();
    }

    void Update() {
    }

    void ToggleVisibility() {
        if (_isVisible) DisappearUpgradeScreen();
        else AppearUpgradeScreen();
    }

    public void AppearUpgradeScreen() {
        _anim.SetTrigger("appear");
        _isVisible=true;
        GameHandler.instance.ChangeState(GameState.MENU);
    }

    public void DisappearUpgradeScreen() {
        _anim.SetTrigger("disappear");
        _isVisible=false;
        GameHandler.instance.ChangeState(GameState.INGAME);
    }

}
