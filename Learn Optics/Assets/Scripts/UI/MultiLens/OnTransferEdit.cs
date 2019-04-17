using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTransferEdit : MonoBehaviour {
    Animator HeightAnimation;
    public GameObject Divider;
    public GameObject HeightEquation;
    public GameObject PromptButton;
    public bool Demo;
    GameObject EditSlopeButton;
    void Start () {
        HeightAnimation = HeightEquation.GetComponent<Animator>();
        EditSlopeButton = transform.parent.GetChild(1).gameObject;
    }
	public void OnTransferClick()
    {
        Divider.SetActive(false);        
        EditSlopeButton.SetActive(false);
        HeightAnimation.SetTrigger("PopInTrig");
        if (Demo)
        {
            PromptButton.GetComponent<Button>().onClick.Invoke();
            Demo = false;
        }
        gameObject.SetActive(false);
    }
}
