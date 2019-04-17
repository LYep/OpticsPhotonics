using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeRayNumber : MonoBehaviour
{

    public GameObject LightEmitter;
    private GameObject InitialPlayer;

    System.Random numRand = new System.Random();

    private void Start()
    {
        InitialPlayer = GameObject.Find("Player");
    }

    public void AddRays()
    {
        Instantiate(LightEmitter, new Vector3(numRand.Next(-69, -43), numRand.Next(-35, -10)), 
            Quaternion.identity, InitialPlayer.transform);

    }

    public void RemoveRays()
    {
        foreach (GameObject lightObj in GameObject.FindGameObjectsWithTag("LightEmitter"))
        {
            if (lightObj.GetComponent<Player_Controls>().isChoosen == true)
            {
                Destroy(lightObj);
            }
        }
    }
}
