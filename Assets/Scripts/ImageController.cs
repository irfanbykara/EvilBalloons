using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image backgroundImage;

    void Start()
    {
        if (backgroundImage != null)
        {

            RectTransform canvasRect = GetComponent<RectTransform>();
            RectTransform imageRect = backgroundImage.GetComponent<RectTransform>();
            canvasRect.sizeDelta = new Vector2(Screen.width, Screen.height);

            // Set the image size to match the Canvas size
            imageRect.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
}
