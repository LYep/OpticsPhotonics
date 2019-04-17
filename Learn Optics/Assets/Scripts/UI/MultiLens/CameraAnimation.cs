using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAnimation : MonoBehaviour {

    public GameObject OverlaySnell;
    public GameObject CrossOutObj;
    GameObject SnellEquations;
    GameObject SnellApprox;
    GameObject ParameterPanel;
    GameObject TransferFill;
    GameObject RefractionFill;
    ScrollRect ScrollBar;
    public GameObject OverlayHeight;
    public GameObject OverlayRefract;
    public GameObject SurfacePanel;
    public GameObject OverlayExit;
    public GameObject OverlayDemo;
    public GameObject OverlayDemoRefract;
    public GameObject OverlayRadius;
    public GameObject OverlayRadiusConcave;
    public GameObject OverlayRadiusMirror;
    bool done;

    private void Start()
    {
        ParameterPanel = GameObject.Find("Parameter Menu");
        ScrollBar = ParameterPanel.transform.GetChild(1).gameObject.GetComponent<ScrollRect>();
        TransferFill = ParameterPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
        RefractionFill = ParameterPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject;
        SnellEquations = OverlaySnell.transform.GetChild(8).gameObject;
        SnellApprox = OverlaySnell.transform.GetChild(9).gameObject;
        // OverlayHeight = GameObject.Find("OverlayEquationsHeight");
        //OverlayHeight.SetActive(false);
        //OverlayRefract.SetActive(false);
        //SurfacePanel = GameObject.Find("SurfacePowerPanel");
        done = false;
    }
    public void StartOverlay()
    {
        OverlaySnell.SetActive(true);
    }
    public void SnellApproxSwitch()
    {
        SnellEquations.SetActive(false);
        CrossOutObj.SetActive(false);
        SnellApprox.SetActive(true);
    }
    public void ClearOverlaySnell()
    {
        OverlaySnell.SetActive(false);
    }
    public void StartOverlayHeight()
    {
        if (!done)
        {
            OverlayHeight.SetActive(true);
            done = true;
        }
    }
    public void ClearOverlayHeight()
    {
        OverlayHeight.SetActive(false);
    }
    public void StartOverlayRefract()
    {
        OverlayRefract.SetActive(true);
        OverlayHeight.SetActive(false);
        SurfacePanel.SetActive(true);
        SurfacePanel.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
        SurfacePanel.GetComponent<Animator>().SetTrigger("SlideUp");
        SurfacePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Bounce");
        SurfacePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("KeepBounce", true);
    }

    public void ClearOverlayRefract()
    {
        OverlayRefract.SetActive(false);        
    }
    public void StartOverlayRadius()
    {
        OverlayRadius.SetActive(true);
    }
    public void ClearOverlayRadius()
    {
        OverlayRadius.SetActive(false);
        SurfacePanel.SetActive(false);
    }
    public void StartOverlayRadiusConcave()
    {
        OverlayRadiusConcave.SetActive(true);
    }
    public void ClearOverlayRadiusConcave()
    {
        OverlayRadiusConcave.SetActive(false);
    }
    public void StartOverlayRadiusMirror()
    {
        OverlayRadiusMirror.SetActive(true);
    }
    public void ClearOverlayRadiusMirror()
    {
        OverlayRadiusMirror.SetActive(false);
    }
    public void StartOverlayExit()
    {
        OverlayRefract.SetActive(false);
        OverlayExit.SetActive(true);
    }
    public void ClearOverlayExit()
    {
        OverlayExit.SetActive(false);
    }
    //This is a catch if scene gets clicked through quickly
    public void ClearRadiusOverlays()
    {
        OverlayRadiusConcave.SetActive(false);
        OverlayRadius.SetActive(false);
    }
    public void StartOverlayDemo()
    {
        SurfacePanel.SetActive(false);
        OverlayDemo.SetActive(true);
        OverlayDemo.transform.GetChild(3).gameObject.SetActive(false);
        TransferFill.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "50mm"; //distance
        TransferFill.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        TransferFill.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "0.197"; //Incident Angle
        TransferFill.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        TransferFill.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "1.0"; // Index Ni
        TransferFill.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        TransferFill.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "5mm"; //Incident Height
        TransferFill.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

        RefractionFill.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "1.0"; //Index Ni
        RefractionFill.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        RefractionFill.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "0.197"; //Incident Angle
        RefractionFill.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        RefractionFill.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "73mm"; //Radius
        RefractionFill.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        RefractionFill.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().text = "1.5";
        RefractionFill.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
    }
    public void ClearOverlayDemo()
    {
        OverlayDemo.SetActive(false);
    }
    public void StartOverlayDemoRefract()
    {
        OverlayDemo.SetActive(false);
        OverlayDemoRefract.SetActive(true);
        RefractionFill.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "14.85mm"; //Refracted Height
        RefractionFill.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
        ScrollBar.verticalNormalizedPosition = 0;
    }
    public void EndOfAnimation()
    {
        
    }
}
