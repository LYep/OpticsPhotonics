using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRefractionEdit : MonoBehaviour {
    public GameObject SlopeEquation;
    public GameObject NonMirrorButton;
    public GameObject MirrorButton;
    Animator SlopeAnimation;
    GameObject EditHeightButton;

    void Start () {
        SlopeAnimation = SlopeEquation.GetComponent<Animator>();
        EditHeightButton = transform.parent.GetChild(0).gameObject;
    }
	
    public void OnRefractionClick()
    {
        EditHeightButton.SetActive(false);
        NonMirrorButton.SetActive(true);
        MirrorButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
