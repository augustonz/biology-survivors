using UnityEngine;
using UnityEngine.EventSystems;
public class ClickableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorConfig.instance.ChangeCursorHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorConfig.instance.ChangeCursorDefault();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CursorConfig.instance.ChangeCursorClicking();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CursorConfig.instance.ChangeCursorDefault();
    }
}
