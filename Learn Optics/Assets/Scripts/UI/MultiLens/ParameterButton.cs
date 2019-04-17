using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterButton : MonoBehaviour
{

    Animator animator_OpticalMenuPanel;
    Animator animator_ParameterMenuPanel;
    Animator animator_button;

    void Start()
    {
        animator_button = GetComponent<Animator>();           //Get animator of button
        animator_ParameterMenuPanel = transform.root.Find("Parameter Menu").gameObject.GetComponent<Animator>(); //Get animator of Parmeter Menu Panel
        animator_button.SetBool("toggleMenu", false);
        animator_ParameterMenuPanel.SetBool("toggleMenu", false);
    }
    public void toggleMenu()
    {
        animator_button.SetBool("toggleMenu", !animator_button.GetBool("toggleMenu"));
        animator_ParameterMenuPanel.SetBool("toggleMenu", !animator_ParameterMenuPanel.GetBool("toggleMenu"));

    }
}