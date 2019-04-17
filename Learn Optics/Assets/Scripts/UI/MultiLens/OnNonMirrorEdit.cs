using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNonMirrorEdit : MonoBehaviour {
    public GameObject SlopeEquation;
    public GameObject MirrorButton;
    public GameObject Divider;
    Animator SlopeAnimation;

    void Start () {
        SlopeAnimation = SlopeEquation.GetComponent<Animator>();
        gameObject.SetActive(false);
	}
	
    public void OnNonMirrorClick()
    {
        Divider.SetActive(false);
        MirrorButton.SetActive(false);
        gameObject.SetActive(false);
        SlopeAnimation.SetTrigger("PopInTrig");
    }

}
