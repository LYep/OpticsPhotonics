using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BGClick : MonoBehaviour, IPointerClickHandler {

    public ActivateLens sceneReset;
	// Use this for initialization
	void Start () {
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        print("access");
        sceneReset.GetComponent<ActivateLens>().controlRay();
    }
}
