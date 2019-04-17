using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEquationEdit : MonoBehaviour {

    public GameObject Divider;
    public GameObject HeightEquation;
    public GameObject SlopeEquation;
    public GameObject PromptButton;
    public GameObject MirrorEquation;
    public GameObject OtherEquation;
    public bool Demo;
    Animator HeightAnimation;
    Animator SlopeAnimation;
    GameObject EditHeightButton;
    GameObject EditSlopeButton;
    GameObject PromptPanel; 
    
	void Start () {
        PromptPanel = GameObject.FindGameObjectWithTag("PromptPanel");
        HeightAnimation = HeightEquation.GetComponent<Animator>();
        SlopeAnimation = SlopeEquation.GetComponent<Animator>();
        EditHeightButton = transform.parent.GetChild(0).gameObject;
        EditSlopeButton = transform.parent.GetChild(1).gameObject;
        Demo = false;
	}

    public void OnEditClick()
    {
        Divider.SetActive(false);
        EditHeightButton.SetActive(false);
        EditSlopeButton.SetActive(false);
        if (gameObject.name == "SelectTransfer")
        {
            HeightAnimation.SetTrigger("PopInTrig");
        }
        else
        {
            SlopeAnimation.SetTrigger("PopInTrig");
        }
        if (Demo)
        {
           PromptButton.GetComponent<Button>().onClick.Invoke();
            Demo = false;
        }
    }
}
