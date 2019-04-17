#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class MovementPC : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler,IPointerDownHandler
{
    //UNITY_IOS || UNITY_ANDROID
    //UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR

    private Vector3 offset;
    void Start ()
    {
        print("script started");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        bool choose = GetComponent<Player_Controls>().isChoosen;
        //print("has clicked");
        if (choose == false)
        {
            GetComponent<Player_Controls>().isChoosen = true;
            transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<Player_Controls>().isChoosen = false;
            transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
    public void OnDrag(PointerEventData eventData)
    {
        //print("is dragging");
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       // print(eventData);
        //print(Input.touchCount);
    }
        /*if (Input.touchCount == 1)
        {
            RotateLE();
        }
    }
    void RotateLE()
    {
        print("accessed thrpugh event");
    }*/
   
}
#endif