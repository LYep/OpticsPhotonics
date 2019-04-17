using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SIM2 : StandaloneInputModule
{

    public PointerEventData GetLastPointerEventDataPublic(int id)
    {
        return GetLastPointerEventData(id);
    }

    public GameObject GameObjectUnderPointer(int pointerId)
    {
        PointerEventData lastPointer = GetLastPointerEventData(pointerId);
        if (lastPointer != null)
            return lastPointer.pointerCurrentRaycast.gameObject;
        return null;
    }
    public GameObject GameObjectPointerPress(int pointerId)
    {
        PointerEventData lastPointer = GetLastPointerEventData(pointerId);
        if (lastPointer != null)
            return lastPointer.pointerPress.gameObject;
        return null;
    }
    public GameObject GameObjectUnderPointer()
    {
        return GameObjectUnderPointer(PointerInputModule.kMouseLeftId);
    }
    public GameObject GameObjectPointerPress()
    {
        return GameObjectPointerPress(PointerInputModule.kMouseLeftId);
    }
}