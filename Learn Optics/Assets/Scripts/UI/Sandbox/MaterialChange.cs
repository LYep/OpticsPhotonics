using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour {

    public Material[] materials;
    MeshRenderer rend;
    public GameObject lenButton;
    GameObject[] listOfObjs;
	// Use this for initialization
	void Start () {
        rend = GetComponent<MeshRenderer>();
        //rend.enabled = true;
        //rend.sharedMaterial = materials[0];
	}
	
    public void replace()
    {
        listOfObjs = GameObject.FindGameObjectsWithTag("OpticalElement");
        foreach (GameObject opticalEle in listOfObjs)
        {
            if(opticalEle.name=="ConcaveLens")
                opticalEle.GetComponent<MeshRenderer>().sharedMaterial = materials[2];
            else
                opticalEle.GetComponent<MeshRenderer>().sharedMaterial = materials[1];
        }
    }
}
