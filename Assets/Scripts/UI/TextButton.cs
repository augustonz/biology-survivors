using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta*=1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta/=1.1f;
    }
}
