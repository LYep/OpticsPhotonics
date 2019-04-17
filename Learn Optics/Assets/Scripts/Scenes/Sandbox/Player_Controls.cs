using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//added selectability, indicated by highlight
public class Player_Controls : MonoBehaviour
{
    Vector3 origin;
    bool isSelected;
    RaycastHit hit;
    private Vector3 offset;
    public bool isChoosen;

    // Use this for initialization
    void Start()
    {
        origin = Camera.main.gameObject.transform.position;
        isSelected = false;
        transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    void FixedUpdate()
    {
        //touch count collects info on entire screen, not just object
        if (Input.touchCount > 0)
        {
            /*print("hasgonethrough");
            if (Input.touchCount == 2)
            {
                print("2touches");
            }*/
            //Do Nothing
        }
        else
        {
            if (Input.GetKey(KeyCode.Q) && isChoosen == true)
            {
                transform.eulerAngles += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(KeyCode.E) && isChoosen == true)
            {
                transform.eulerAngles += new Vector3(0, 0, -1);
            }
            if (Input.GetKey(KeyCode.UpArrow) && isChoosen == true)
            {
                transform.position += new Vector3(0, 0.5F, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) && isChoosen == true)
            {
                transform.position += new Vector3(0, -0.5F, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow) && isChoosen == true)
            {
                transform.position += new Vector3(-0.5F, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow) && isChoosen == true)
            {
                transform.position += new Vector3(0.5F, 0, 0);
            }
        }

    }
    //delete no use
    float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);
        float result = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);
        if (cross.z > 0)
        {
            result = 360f - result;
        }
        return result;
    }
}

