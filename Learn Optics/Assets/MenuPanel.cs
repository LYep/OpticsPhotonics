using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {
    public GameObject LoadSavePanel;

    void Start () {
     
	}
	
    public void OpenPanelOnClick()
    {
        gameObject.GetComponent<Animator>().SetTrigger("PopIn");
    }
    public void ClosePanelOnClick()
    {
        gameObject.GetComponent<Animator>().SetTrigger("PopOut");
    }
    public void LoadSandboxScene()
    {
        SceneManager.LoadSceneAsync("Activity");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    public void OnClickToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LoadPanel()
    {
        ClosePanelOnClick();
        LoadSavePanel.transform.GetChild(1).GetComponent<Text>().text = "Select a Scene to load";
        LoadSavePanel.GetComponent<SceneState>().SavingScene = false;
        LoadSavePanel.GetComponent<Animator>().SetTrigger("PopIn");

    }

    public void SavePanel()
    {
        ClosePanelOnClick();
        LoadSavePanel.transform.GetChild(1).GetComponent<Text>().text = "Select a slot to save in";
        LoadSavePanel.GetComponent<SceneState>().SavingScene = true;
        LoadSavePanel.GetComponent<Animator>().SetTrigger("PopIn");
    }
}
