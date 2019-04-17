using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMirrorEdit : MonoBehaviour {
    public GameObject SlopeEquation;
    public GameObject NonMirrorButton;
    public GameObject Divider;
    Animator SlopeAnimation;

    void Start () {
        SlopeAnimation = SlopeEquation.GetComponent<Animator>();
        gameObject.SetActive(false);
	}

    public void OnMirrorClick()
    {
        Divider.SetActive(false);
        NonMirrorButton.SetActive(false);
        gameObject.SetActive(false);
        SlopeAnimation.SetTrigger("SlideIn");
    }


}
