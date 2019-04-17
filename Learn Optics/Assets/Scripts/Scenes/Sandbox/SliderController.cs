using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{ 
    public float NIndex1;
    GameObject OpticalElement;
    Slider NIndexSlider;
    Slider RoCSlider;
    Slider NIndexSliderRay;
    Vector3 InitialScale;
    Vector3 size;
    float FixedRoC;
    GameObject[] OpticalElements;
    // Use this for initialization
    void Start()
    {
        NIndexSlider = GameObject.Find("NIndexSlider").GetComponent<Slider>();
        RoCSlider = GameObject.Find("RoCSlider").GetComponent<Slider>();
        NIndexSliderRay = GameObject.Find("NIndexSliderRay").GetComponent<Slider>();
        OpticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
        //InitialScale = OpticalElement.transform.localScale;
        //FixedRoC = OpticalElement.transform.localScale.z;
    }

    private void FixedUpdate()
    {
        //OpticalElement = GameObject.FindGameObjectWithTag("OpticalElement");                //There will only ever be one (Unless the MOE feature has been implemented)
        InitialScale = GameObject.Find("Content").GetComponent<SpawnElements>().InitialScale;
        // size = OpticalElement.transform.localScale;
       // print(FixedRoC + "accessed2");
    }

    /*public void onElementSpawn()
    {
        OpticalElement = GameObject.FindGameObjectsWithTag("OpticalElement")[0];
        print("check element" + OpticalElement);
        InitialScale = OpticalElement.transform.localScale;

        //transform.parent.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetComponent<Slider>().maxValue = 5;
        //transform.parent.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetComponent<Slider>().minValue = 1;
        if (OpticalElement.name.Contains("Concave") || OpticalElement.name.Contains("Mirror"))
        {
            RoCSlider.maxValue = 5;
            RoCSlider.minValue = 1;
            RoCSlider.wholeNumbers = false;
            RoCSlider.value = 3;

            HeightSlider.maxValue = 14;
            HeightSlider.minValue = 4;
            HeightSlider.wholeNumbers = false;
            HeightSlider.value = 10;
        }
    }*/
    public void onValueRoCChanged()
    {
        OpticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
        if (OpticalElements.Length == 1)
            OpticalElement = OpticalElements[0];
        else
        {
            foreach (GameObject obj in OpticalElements)
            {
                if (obj.GetComponent<Properties_Optical>().active)
                {
                    OpticalElement = obj;
                    break;
                }
            }
        }
        Vector3 size = OpticalElement.transform.localScale;
        if (OpticalElement.name == "ConvexLens" || OpticalElement.name == "ConvexLens(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(size.x, size.y, (11- RoCSlider.value)* 5);
        }

        if (OpticalElement.name == "ConcaveLens(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(size.x, size.y, (11- RoCSlider.value)*1.3F); 

        }

        if (OpticalElement.name == "ConcaveMirror(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(size.x, size.y, (11 - RoCSlider.value) * 5);

        }

        if (OpticalElement.name == "ConvexMirror(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(size.x, size.y, (11 - RoCSlider.value) * 5);

        }
        //FixedRoC = OpticalElement.transform.localScale.z;
        //print(FixedRoC + "accessed");
    }
   /* public void onValueChangedRoC()
    {
        //Works, however takes values of both sliders at the same time
        //float n_Index_slider_value = NIndexSlider.value;
        // print("val of Slider" + n_Index_slider_value);
        float RoC_Slider_value = RoCSlider.value;
        // print("val of Roc" + RoC_Slider_value);
        //print("check if still alive DOS: " + OpticalElement);

       // OpticalElement.GetComponent<Properties_Optical>().n_Index = n_Index_slider_value;
        OpticalElement.transform.localScale = new Vector3(InitialScale.x, InitialScale.y, RoC_Slider_value);
          print(OpticalElement.transform.localScale);
    }
    public void onValueChangedHeight()
    {
        float H_Slider_value = HeightSlider.value;
        if (OpticalElement.name.Contains("Concave"))
        {
            OpticalElement.transform.localScale = new Vector3(InitialScale.x, H_Slider_value, InitialScale.z);
        }
        else
            OpticalElement.transform.localScale = new Vector3(H_Slider_value, InitialScale.y, InitialScale.z);
    }*/

        //replace with second index of refraction
   public void onNIndexRayChanged()
    {
        //Vector3 RoC = OpticalElement.transform
        /*if (OpticalElement.name == "ConvexLens" || OpticalElement.name == "ConvexLens(Clone)")
        {
            RoCSlider.GetComponent<SliderController>().enabled = false;
            float RoC = (11 - RoCSlider.value) * 5;
            OpticalElement.transform.localScale = new Vector3(InitialScale.x + 8 * HeightSlider.value, size.y, RoC - (HeightSlider.value*15));
            //RoCSlider.value = (HeightSlider.value * 15) + 1;
        }

        if (OpticalElement.name == "ConcaveLens(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(size.x, InitialScale.y + 4 * HeightSlider.value, size.z);
        }

        if (OpticalElement.name == "ConcaveMirror(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(InitialScale.x + 4 * HeightSlider.value, size.y, size.z);

        }

        if (OpticalElement.name == "ConvexMirror(Clone)")
        {
            OpticalElement.transform.localScale = new Vector3(InitialScale.x + 4 * HeightSlider.value, size.y, size.z);

        }*/

    }

    public void onValueNIndexChanged()
    {
        //prob set N from 1 to 2.5
        //change N of air as well and account for it in light physics
        /*if (OpticalElement.name == "ConvexLens" || OpticalElement.name == "ConvexLens(Clone)")
        {
        }

        if (OpticalElement.name == "ConcaveLens(Clone)")
        {

        }

        if (OpticalElement.name == "ConcaveMirror(Clone)")
        {

        }

        if (OpticalElement.name == "ConvexMirror(Clone)")
        {

        }*/
        OpticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
        if (OpticalElements.Length == 1)
            OpticalElement = OpticalElements[0];
        else
        {
            foreach (GameObject obj in OpticalElements)
            {
                if (obj.GetComponent<Properties_Optical>().active)
                {
                    OpticalElement = obj;
                    break;
                }
            }
        }
        OpticalElement.GetComponent<Properties_Optical>().n_Index = NIndexSlider.value;
    }

}