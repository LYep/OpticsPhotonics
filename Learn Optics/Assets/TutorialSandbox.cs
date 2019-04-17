using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialSandbox : MonoBehaviour
{
    private int counter;
    GameObject RoCSlider;
    GameObject FirstPanel;
    GameObject SecondPanel;
    GameObject ThirdPanel;
    GameObject FourthPanel;
    GameObject FifthPanel;
    GameObject LensPanel;
    GameObject OpticalElementPanel;
    GameObject ParameterPanel;
    GameObject NewLens;
    GameObject TutorialButton;
    GameObject ModeScene;
    GameObject MainMenuPanel;
    Animator OEP;
    Animator ParameterPanelAnimator;
    Animator OpticalElementPanelButton;
    Ray ray;
    RaycastHit hit;
    Touch TutorialTouch;
    Vector3 PosTouchStart;
    Vector3 PosTouchEnd;
    PointerEventData pointer;
    bool CheckTut;
    bool DragDrop;
    bool ChangeRadius;
    bool TouchDrag;
    bool TouchDragSlider;
    float StartRadius;
    Touch FirstTouch;
    GameObject MenuButton;

    void Start()
    {
        MainMenuPanel = GameObject.Find("MenuPanel");
        SaveData.dataFile.Load();
        if (SaveData.dataFile.SandBoxTutorialActive)
        {
            gameObject.SetActive(false); //enable for release
            print(" also " + Application.persistentDataPath);
        }
        else
        {
            MenuButton = GameObject.FindGameObjectWithTag("MenuButton");
            counter = 0;
            DragDrop = false;
            ChangeRadius = false;
            TouchDrag = false;
            TouchDragSlider = false;
            OpticalElementPanel = GameObject.Find("Optical Element Menu");
            RoCSlider = GameObject.Find("RoCSlider");
            OEP = OpticalElementPanel.GetComponent<Animator>();
            ParameterPanel = GameObject.Find("Parameter Menu");
            ParameterPanelAnimator = ParameterPanel.GetComponent<Animator>();
            OpticalElementPanelButton = GameObject.FindGameObjectWithTag("MenuButton").GetComponent<Animator>();
            TutorialButton = GameObject.Find("TutorialButton");
            ModeScene = GameObject.FindGameObjectWithTag("ActiveLens");
            FirstPanel = transform.GetChild(0).gameObject;
            SecondPanel = transform.GetChild(1).gameObject;
            ThirdPanel = transform.GetChild(2).gameObject;
            FourthPanel = transform.GetChild(3).gameObject;
            FifthPanel = transform.GetChild(5).gameObject;
            LensPanel = transform.GetChild(4).gameObject;
            FirstPanel.SetActive(true);
            SecondPanel.SetActive(false);
            ThirdPanel.SetActive(false);
            FourthPanel.SetActive(false);
            FifthPanel.SetActive(false);
            LensPanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (counter == 1 && !DragDrop)
        {
            string nameCheck = EventSystem.current.currentSelectedGameObject.name;
            if (nameCheck == "Concave Lens" || nameCheck == "Convex Lens" || nameCheck == "Convex Mirror" || nameCheck == "Concave Mirror")
            {               
                SecondPanel.SetActive(false);
                LensPanel.SetActive(true);
                DragDrop = true;
            }
            /*if (Input.touchCount == 1)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider)
                {
                    SecondPanel.SetActive(false);
                    TutorialTouch = Input.GetTouch(0);                    
                }
                if (TutorialTouch.phase == TouchPhase.Began)
                {
                    PosTouchStart = TutorialTouch.position;
                }
                if (TutorialTouch.phase == TouchPhase.Moved)
                {
                    DragDrop = true;
                }
                if (DragDrop && TutorialTouch.phase == TouchPhase.Ended)
                {
                    PosTouchEnd = TutorialTouch.position;
                    // if(TutorialTouch.deltaPosition > )
                }
            }*/            
        }
        else if (DragDrop)
        {
            //PointerEventData pointer = new PointerEventData(EventSystem.current);          
            /*if (EventSystem.current.currentSelectedGameObject.tag == "OpticalElement")
            {
                if (Mathf.Abs(EventSystem.current.currentSelectedGameObject.transform.localPosition.y + 22) <= 1)
                {
                    LensPanel.SetActive(false);
                    counter++;
                    ThirdPanel.SetActive(true);
                }
            }*/
            #if UNITY_IOS || UNITY_ANDROID 
            if (Input.touchCount == 1) {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Physics.Raycast(ray.origin, ray.direction, out hit);
                if (hit.collider != null)
                {
                    if(hit.collider.gameObject.tag == "OpticalElement")
                    {
                        TouchDrag = true;
                        FirstTouch = Input.GetTouch(0);
                        NewLens = hit.collider.gameObject;
                    }
                }
                if(TouchDrag)
                {
                    if (Mathf.Abs(NewLens.transform.localPosition.y) <= 0.1)
                    {
                        LensPanel.SetActive(false);
                        counter++;
                        ThirdPanel.SetActive(true);
                        TouchDrag = false;
                        DragDrop = false;
                    }
                }
            }
            //TouchDrag = false;
            #else
            if (EventSystem.current.gameObject.GetComponent<SIM2>().GameObjectUnderPointer(-1) != null)
            {
                if (EventSystem.current.gameObject.GetComponent<SIM2>().GameObjectUnderPointer(-1).tag == "OpticalElement")
                {
                    NewLens = EventSystem.current.gameObject.GetComponent<SIM2>().GameObjectUnderPointer(-1);
                    if (Mathf.Abs(NewLens.transform.localPosition.y) <= 0.1)
                    {
                        LensPanel.SetActive(false);
                        counter++;
                        ThirdPanel.SetActive(true);
                        DragDrop = false;
                    }
                }
            }
            #endif
        }
        else if(counter == 3 && !ChangeRadius)
        {
            #if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                /*ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Physics.Raycast(ray.origin, ray.direction, out hit);*/
                if(EventSystem.current.currentSelectedGameObject.name == "RoCSlider")
                {
                    if (StartRadius != RoCSlider.GetComponent<Slider>().value)
                    {
                        FourthPanel.SetActive(false);
                        counter++;
                        FifthPanel.SetActive(true);
                        ChangeRadius = true;
                    }
                }
                /*if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == "RoCSlider")
                    {
                        TouchDragSlider = true;
                        FirstTouch = Input.GetTouch(0);
                        //NewLens = hit.collider.gameObject;
                    }
                }
                if (TouchDragSlider)
                {
                    if (StartRadius != RoCSlider.GetComponent<Slider>().value)
                    {
                        FourthPanel.SetActive(false);
                        counter++;
                        FifthPanel.SetActive(true);
                        TouchDragSlider = false;
                    }
                }*/
            }
            #else
            if (EventSystem.current.gameObject.GetComponent<SIM2>().GameObjectPointerPress(-1) != null)
            {
                if (EventSystem.current.gameObject.GetComponent<SIM2>().GameObjectPointerPress(-1).name == "RoCSlider")
                {
                    print(StartRadius + "r");
                    print(RoCSlider.GetComponent<Slider>().value + "r2");
                    if (StartRadius != RoCSlider.GetComponent<Slider>().value)
                    {
                        
                        FourthPanel.SetActive(false);
                        counter++;
                        FifthPanel.SetActive(true);
                        ChangeRadius = true;
                    }
                }
            }
            #endif
        }
    }
   
    public void DisableOnStartUp(bool Check)
    {
        if (Check)
            SaveData.dataFile.SandBoxTutorialActive = true;
        else
            SaveData.dataFile.SandBoxTutorialActive = false;
    }
    public void NextPanel()
    {
        counter++;
        SwitchPanel();
    }
    public void PrevPanel()
    {
        counter--;
        SwitchPanel();
    }
    void SwitchPanel()
    {
        switch (counter)
        {
            case 0:
                MenuButton.GetComponent<MenuButtonClick>().toggleMenu();
                FirstPanel.SetActive(true);
                SecondPanel.SetActive(false);
                break;

            case 1:
                MenuButton.GetComponent<MenuButtonClick>().toggleMenu();
                FirstPanel.SetActive(false);
                SecondPanel.SetActive(true);
                ThirdPanel.SetActive(false);
                break;

            case 2:
                SecondPanel.SetActive(false);
                ThirdPanel.SetActive(true);
                FourthPanel.SetActive(false);
                break;

            case 3:
                StartRadius = RoCSlider.GetComponent<Slider>().value;
                ThirdPanel.SetActive(false);
                FourthPanel.SetActive(true);
                break;
            case 4:
                FourthPanel.SetActive(false);
                break;
        }
    }
    public void ExitTutorial()
    {
        SaveData.dataFile.Save();
        gameObject.GetComponent<Animator>().SetTrigger("ScaleDown");
        ModeScene.GetComponent<ActivateLens>().active = true;
        ModeScene.GetComponent<ActivateLens>().switchMode();

    }
    public void OpenTutorial()
    {
        gameObject.SetActive(true);
        GetComponent<Animator>().Play("Idle");
        OEP.SetBool("toggleMenu", false);
        ParameterPanelAnimator.SetBool("toggleMenu", false);
        OpticalElementPanelButton.SetBool("toggleMenu", false);
        FirstPanel = transform.GetChild(0).gameObject;
        SecondPanel = transform.GetChild(1).gameObject;
        ThirdPanel = transform.GetChild(2).gameObject;
        FourthPanel = transform.GetChild(3).gameObject;
        FifthPanel = transform.GetChild(5).gameObject;
        LensPanel = transform.GetChild(4).gameObject;
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
        FourthPanel.SetActive(false);
        FifthPanel.SetActive(false);
        LensPanel.SetActive(false);
        MainMenuPanel.GetComponent<Animator>().SetTrigger("PopOut");
        counter = 0;
        DragDrop = false;
        ChangeRadius = false;
        TouchDrag = false;
        TouchDragSlider = false;
    }
}
