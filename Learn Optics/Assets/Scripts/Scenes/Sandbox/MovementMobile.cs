#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementMobile : MonoBehaviour
{
    private Vector3 offset;
    private Ray ray;
    private RaycastHit2D hit;
    private RaycastHit hitLens;
    Vector3 origin;
    Vector3 offset2;
    GameObject obj;
    private bool drag;
    private bool selected;
    Touch myTouch;
    private bool rotating;
    public const float Touch_Rotation_Width = 1;
    public const float Touch_Rotation_Minimum = 1;
    Vector2 startVector;
    Renderer rend;
    ActivateLens checkLensButton;
    GameObject[] opticalElements;
    Slider RoCSlider;
    Slider NIndexSlider;

    void Start()
    {
        print("testing compile specific");        
        origin = Camera.main.gameObject.transform.position;
        obj = null;
        drag = false;
        rotating = false;
        startVector = Vector2.zero;
        checkLensButton = GameObject.FindGameObjectWithTag("ActiveLens").GetComponent<ActivateLens>();
        RoCSlider = GameObject.Find("RoCSlider").GetComponent<Slider>();
        NIndexSlider = GameObject.Find("NIndexSlider").GetComponent<Slider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        if (Input.touchCount >= 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
        
            Physics.Raycast(ray.origin, ray.direction, out hitLens);  //Lenses don't have 2d colliders, use regular raycast
            //This will always be called, touch phases get recorded through this.
            if ((hit.collider && !checkLensButton.active) || (hitLens.collider && checkLensButton.active))
            {
                print(checkLensButton.active + "is it active");
                if (hit.collider)
                    obj = hit.collider.gameObject;
                else
                {
                    obj = hitLens.collider.gameObject;
                    rend = hitLens.collider.gameObject.GetComponent<Renderer>();
                }
                //print("finger hit objec " + hit.collider.name);
                myTouch = Input.touches[0];
                selected = true;  
            }

            if (hit.collider && Input.touchCount == 2)
                RotateMobile();
            if (Input.touchCount == 1)
                checkSelected();          
        }
        else
        {
            rotating = false;
        }
    }
    void checkSelected()
    {
        rotating = false;
        //print("touch phase" + myTouch.phase);
        //will go off if touch a screen
        if (selected == true && myTouch.phase == TouchPhase.Began)
        {
            drag = true;
            //print("finger is hit");
            offset2 = obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
        
            if (hit.collider)
            {
                bool check = obj.GetComponent<Player_Controls>().isChoosen;
                if (check == false)
                {
                    obj.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = true;
                    obj.GetComponent<Player_Controls>().isChoosen = true;
                }
                else
                {
                    obj.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = false;
                    obj.GetComponent<Player_Controls>().isChoosen = false;
                }
            }
            else
            {
                opticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
                foreach (GameObject objEle in opticalElements)
                {
                    objEle.GetComponent<Properties_Optical>().active = false;
                    objEle.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
                }
                bool check = obj.GetComponent<Properties_Optical>().active;
                if (check == false)
                {
                    //obj.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = true;
                   //rend.material.shader = Shader.Find("OutlineTransparent");
                    rend.material.SetColor("_OutlineColor", Color.green);
                    obj.GetComponent<Properties_Optical>().active = true;
                    if (obj.name == "ConcaveLens(Clone)")
                        RoCSlider.value = 11 - (obj.transform.localScale.z / 1.3F);
                    else
                        RoCSlider.value = 11 - (obj.transform.localScale.z / 5);

                   NIndexSlider.value = obj.GetComponent<Properties_Optical>().n_Index;
                }
                else
                {
                    //obj.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = false;
                     //rend.material.shader = Shader.Find("OutlineTransparent");
                    rend.material.SetColor("_OutlineColor", Color.black);
                    obj.GetComponent<Properties_Optical>().active = false;
                }
            }           
        }
        if (drag && myTouch.phase == TouchPhase.Moved)
        {
            //print("obj is moving");
           // drag = true;
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            obj.transform.position = new Vector3(point.x + offset2.x, point.y + offset2.y, 0);
        }
        //ended phase will be called if finger touches screen w/o select true
        //not needed for now
        if (drag && myTouch.phase == TouchPhase.Ended)
        {
            //print("exiting drag");
            drag = false;
            // obj = null;
        }
        selected = false;
    }
    void RotateMobile()
    {
        if (!rotating)
        {
            startVector = Input.touches[1].position - Input.touches[0].position;
            rotating = startVector.sqrMagnitude > Touch_Rotation_Width;
        }
        else
        {
            Vector2 currVector = Input.touches[1].position - Input.touches[0].position;
            float angleOffset = Vector2.Angle(startVector, currVector);

            if (angleOffset > Touch_Rotation_Minimum)
            {
                Vector3 Rotate = Vector3.Cross(startVector, currVector);
                obj.transform.eulerAngles += new Vector3(0, 0, (Rotate.z / 8000));
                startVector = currVector;
            }
        }
    }
}
#endif
