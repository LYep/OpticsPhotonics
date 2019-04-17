using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleButton : MonoBehaviour
{
    public GameObject ExamplePanel;
    public GameObject TelescopePanel;
    public GameObject ActiveLens;
    public GameObject LightEmitter;
    public GameObject ConcaveLens, ConvexLens, ConcaveMirror, ConvexMirror;
    GameObject CurrentObj;
    Transform InitialPlayer;
    Transform RootScene;
    public Slider IndexLens;

    public void Start()
    {
        InitialPlayer = GameObject.Find("Player").transform;
        RootScene = GameObject.Find("Root").transform;
    }
    public void ExampleOnClick()
    {
        ExamplePanel.SetActive(true);
        ExamplePanel.GetComponent<Animator>().SetTrigger("PopIn");
    }
    public void ExampleExitOnClick()
    {
        ExamplePanel.GetComponent<Animator>().SetTrigger("PopOut");
        ExamplePanel.SetActive(false);
    }
    public void TelescopeOnClick()
    {
        TelescopePanel.SetActive(true);
        TelescopePanel.GetComponent<Animator>().SetTrigger("PopIn");
        ExampleExitOnClick();
    }
    public void TelescopeExitOnClick()
    {
        TelescopePanel.GetComponent<Animator>().SetTrigger("PopOut");
        TelescopePanel.SetActive(false);
        ExampleOnClick();
    }
    public void AstronomicalOnClick()
    {
        TelescopePanel.GetComponent<Animator>().SetTrigger("PopOut");
        TelescopePanel.SetActive(false);
        ResetScene();
        ActiveLens.GetComponent<ActivateLens>().LensCounter = 2;
        IndexLens.value = 0;

        CurrentObj = Instantiate(LightEmitter, new Vector3(0,0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition= new Vector3(-20, 7.5f, 0);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, 0, 0);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, -7.5f, 0);

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(-13.5f, 0, 0);
        Vector3 Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 30);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(32.05f, 0, 0);
        Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 30);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

    }
    public void FieldLens()
    {
        ExampleExitOnClick();
        ResetScene();
        ActiveLens.GetComponent<ActivateLens>().LensCounter = 3;
        IndexLens.value = 0;

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-21.65F, -10.75f, 0);
        CurrentObj.transform.eulerAngles = new Vector3(0, 0, 14);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
             Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-24.65F, -3.5F, 0);
        CurrentObj.transform.eulerAngles = new Vector3(0, 0, 14);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
           Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-26.2f, 3.98f, 0);
        CurrentObj.transform.eulerAngles = new Vector3(0, 0, 14);

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(-26, 0, 0);
        Vector3 Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 21);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(3.87f, 0, 0);
        Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 50);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(21.9f, 0, 0);
        Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 23);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;
    }
    public void GalOnClick()
    {
        TelescopePanel.GetComponent<Animator>().SetTrigger("PopOut");
        TelescopePanel.SetActive(false);
        ResetScene();
        ActiveLens.GetComponent<ActivateLens>().LensCounter = 2;
        IndexLens.value = 0;

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, 9.5f, 0);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, 0, 0);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, -9.5f, 0);

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(-13.5f, 0, 0);
        Vector3 Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 13);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

        CurrentObj = Instantiate(ConcaveLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 0)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(17.8f, 0, 0);
        Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 5.6f);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;
    }
    public void ZoomOnClick()
    {
        ExampleExitOnClick();
        ResetScene();
        ActiveLens.GetComponent<ActivateLens>().LensCounter = 3;
        IndexLens.value = 0;
        Vector3 startPos = new Vector3(-13.32f, 0, 0);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, -6, 0);

        CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
             Quaternion.identity, InitialPlayer.transform);
        CurrentObj.transform.localPosition = new Vector3(-20, 6, 0);

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(-25, 0, 0);
        Vector3 Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 16.38f);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

        CurrentObj = Instantiate(ConvexLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        CurrentObj.transform.localPosition = new Vector3(13.37f, 0, 0);
        Scale = CurrentObj.transform.localScale;
        CurrentObj.transform.localScale = new Vector3(Scale.x, Scale.y, 15.11f);
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;

        CurrentObj = Instantiate(ConcaveLens, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 0)), RootScene);
        CurrentObj.transform.localPosition = startPos;
        CurrentObj.GetComponent<Properties_Optical>().n_Index = 0;
        CurrentObj.AddComponent<ZoomLens>();        
    }
    void ResetScene()
    {
        foreach (GameObject lightObj in GameObject.FindGameObjectsWithTag("LightEmitter"))
        {
            Destroy(lightObj);
        }
        foreach (GameObject LensObj in GameObject.FindGameObjectsWithTag("OpticalElement"))
        {
            Destroy(LensObj);
        }
    }
}
