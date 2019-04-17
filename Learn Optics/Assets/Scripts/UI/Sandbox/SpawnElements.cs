using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElements : MonoBehaviour {
    public Material[] materials;
    public GameObject ConcaveLens, ConvexLens, ConcaveMirror, ConvexMirror;
    public Vector3 InitialScale;
    private GameObject[] OpticalElements;
    private GameObject Root;
    public float downTimer;
    GameObject clicker;
    public GameObject BG;
    GameObject activeLens;
    Transform RootScene;
    private void Start()
    {
        RootScene = GameObject.Find("Root").transform;
        activeLens = GameObject.Find("LensButton");
        /*Root = GameObject.Find("Root");
        OpticalElement = GameObject.FindGameObjectWithTag("OpticalElement");
        InitialScale = OpticalElement.transform.localScale;*/
    }

    //Whenever something is spawned, save its initial scale and height so the sliders can access it.
    public void SpawnConcaveLens()
    {
        BG.GetComponent<SpriteRenderer>().enabled = true;

        //Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(ConcaveLens, new Vector3(-9.5F, -18, 0), Quaternion.Euler(new Vector3(0, 90, 0)), RootScene);
        activeLens.GetComponent<ActivateLens>().LensCounter++;
        clicker = GameObject.FindGameObjectWithTag("MenuButton");
        clicker.GetComponent<MenuButtonClick>().toggleMenu();
    }
    public void SpawnConvexLens()
    {
        /* OpticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
         for (int i = 0; i < OpticalElements.Length; i++)
         {
             Destroy(OpticalElements[i]);
         }

         OpticalElement = Instantiate(ConvexLens, Root.transform.position, Quaternion.Euler(new Vector3(0, 90, 90)));
         InitialScale = OpticalElement.transform.localScale;
         print(InitialScale);*/

        BG.GetComponent<SpriteRenderer>().enabled = true;

       // Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(ConvexLens, new Vector3(-9.5F, -18, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        activeLens.GetComponent<ActivateLens>().LensCounter++;
        clicker = GameObject.FindGameObjectWithTag("MenuButton");
        clicker.GetComponent<MenuButtonClick>().toggleMenu();
    }
    public void SpawnConcaveMirror()
    {
        BG.GetComponent<SpriteRenderer>().enabled = true;

        //Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(ConcaveMirror, new Vector3(-9.5F, -18, 0), Quaternion.Euler(new Vector3(0, 90, 90)), RootScene);
        activeLens.GetComponent<ActivateLens>().LensCounter++;
        clicker = GameObject.FindGameObjectWithTag("MenuButton");
        clicker.GetComponent<MenuButtonClick>().toggleMenu();

    }
    public void SpawnConvexMirror()
    {
        BG.GetComponent<SpriteRenderer>().enabled = true;

        //Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(ConvexMirror, new Vector3(-9.5F, -18, 0), Quaternion.Euler(new Vector3(0, -90, 90)), RootScene);
        activeLens.GetComponent<ActivateLens>().LensCounter++;
        clicker = GameObject.FindGameObjectWithTag("MenuButton");
        clicker.GetComponent<MenuButtonClick>().toggleMenu();


    }
}
