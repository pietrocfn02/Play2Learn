using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour, IDropHandler
{
    private Vector3 firstPos;
    private bool done = false;
    Vector3 firstPos;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform tmp = eventData.pointerDrag.GetComponent<RectTransform>();
            if (eventData.pointerEnter.name == eventData.pointerDrag.name)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                if (eventData.pointerEnter.GetComponent<TMP_Text>())
                {
                    eventData.pointerEnter.GetComponent<TMP_Text>().text = "";
                }
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                Debug.Log(firstPos);
            }
            
        }
    }

    void OnMouseDown()
    {
        firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
