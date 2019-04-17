using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject SinglePanel;
    Animator Return;
    Animator Main;
    Animator Single;
    GameObject Tutorial;

    private void Start()
    {
        Main = MainPanel.GetComponent<Animator>();
        Single = SinglePanel.GetComponent<Animator>();
        Return = GameObject.Find("ReturnMainButton").GetComponent<Animator>();
        Tutorial = GameObject.Find("TutorialButton");
        #if UNITY_WEBGL || UNITY_EDITOR
            Screen.SetResolution(600, 1000, false);
        #endif
    }
    public void OnSingleClick()
    {
        Main.SetTrigger("SlideOutTrig");
        Single.SetTrigger("SlideInTrig");
        Return.SetTrigger("SlideInTrig");
        Tutorial.SetActive(false);      
    }
    public void OnReturn()
    {
        Main.SetTrigger("SlideInTrig");
        Single.SetTrigger("SlideOutTrig");
        Return.SetTrigger("SlideOutTrig");
        Tutorial.SetActive(true);
    }
    public void LoadMultipleLens()
    {
        SceneManager.LoadSceneAsync("MultipleLenses");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        #if UNITY_WEBGL || UNITY_EDITOR
            Screen.SetResolution(960, 600, false);
        #endif
    }
    public void LoadConvexScene()
    {
        SceneManager.LoadSceneAsync("LearnConvex");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        #if UNITY_WEBGL || UNITY_EDITOR
            Screen.SetResolution(960, 600, false);
        #endif
    }

    public void LoadConcaveScene()
    {
        SceneManager.LoadSceneAsync("LearnConcave");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        #if UNITY_WEBGL || UNITY_EDITOR
         Screen.SetResolution(960, 600, false);
        #endif
    }

    public void LoadMirrorScene()
    {
        SceneManager.LoadSceneAsync("LearnMirrors");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        #if UNITY_WEBGL || UNITY_EDITOR
         Screen.SetResolution(960, 600, false);
        #endif
    }

    public void LoadSandboxScene()
    {
        SceneManager.LoadSceneAsync("Activity");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        #if UNITY_WEBGL || UNITY_EDITOR
            Screen.SetResolution(960, 600, false);
        #endif
    }

}
