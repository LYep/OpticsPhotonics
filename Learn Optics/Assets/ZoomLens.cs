using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomLens : MonoBehaviour, IPointerDownHandler
{
    private bool moveRight;
    private bool active;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 currPos;

    void Start()
    {
        active = true;
        endPos = new Vector3(3, 0, 0);
        startPos = new Vector3(-13.32f, 0, 0);
        moveRight = true; //Lens is initalized at the left side, so first move right
    }
   
    void Update()
    {
        if (active)
        {
            currPos = transform.localPosition;

            if (currPos == startPos)
                moveRight = true;
            else if (currPos == endPos)
                moveRight = false;

            float step = 4 * Time.deltaTime;
            if (moveRight)
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, step);
            else
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, step);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (active)
            active = false;
        else
            active = true;
    }
}
