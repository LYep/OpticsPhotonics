using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMainMenu : MonoBehaviour {
    private int counter;
    GameObject FirstPanel;
    GameObject SecondPanel;
    GameObject ThirdPanel;
    GameObject FourthPanel;
    public GameObject FifthPanel;

    public GameObject RayTraceSingle;
    public GameObject RayTraceMultiple;
    public GameObject RayTraceSandbox;
    public GameObject Feedback;
    public GameObject Portal;
    bool CheckTut;
    void Start () {
        SaveData.dataFile.Load();
        if (SaveData.dataFile.MainMenuTutorialActive)
        {
            gameObject.SetActive(false); //enable for release
            FifthPanel.SetActive(false);
            print(" also " + Application.persistentDataPath);
        }
        else
        {
            counter = 0;
            FirstPanel = transform.GetChild(0).gameObject;
            SecondPanel = transform.GetChild(1).gameObject;
            ThirdPanel = transform.GetChild(2).gameObject;
            FourthPanel = transform.GetChild(3).gameObject;
            FirstPanel.SetActive(true);
            SecondPanel.SetActive(false);
            ThirdPanel.SetActive(false);
            FourthPanel.SetActive(false);
            FifthPanel.SetActive(false);
        }
	}

    public void DisableOnStartUp(bool Check)
    {
        if (Check)
        {
            SaveData.dataFile.MainMenuTutorialActive = true;
            print(true);
        }
        else
        {
            SaveData.dataFile.MainMenuTutorialActive = false;
            print(false);
        }
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
                FirstPanel.SetActive(true);
                SecondPanel.SetActive(false);
                break;

            case 1:
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
                ThirdPanel.SetActive(false);
                FourthPanel.SetActive(true);
                FifthPanel.SetActive(false);
                break;
            case 4:
                FourthPanel.SetActive(false);
                FifthPanel.SetActive(true);
                break;
        }
    }
    public void ExitTutorial()
    {
        SaveData.dataFile.Save();
        gameObject.GetComponent<Animator>().SetTrigger("ScaleDown");
        FifthPanel.GetComponent<Animator>().SetTrigger("ScaleDown");
        RayTraceSingle.GetComponent<Button>().enabled = true;
        RayTraceMultiple.GetComponent<Button>().enabled = true;
        RayTraceSandbox.GetComponent<Button>().enabled = true;
    }
    public void OpenTutorial()
    {
        counter = 0;
        GetComponent<Animator>().Play("Idle");
        FifthPanel.GetComponent<Animator>().Play("Idle");
        FirstPanel = transform.GetChild(0).gameObject;
        SecondPanel = transform.GetChild(1).gameObject;
        ThirdPanel = transform.GetChild(2).gameObject;
        FourthPanel = transform.GetChild(3).gameObject;
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
        FourthPanel.SetActive(false);
        FifthPanel.SetActive(false);
        RayTraceSingle.GetComponent<Button>().enabled = false;
        RayTraceMultiple.GetComponent<Button>().enabled = false;
        RayTraceSandbox.GetComponent<Button>().enabled = false;
    }
}
