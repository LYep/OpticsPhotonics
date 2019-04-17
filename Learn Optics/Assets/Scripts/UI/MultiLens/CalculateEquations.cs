using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateEquations: MonoBehaviour {

    decimal InitialHeight;
    decimal Distance;
    decimal InitialSlope;
    decimal InitialIndex;
    decimal ResultHeight;

    decimal InitialHeightRefract;
    decimal InitialIndexRefract;
    decimal InitialSlopeRefract;
    decimal SecondIndexRefract;
    decimal Radius;
    decimal ResultSlope;

    decimal InitialHeightMirrorRefract;
    decimal InitialSlopeMirrorRefract;
    decimal RadiusMirror;
    decimal ResultMirrorSlope;

    public InputField InitialHeightField;
    public InputField DistanceField;
    public InputField InitialIndexField;
    public InputField InitialSlopeField;
    public InputField InitialIndexField2;
    public InputField ResultHeightField;

    public InputField InitialIndexRefractField;
    public InputField InitialIndexRefractField2;
    public InputField InitialHeightRefractField;
    public InputField InitialSlopeRefractField;
    public InputField RadiusField;
    public InputField SecondIndexRefractField;
    public InputField SecondIndexRefractField2;
    public InputField ResultSlopeField;

    public InputField InitialHeightRefractMirrorField;
    public InputField InitialSlopeRefractMirrorField;
    public InputField RadiusMirrorField;
    public InputField ResultSlopeMirrorField;

    public GameObject SurfacePowerPanel;
    public GameObject SurfacePowerMirrorPanel;
    GameObject PromptPanel;
    bool ActivePanel;
    bool ActiveMirrorPanel;
    public bool DemoHeight;
    public bool DemoRefract;
    //Height Equation
    bool InitialHeightCheck;
    bool DistanceCheck;
    bool InitialSlopeCheck;
    bool InitialIndexCheck;
    //Slope Equation
    bool InitialHeightRefractCheck;  
    bool InitialSlopeRefractCheck;
    bool InitialIndexRefractCheck;
    bool SecondIndexRefractCheck;
    bool RadiusCheck;
    //Mirror Slope Equation
    bool InitialHeightRefractMirrorCheck;
    bool InitialSlopeRefractMirrorCheck;
    bool RadiusMirrorCheck;
    // Use this for initialization
    void Start () {
        PromptPanel = GameObject.Find("PromptPanel");
        SurfacePowerPanel.SetActive(false);
        SurfacePowerMirrorPanel.SetActive(false);
        ActivePanel = false;
        ActiveMirrorPanel = false;
        DemoHeight = false;
        DemoRefract = false;
        InitializeInputFields();
    }
	
    public void OnClickCalculateHeight()
    {
        InitialHeightCheck = decimal.TryParse(InitialHeightField.text, out InitialHeight);
        DistanceCheck = decimal.TryParse(DistanceField.text, out Distance);
        InitialSlopeCheck = decimal.TryParse(InitialSlopeField.text, out InitialSlope);
        InitialIndexCheck = decimal.TryParse(InitialIndexField.text, out InitialIndex);

        if(Distance == 0 || InitialIndex == 0)
        {
            //Error effect animation
        }
        else
        {
            decimal ResultHeight2 = (InitialHeight + (Distance * InitialSlope));
            ResultHeight = decimal.Round((InitialHeight + (Distance * InitialSlope)), 2, System.MidpointRounding.AwayFromZero);
            print(ResultHeight + "result from calc");
            print(ResultHeight2 + "result from calc2");
        }
        ResultHeightField.text = ResultHeight.ToString("0.00");
        if (DemoHeight)
        {
            bool CheckNum = Mathf.Abs(((float)ResultHeight - 14.85f)) < 0.01f;
            if (CheckNum)
            {
                PromptPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
                DemoHeight = false;
            }
            else
                PromptPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Check your calculations!";
        }
    }

    public void OnClickCalculateSlope()
    {
        InitialHeightRefractCheck = decimal.TryParse(InitialHeightRefractField.text, out InitialHeightRefract);
        InitialIndexRefractCheck = decimal.TryParse(InitialIndexRefractField.text, out InitialIndexRefract);
        InitialSlopeRefractCheck = decimal.TryParse(InitialSlopeRefractField.text, out InitialSlopeRefract);
        SecondIndexRefractCheck = decimal.TryParse(SecondIndexRefractField.text, out SecondIndexRefract);
        RadiusCheck = decimal.TryParse(RadiusField.text, out Radius);

        print(InitialHeightRefract + " H2");
        print(InitialIndexRefract + " n1");
        print(InitialSlopeRefract + " u1");
        print(SecondIndexRefract + " n2");
        print(Radius + " R");
        if (InitialIndexRefract == 0 || Radius == 0 || SecondIndexRefract == 0)
        {
            //Error effect animation
        }
        else
        {
            decimal ResultSlope2 = (((InitialIndexRefract * InitialSlopeRefract) - InitialHeightRefract * ((SecondIndexRefract - InitialIndexRefract) / Radius)) / SecondIndexRefract);
            ResultSlope = decimal.Round((((InitialIndexRefract*InitialSlopeRefract) - InitialHeightRefract*((SecondIndexRefract - InitialIndexRefract)/Radius))/SecondIndexRefract), 3, System.MidpointRounding.AwayFromZero);
            print(ResultSlope + "result from calc");
            print(ResultSlope2 + "result from calc2");
        }
        ResultSlopeField.text = ResultSlope.ToString("0.000");
        if (DemoRefract)
        {
            bool CheckNum = Mathf.Abs(((float)ResultSlope - 0.0635f)) < 0.001f;
            if (CheckNum)
            {
                PromptPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
                DemoRefract = false;
            }
            else
                PromptPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Check your calculations!";
        }
    }
    public void OnClickCalculateMirrorSlope()
    {
        InitialHeightRefractMirrorCheck = decimal.TryParse(InitialHeightRefractMirrorField.text, out InitialHeightMirrorRefract);
        InitialSlopeRefractMirrorCheck = decimal.TryParse(InitialSlopeRefractMirrorField.text, out InitialSlopeMirrorRefract);
        RadiusMirrorCheck = decimal.TryParse(RadiusMirrorField.text, out RadiusMirror);

        print(InitialHeightMirrorRefract + " H2");
        print(InitialSlopeMirrorRefract + " u1");
        print(RadiusMirror + " R");
        if (RadiusMirror == 0)
        {
            //Error effect animation
        }
        else
        {
            decimal ResultSlope3 = ((InitialSlopeMirrorRefract) - InitialHeightMirrorRefract * ((-2/ RadiusMirror)));
            ResultMirrorSlope = decimal.Round(((InitialSlopeMirrorRefract) - InitialHeightMirrorRefract * (-2 / RadiusMirror)), 3, System.MidpointRounding.AwayFromZero);
            print(ResultMirrorSlope + "result from calc");
            print(ResultSlope3 + "result from calc2");
        }
        ResultSlopeMirrorField.text = ResultMirrorSlope.ToString("0.000");
        /*if (DemoRefract)
        {
            bool CheckNum = Mathf.Abs(((float)ResultMirrorSlope - 0.0635f)) < 0.001f;
            if (CheckNum)
            {
                PromptPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
                DemoRefract = false;
            }
            else
                PromptPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Check your calculations!";
        }*/
    }
    public void ActiveSurfacePanel()
    {
        if (ActivePanel)
        {
            SurfacePowerPanel.SetActive(false);
            ActivePanel = false;
        }
        else
        {
            SurfacePowerPanel.SetActive(true);
            ActivePanel = true;
        }
    }
    public void ActiveSurfaceMirrorPanel()
    {
        if (ActiveMirrorPanel)
        {
            SurfacePowerMirrorPanel.SetActive(false);
            ActiveMirrorPanel = false;
        }
        else
        {
            SurfacePowerMirrorPanel.SetActive(true);
            ActiveMirrorPanel = true;
        }
    }

    //This is so that the index fields update each other
    void InitializeInputFields()
    {
        InitialIndexField.onValueChanged.AddListener(delegate { OnEditChangeIndex(InitialIndexField.name); });
        InitialIndexField2.onValueChanged.AddListener(delegate { OnEditChangeIndex(InitialIndexField2.name); });

        InitialIndexRefractField.onValueChanged.AddListener(delegate { OnEditChangeIndex(InitialIndexRefractField.name); });
        InitialIndexRefractField2.onValueChanged.AddListener(delegate { OnEditChangeIndex(InitialIndexRefractField2.name); });

        SecondIndexRefractField.onValueChanged.AddListener(delegate { OnEditChangeIndex(SecondIndexRefractField.name); });
        SecondIndexRefractField2.onValueChanged.AddListener(delegate { OnEditChangeIndex(SecondIndexRefractField2.name); });
    }

    public void OnEditChangeIndex(string ObjName)
    {
        if (ObjName == "IndexFirst")
            InitialIndexField2.text = InitialIndexField.text;
        else if (ObjName == "IndexFirst2")
            InitialIndexField.text = InitialIndexField2.text;
        else if (ObjName == "FirstIndexSlope")
            InitialIndexRefractField2.text = InitialIndexRefractField.text;
        else if (ObjName == "IndexFirstSurface")
            InitialIndexRefractField.text = InitialIndexRefractField2.text;
        else if (ObjName == "SecondIndexSlope")
            SecondIndexRefractField2.text = SecondIndexRefractField.text;
        else
            SecondIndexRefractField.text = SecondIndexRefractField2.text;
    }
    public void ClearFieldsSlope()
    {
        InitialHeightRefractField.text = "";
        InitialIndexRefractField.text = "";
        InitialSlopeRefractField.text = "";
        SecondIndexRefractField.text = "";
        RadiusField.text = "";
        ResultSlopeField.text = "";
    }
    public void ClearFieldsMirrorSlope()
    {
        InitialHeightRefractMirrorField.text = "";
        InitialSlopeRefractMirrorField.text = "";
        RadiusMirrorField.text = "";
        ResultSlopeMirrorField.text = "";
    }
    public void ClearFieldsHeight()
    {
        InitialHeightField.text = "";
        DistanceField.text = "";
        InitialSlopeField.text = "";
        InitialIndexField.text = "";
        ResultHeightField.text = "";
    }
}
