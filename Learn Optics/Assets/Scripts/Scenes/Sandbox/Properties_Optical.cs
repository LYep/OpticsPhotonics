using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties_Optical : MonoBehaviour
{
    public float focal_length = 5;
    public float n_Index = 1.52F;
    //Radii of curvature. 99% of the time, they'll be the same. I just use them as separate variables since the lensmaker equation dictates it.
    public float R_Curve_1 = 25;
    public float R_Curve_2 = 25;
    public float damping_factor;
    //Is this object a mirror? Default is false
    public bool isReflective = false;
    public bool isCurved = true;
    public bool isConcaveReflective;
    public bool isConvexReflective;
    public bool isConcave;
    //Distance moved on keypress
    private float movementDistance;
    public static int gottem;
    public bool platformPC;
    public bool active;

    // Use this for initialization
    void Start()
    {
        damping_factor = Vector3.Magnitude(transform.localScale) / 10;
        movementDistance = 0.3F;
        gottem = 10;
        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            platformPC = true;
        #else
            platformPC = false;
            active = false;
        #endif
        print(platformPC + "if pc");
    }
    public void setNIndex (float n_Index)
    {
        this.n_Index = n_Index;
    }

}
