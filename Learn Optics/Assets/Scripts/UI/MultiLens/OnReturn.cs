using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnReturn : MonoBehaviour {

    public GameObject Divider;
    public GameObject HeightEquation;
    public GameObject SlopeEquation;
    public GameObject SlopeMirrorEquation;
    Animator HeightAnimation;
    Animator SlopeAnimation;
    Animator SlopeMirrorAnimation;
    public GameObject EditHeightButton;
    public GameObject EditSlopeButton;

    void Start () {
        HeightAnimation = HeightEquation.GetComponent<Animator>();
        SlopeAnimation = SlopeEquation.GetComponent<Animator>();
        SlopeMirrorAnimation = SlopeMirrorEquation.GetComponent<Animator>();
        //EditHeightButton = transform.parent.GetChild(0).gameObject;
        //EditSlopeButton = transform.parent.GetChild(1).gameObject;
    }

    public void OnReturnClick()
    {
        if (gameObject.name == "ReturnHeight")
        {
            HeightAnimation.SetTrigger("PopOutTrig");
        }
        else if (gameObject.name == "ReturnMirrorSlope")
        {
            SlopeMirrorAnimation.SetTrigger("SlideOut");
        }
        else
            SlopeAnimation.SetTrigger("PopOutTrig");

        StartCoroutine(wait());              
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.35F);
        Divider.SetActive(true);
        EditHeightButton.SetActive(true);
        EditSlopeButton.SetActive(true);
    }
}
