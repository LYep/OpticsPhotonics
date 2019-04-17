using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhiButtonClick : MonoBehaviour {

	
	void Start () {
		
	}
    public void PhiButtonOnClick()
    {
        transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("KeepBounce", false);
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("SlideUp");
    }
}
