using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivateLens : MonoBehaviour
{
    public GameObject LightEmitter;
    private GameObject InitialPlayer;
    public bool active;
    public int LensCounter;
    public Material[] materials;
    GameObject[] listOfObjs;
    GameObject[] listOfRays;
    public GameObject BG;

    private void Start()
    {
        InitialPlayer = GameObject.Find("Player");
        active = false;
        LensCounter = 1;
    }

    public void switchMode()
    {
        if (active)
            controlRay();
        else
            controlLens();
    }
    public void controlRay()
    {
        BG.GetComponent<SpriteRenderer>().enabled = false;
        active = false;
        listOfObjs = GameObject.FindGameObjectsWithTag("OpticalElement");
        listOfRays = GameObject.FindGameObjectsWithTag("LightEmitter");

        foreach (GameObject lightEm in listOfRays)
        {
            //#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            #if UNITY_STANDALONE || UNITY_WEBGL||UNITY_EDITOR
                lightEm.GetComponent<MovementPC>().enabled = true;
            #endif
            /*if (lightEm.GetComponent<MovementPC>() != null)
                lightEm.GetComponent<MovementPC>().enabled = true;
            else
                lightEm.GetComponent<MovementMobile>().enabled = true;*/
        }
        foreach (GameObject opticalEle in listOfObjs)
        {
            print("sfs");
            opticalEle.GetComponent<MeshRenderer>().sharedMaterial = materials[0];

            #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            opticalEle.GetComponent<MovementLens>().active = false;
            opticalEle.GetComponent<MovementLens>().enabled = false;
            #endif

           /* if (opticalEle.GetComponent<Properties_Optical>().platformPC)
            {
                opticalEle.GetComponent<MovementLens>().active = false;
                opticalEle.GetComponent<MovementLens>().enabled = false;
            }
            else
                opticalEle.GetComponent<MovementLensMobile>().enabled = false;*/
        }
    }
    public void controlLens()
    {
        BG.GetComponent<SpriteRenderer>().enabled = true;
        active = true;
        listOfObjs = GameObject.FindGameObjectsWithTag("OpticalElement");
        listOfRays = GameObject.FindGameObjectsWithTag("LightEmitter");
        foreach (GameObject opticalEle in listOfObjs)
        {
            if (opticalEle.name == "ConcaveLens" ||opticalEle.name=="ConcaveLens(clone)")
                opticalEle.GetComponent<MeshRenderer>().sharedMaterial = materials[2];
            else
                opticalEle.GetComponent<MeshRenderer>().sharedMaterial = materials[1];

            #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            opticalEle.GetComponent<MovementLens>().active = true;
            opticalEle.GetComponent<MovementLens>().enabled = true;
            #endif
            /*
            opticalEle.GetComponent<MovementLens>().active = true;
            if (opticalEle.GetComponent<MovementLens>() != null)
                opticalEle.GetComponent<MovementLens>().enabled = true;
            else
                opticalEle.GetComponent<MovementLensMobile>().enabled = true;*/
        }
        foreach(GameObject lightEm in listOfRays)
        {
            #if UNITY_STANDALONE || UNITY_WEBGL ||UNITY_EDITOR
                lightEm.GetComponent<MovementPC>().enabled = false;
            #endif
            /*if (lightEm.GetComponent<MovementPC>() != null)
                lightEm.GetComponent<MovementPC>().enabled = false;
            else
                lightEm.GetComponent<MovementMobile>().enabled = false;*/
        }
    }


}
