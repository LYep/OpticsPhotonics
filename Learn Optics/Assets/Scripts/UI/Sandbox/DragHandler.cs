using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject clicker;
    public GameObject lens;
    private bool isPointerOverGameObject;
    float downTimer;
    ScrollRect scroller;
    GameObject newLens;
    Vector3 eulerPos;
    PointerEventData data;

    void Start()
    {
        scroller = transform.parent.parent.parent.GetComponent<ScrollRect>();
        if (lens.name == "ConcaveLens")
        {
            eulerPos = new Vector3(0, 90, 0);
        }
        else if (lens.name == "ConvexMirror")
        {
            eulerPos = new Vector3(0, -90, 90);
        }
        else
        {
            eulerPos = new Vector3(0, 90, 90);
        }
    }

    void Update()
    {
        if (isPointerOverGameObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                downTimer = Time.time;
            }

            if (Input.GetMouseButtonUp(0))
            {
                print("we are here");
                scroller.enabled = true;
            }

            if (Input.GetMouseButton(0))
            {
                if ((Time.time - downTimer) > 1)
                {
                    print("straert dragg");
                    scroller.enabled = false;
                    Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    newLens = Instantiate(lens, new Vector3(cursor.x, cursor.y, 0), Quaternion.Euler(eulerPos));
                    clicker.GetComponent<MenuButtonClick>().toggleMenu();
                    
                    StartCoroutine(Holding());                 
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverGameObject = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverGameObject = true;
    }
    IEnumerator Holding()
    {
        ExecuteEvents.Execute<IBeginDragHandler>(newLens, data, ExecuteEvents.beginDragHandler);
        yield return null;
    }
}