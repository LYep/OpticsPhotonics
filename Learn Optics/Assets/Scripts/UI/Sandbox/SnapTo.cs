using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTo : MonoBehaviour {
    bool activeScene;
    ActivateLens checkButton;
    GameObject[] ObjectsInScene;
    float Num1;

	void Start () {
        checkButton = GameObject.FindGameObjectWithTag("ActiveLens").GetComponent<ActivateLens>();
        activeScene = checkButton.active;
        Num1 = -22.29F; //use two different numbers because light physics has slight discrepancy for objects > 1 on concavelens
	}

    public void SnapLens()
    {
        activeScene = checkButton.active;
        if (activeScene)
        {
            ObjectsInScene = GameObject.FindGameObjectsWithTag("OpticalElement");
            if (ObjectsInScene.Length == 1)
            {
                Vector3 pos = ObjectsInScene[0].transform.localPosition;
                ObjectsInScene[0].transform.localPosition = new Vector3(pos.x, 0, pos.z);
            }
            else
            {
                foreach (GameObject objEle in ObjectsInScene)
                {
                    if (objEle.GetComponent<Properties_Optical>().active)
                    {
                        Vector3 pos = objEle.transform.localPosition;
                        if (objEle.name == "ConcaveLens(Clone)")
                        {
                            objEle.transform.localPosition = new Vector3(pos.x, 0, pos.z);
                        }
                        else 
                            objEle.transform.localPosition = new Vector3(pos.x, 0, pos.z);
                        break;
                    }
                }
            }
        }
        else
        {
            ObjectsInScene = GameObject.FindGameObjectsWithTag("LightEmitter");
            if (ObjectsInScene.Length == 1)
            {
                Vector3 pos = ObjectsInScene[0].transform.localPosition;
                ObjectsInScene[0].transform.localPosition = new Vector3(pos.x, 0, pos.z);
            }
            else
            {
                foreach (GameObject RayObj in ObjectsInScene)
                {
                    if (RayObj.GetComponent<Player_Controls>().isChoosen)
                    {
                        Vector3 pos = RayObj.transform.localPosition;
                        RayObj.transform.localPosition = new Vector3(pos.x, 0, pos.z);
                    }
                }
            }
        }
    }
	
}
