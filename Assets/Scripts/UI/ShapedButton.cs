using UnityEngine;
using UnityEngine.UI;
public class ShapedButton : MonoBehaviour
{
    
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.001f;
    }
}
