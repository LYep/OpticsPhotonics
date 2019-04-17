using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonClick : MonoBehaviour
{
    GameObject TutorialButton, ExampleButton;
    Animator animator_OpticalMenuPanel;
    Animator animator_ParameterMenuPanel;
    Animator animator_button;

    void Start()
    {
        TutorialButton = GameObject.Find("TutorialButton");
        ExampleButton = GameObject.Find("ExampleButton");
        animator_button = GetComponent<Animator>();           //Get animator of button
        animator_OpticalMenuPanel = transform.parent.GetChild(1).gameObject.GetComponent<Animator>();  //Get animator of Optical Menu Panel
        animator_ParameterMenuPanel = transform.parent.GetChild(2).gameObject.GetComponent<Animator>();  //Get animator of Parmeter Menu Panel
        animator_button.SetBool("toggleMenu", false);
        animator_OpticalMenuPanel.SetBool("toggleMenu", false);
        animator_ParameterMenuPanel.SetBool("toggleMenu", false);
    }
    public void toggleMenu()
    {
        if (TutorialButton.activeSelf)
            TutorialButton.SetActive(false);
        else
            TutorialButton.SetActive(true);

        if (ExampleButton.activeSelf)
            ExampleButton.SetActive(false);
        else
            ExampleButton.SetActive(true);
        animator_button.SetBool("toggleMenu", !animator_button.GetBool("toggleMenu"));
        animator_OpticalMenuPanel.SetBool("toggleMenu", !animator_OpticalMenuPanel.GetBool("toggleMenu"));
        animator_ParameterMenuPanel.SetBool("toggleMenu", !animator_ParameterMenuPanel.GetBool("toggleMenu"));

    }
}