#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MovementLens : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler, IPointerDownHandler {
    private Vector3 offset;
    public bool active;
    GameObject lensCheck;
    MeshRenderer rend;
    GameObject[] opticalElements;
    Slider RoCSlider;
    Slider NIndexSlider;
    public void Start()
    {
        lensCheck = GameObject.FindGameObjectWithTag("ActiveLens");
        active = lensCheck.GetComponent<ActivateLens>().active;
        rend = gameObject.GetComponent<MeshRenderer>();
        RoCSlider = GameObject.Find("RoCSlider").GetComponent<Slider>();
        NIndexSlider = GameObject.Find("NIndexSlider").GetComponent<Slider>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(active)
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
       // print("sfsdf");
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (active)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
        }
    }

    //for changing properties
    public void OnPointerClick(PointerEventData eventData)
    {
        if (active)
        {
            //find better way to implement this
            opticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
            foreach (GameObject obj in opticalElements)
            {
                obj.GetComponent<Properties_Optical>().active = false;
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
            }

            bool check = gameObject.GetComponent<Properties_Optical>().active;
            
            if (check == false)
            {
                rend.material.SetColor("_OutlineColor", Color.green);
                gameObject.GetComponent<Properties_Optical>().active = true;
                if (gameObject.name == "ConcaveLens(Clone)")
                    RoCSlider.value = 11 - (gameObject.transform.localScale.z / 1.3F);
                else
                    RoCSlider.value = 11 - (gameObject.transform.localScale.z / 5);
                NIndexSlider.value = gameObject.GetComponent<Properties_Optical>().n_Index;
            }
            else
            {
                rend.material.SetColor("_OutlineColor", Color.black);
                gameObject.GetComponent<Properties_Optical>().active = false;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //print(eventData);
        //print(Input.touchCount);
    }
}
#endif
