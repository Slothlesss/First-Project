using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickUi : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image imgJoyStickBg;
    private Image imgJoyStick;
    private Vector2 posInput;

    // Start is called before the first frame update
    void Start()
    {
        imgJoyStickBg = GetComponent<Image>();
        imgJoyStick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoyStickBg.rectTransform, eventData.position, eventData.pressEventCamera, out posInput))
        {
            posInput.x = posInput.x / (imgJoyStickBg.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imgJoyStickBg.rectTransform.sizeDelta.y);

            if (posInput.magnitude  > 0.2f) posInput = posInput.normalized;
            
            imgJoyStick.rectTransform.anchoredPosition = new Vector2(
                posInput.x * (imgJoyStickBg.rectTransform.sizeDelta.x/5),
                posInput.y * (imgJoyStickBg.rectTransform.sizeDelta.y/5));
        }
    }
    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        imgJoyStick.rectTransform.anchoredPosition = Vector2.zero;
    }
    
}
