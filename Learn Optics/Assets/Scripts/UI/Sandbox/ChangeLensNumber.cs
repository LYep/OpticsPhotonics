using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLensNumber : MonoBehaviour {

    GameObject[] OpticalElements;
    MenuButtonClick clicker;
    GameObject ActiveLens;
    public int LensCounter;
    void Start () {
        clicker = GameObject.FindGameObjectWithTag("MenuButton").GetComponent<MenuButtonClick>();
        ActiveLens = GameObject.Find("LensButton");
	}
	
    public void AddLens()
    {
        clicker.toggleMenu();
    }

    public void RemoveLens()
    {
        OpticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
        foreach(GameObject objEle in OpticalElements)
        {
            if (objEle.GetComponent<Properties_Optical>().active)
            {
                Destroy(objEle);
                ActiveLens.GetComponent<ActivateLens>().LensCounter--;
            }
        }
    }
}
