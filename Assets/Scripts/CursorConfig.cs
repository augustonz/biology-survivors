using UnityEngine;

public class CursorConfig : MonoBehaviour
{
    [SerializeField]Texture2D _clickingCursor;
    [SerializeField]Texture2D _defaultCursor;
    [SerializeField]Texture2D _hoverCursor;
    public static CursorConfig instance;
    void Awake() {
        instance = this;
    }

    public void HideCursor() {
        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor() {
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeCursorClicking() {
        Cursor.SetCursor(_clickingCursor,new Vector2(7,7),CursorMode.Auto);
    }

    public void ChangeCursorHover() {
        Cursor.SetCursor(_hoverCursor,new Vector2(7,7),CursorMode.Auto);
    }

    public void ChangeCursorDefault() {
        Cursor.SetCursor(_defaultCursor,new Vector2(7,7),CursorMode.Auto);
    }
}
