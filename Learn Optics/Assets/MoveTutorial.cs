using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTutorial : MonoBehaviour, IBeginDragHandler, IDragHandler {

    private Vector3 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 cursorPosition = cursorPoint + offset;
        transform.position = cursorPosition;
    }
}
