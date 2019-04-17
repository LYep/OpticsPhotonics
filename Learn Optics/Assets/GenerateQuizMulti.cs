using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.AnimatedLineRenderer
{
    public class GenerateQuizMulti : MonoBehaviour
    {
        public GameObject ConcaveFab;
        public GameObject ConvexFab;
        public GameObject Mirror;
        public GameObject ProgammableALR;

        public GameObject QuizPanel;
        public GameObject HintPrompt;
        public GameObject HintButton;
        public GameObject ObjectArrow;

        public GameObject DistanceField;
        public GameObject IncidentAngleField;
        public GameObject IncidentHeightField;
        public GameObject IndexNField;

        public GameObject AnswerHeightRefractField;
        public GameObject IncidentAngleRefractField;
        public GameObject IncidentIndexRefractField;
        public GameObject RadiusField;
        public GameObject SecondIndexField;
        public GameObject SecondIndexField2;
        public GameObject AnswerSlopeRefractField;

        Vector3 FirstElementPosition;
        Vector3 FirstRayPosition;
        Vector3 MostRecentRayPos;
        List<GameObject> ListOfLenses;
        List<GameObject> ListOfRays;
        InputField AnswerField;
        GameObject[] ListLens;
        GameObject IncidentRay;
        GameObject FirstLens;
        GameObject PromptPanel;
        GameObject Root;
        GameObject ParameterMenu;        
        GameObject EquationPanel;
        GameObject TransferEquationParameters;
        GameObject RefractionEquationParameters;
        GameObject MainCamera;
        Camera MainCameraMove;
        Animator HintPromptAnimator;
        Animator ParameterButton;
        Text QuizPanelText;
        Text HintPromptText;
        RaycastHit RayHit;
        
        System.Random numRand;

        bool AngleRayHit;
        bool isUICoroutineExecuting;
        //bool isHeightQuestion;
        bool isAnswerCorrect;
        public decimal InitialAngleOfRay;
        public decimal Radius;
        public int CounterSurface;
        public decimal DistanceOfContact;
        public decimal InitialHeight;
        public decimal TransferHeight;
        public decimal IndexAir;
        public decimal IndexMaterial;
        public decimal RoundAngle;

        public decimal TrueAnswerSlope;
        public decimal TrueAnswerHeight;

        decimal AnswerUser;
        bool AnswerUserCheck;
        Vector3 AngleOfRayConv;
        bool EndQuizCheck;
        public string Cases;
        float TransitionTime = 1f;
        Vector3 CameraNewPos;
        Vector3 CameraStartPos;
        float ZoomA;
        float ZoomB;
        float timeWait;
        bool DoneLoad;
        int CurrentLens;
        void Start()
        {
            CounterSurface = 0;
            numRand = new System.Random();
            FirstElementPosition = new Vector3(-175.8f, -20, 0); //Pos of 1st lens at root always
            Root = GameObject.Find("Root");
            ListLens = new GameObject[3];
            ListOfLenses = new List<GameObject>();
            PromptPanel = GameObject.Find("PromptPanel");
            AngleRayHit = false;
            EndQuizCheck = false;
            DoneLoad = false;
            ParameterMenu = GameObject.Find("Parameter Menu");
            ParameterButton = GameObject.Find("MenuButton").GetComponent<Animator>();
            TransferEquationParameters = ParameterMenu.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
            RefractionEquationParameters = ParameterMenu.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject;
            EquationPanel = GameObject.Find("EquationPanel");
            QuizPanelText = QuizPanel.transform.GetChild(2).gameObject.GetComponent<Text>();
            HintPromptAnimator = HintPrompt.GetComponent<Animator>();
            HintPromptText = HintPrompt.transform.GetChild(0).gameObject.GetComponent<Text>();
            AnswerField = QuizPanel.transform.GetChild(0).gameObject.GetComponent<InputField>();
            MainCamera = GameObject.Find("Main Camera");
            MainCameraMove = MainCamera.GetComponent<Camera>();           
            CameraStartPos = new Vector3(-130.2f, -19.8f, -6.6875f);
        }

        void Update()
        {
            //Debug.DrawRay(IncidentRay.transform.position, 100 * new Vector3(Mathf.Cos(Mathf.Deg2Rad * (float)InitialAngleOfRay), Mathf.Sin(Mathf.Deg2Rad * (float)InitialAngleOfRay)), Color.green);
            //Debug.DrawRay(MostRecentRayPos, 100 * new Vector3(Mathf.Cos(Mathf.Deg2Rad * (float)InitialAngleOfRay), Mathf.Sin(Mathf.Deg2Rad * (float)InitialAngleOfRay)), Color.blue);          
        }

        public void OnClickQuiz()
        {
            MainCamera.GetComponent<MovementCamera>().enabled = true;
            DoneLoad = false;
            ClearScene();
            InstantiateObjects();
            SetParameters();
            UpdateRadius();
            EquationPanel.GetComponent<Animator>().SetBool("toggleMenu", true);
            ParameterMenu.GetComponent<Animator>().SetBool("toggleMenu", true);
            GetComponent<Animator>().SetBool("TrigBounce", false);
            ParameterButton.SetBool("toggleMenu", true);
            QuizPanel.SetActive(true);
            HintPrompt.SetActive(true);
            HintButton.SetActive(true);
            ObjectArrow.SetActive(false); //set up arrow generation files
            HintButton.GetComponent<HintButtonMulti>().counter = 0;
            QuizPanelText.text = "Find the Height of the Ray at the surface.";
            MainCamera.GetComponent<Animator>().enabled = false;
            MainCameraMove.orthographicSize = 25;
            MainCamera.transform.localPosition = CameraStartPos;
            //isHeightQuestion = true;
            EndQuizCheck = false;
            Cases = "Start";
            LoadCase();
        }

        public void OnClickAdvanceQuiz()
        {
            if (!EndQuizCheck)
            {
                if (decimal.TryParse(AnswerField.text, out AnswerUser))
                {
                    if (CalculateAnswer())
                    {
                        QuizPanelText.fontSize = 14;
                        if (Cases.Equals("IsHeight") || Cases.Equals("Start"))
                        {
                            QuizPanelText.text = "Great! Now solve for the new slope of the ray.";
                            Cases = "IsSlope";
                            HintButton.GetComponent<HintButtonMulti>().counter = 0;
                        }
                        else
                        {
                            Cases = "IsHeight";
                            HintButton.GetComponent<HintButtonMulti>().counter = 0;
                            SetRayPosition();
                        }
                        LoadCase();
                    }
                    else
                    {
                        QuizPanelText.fontSize = 13;
                        if (Cases.Equals("IsHeight"))
                            QuizPanelText.text = "Check your calculations. You need ray height, distance and the initial slope of the ray.";
                        else
                            QuizPanelText.text = "Check your calculations. You need the transfered ray height, radius, initial slope of the ray and the indexes of refraction.";
                    }
                }
                else
                {
                    //error null anim
                }
            }
            else
            {
                QuizPanelText.text = "Generate a new Quiz!";
            }
        }

        private void ClearScene()
        {
            GameObject[] Lines = GameObject.FindGameObjectsWithTag("ProgrammableALR");
            foreach(GameObject obj in Lines)
            {
                DestroyImmediate(obj);
            }
            GameObject[] OpticalElements = GameObject.FindGameObjectsWithTag("OpticalElement");
            foreach(GameObject Elements in OpticalElements)
            {
                DestroyImmediate(Elements);
            }
            PromptPanel.SetActive(false);
            AngleRayHit = false;
        }

        private void InstantiateObjects()
        {
            int randomNumber = numRand.Next(0, 100);
            int randNumScaleMirror = numRand.Next(7, 23);
            for (int i = 0; i < 3; i++)
            {
                int randNumScaleConvex = numRand.Next(17, 40);                          
                decimal randNumScaleConcave = decimal.Round((decimal)(4.5 * numRand.NextDouble() + 1.5), 2, System.MidpointRounding.AwayFromZero);
                /* Reflect
                if(i == 2 && randomNumber <= 50)
                {
                    int ConcaveOrConvex = -90;
                    randomNumber = numRand.Next(0, 100);
                    if (randomNumber <= 50)
                        ConcaveOrConvex = 90;
                    FirstLens = Instantiate(Mirror, FirstElementPosition, Quaternion.Euler(new Vector3(0, ConcaveOrConvex, 90)), Root.transform) as GameObject;
                    Vector3 scale = FirstLens.transform.localScale;
                    Vector3 newScale = new Vector3(scale.x, scale.y, (float)randNumScaleMirror);
                    FirstLens.transform.localScale = newScale;
                    //ListOfLenses.Add(FirstLens);
                    ListLens[i] = FirstLens;
                }
                else*/ if (randomNumber <= 50)
                {
                    FirstLens = Instantiate(ConcaveFab, FirstElementPosition, Quaternion.Euler(new Vector3(0, 90, 0)), Root.transform) as GameObject;
                   
                    Vector3 scale = FirstLens.transform.localScale;
                    Vector3 newScale = new Vector3(scale.x, scale.y, (float)randNumScaleConcave);
                    FirstLens.transform.localScale = newScale;
                    //ListOfLenses.Add(FirstLens);
                    ListLens[i] = FirstLens;
                }
                else
                {
                    FirstLens = Instantiate(ConvexFab, FirstElementPosition, Quaternion.Euler(new Vector3(0, 90, 90)), Root.transform) as GameObject;
                    //ListOfLenses.Add(Instantiate(ConvexFab, FirstElementPosition, Quaternion.Euler(new Vector3(0, 90, 90)), Root.transform));
                    Vector3 scale = FirstLens.transform.localScale;
                    Vector3 newScale = new Vector3(scale.x, scale.y, randNumScaleConvex);
                    FirstLens.transform.localScale = newScale;
                    ListLens[i] = FirstLens;
                }
                FirstElementPosition += new Vector3(20, 0, 0); //could randomly seperate lenses as well
                randomNumber = numRand.Next(0, 100);
            }
            FirstElementPosition = new Vector3(-175.8f, -20, 0);

            int randomNumberRay1 = numRand.Next(-194, -181); //X 
            int randomNumberRay2 = numRand.Next(-28, -12);   //Y
            InitialAngleOfRay = (decimal)(numRand.NextDouble() * 50) - 25;       //angles need to be small for paraxial ray trace
            AngleOfRayConv = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (float)InitialAngleOfRay), Mathf.Sin(Mathf.Deg2Rad * (float)InitialAngleOfRay));
            FirstRayPosition = new Vector3(randomNumberRay1, randomNumberRay2, 0);
            IncidentRay = Instantiate(ProgammableALR, FirstRayPosition, Quaternion.identity, Root.transform) as GameObject;
          
            while (!AngleRayHit) {
                if (Physics.Raycast(FirstRayPosition, new Vector3(Mathf.Cos(Mathf.Deg2Rad * (float)InitialAngleOfRay), Mathf.Sin(Mathf.Deg2Rad * (float)InitialAngleOfRay)), out RayHit))
                {
                    AngleRayHit = true;
                }
                else
                    InitialAngleOfRay = numRand.Next(-25, 25);
            }
            print(InitialAngleOfRay);
            IncidentRay.GetComponent<SetPoints>().InitializeALR();
            IncidentRay.GetComponent<LineRenderer>().useWorldSpace = false;
            IncidentRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.14F;
            IncidentRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.14F;
            IncidentRay.GetComponent<AnimatedLineRenderer>().StartColor = Color.red;
            IncidentRay.GetComponent<AnimatedLineRenderer>().EndColor = Color.red;
            IncidentRay.GetComponent<SetPoints>().SetLinePoint(new Vector3(0,0,0), 0);
            IncidentRay.GetComponent<SetPoints>().SetLinePoint(IncidentRay.transform.InverseTransformPoint(RayHit.point), 1);
            CounterSurface = 0;
            CurrentLens = 0;
        }

        private void SetParameters()
        {
            //Calculate Distance, Height, Angle of Ray
            DistanceOfContact = decimal.Round((decimal)Mathf.Abs((RayHit.point.x - IncidentRay.transform.position.x))*2, 2, System.MidpointRounding.AwayFromZero); //Each Unity unit is 2x in mm. This is just an arbitrary scale I made
            print(DistanceOfContact + "rD");
            InitialHeight = decimal.Round((decimal)(IncidentRay.transform.position.y + 20)*2, 2, System.MidpointRounding.AwayFromZero); //Ray's 0 is -20 in world pos, 
            print(InitialHeight + "rH");
            IndexAir = decimal.Round((decimal) (1.4* numRand.NextDouble() + 1), 2, System.MidpointRounding.AwayFromZero);
            IndexMaterial = decimal.Round((decimal)(1.4 * numRand.NextDouble() + 1),2, System.MidpointRounding.AwayFromZero);
        }

        private void LoadCase()
        {
            int PosCount = IncidentRay.GetComponent<LineRenderer>().positionCount;
            Vector3 PointLocation = IncidentRay.GetComponent<LineRenderer>().GetPosition(PosCount - 1);
            Vector3 RayLocation = IncidentRay.transform.TransformPoint(IncidentRay.GetComponent<LineRenderer>().GetPosition(PosCount - 1));

            switch (Cases)
            {
                case "Start":
                    //For some reason I can't get the pos of last pt on line at start, need to use rayhit
                    /*CameraNewPos = new Vector3 (RayHit.point.x-1, RayHit.point.y, -3);
                    ZoomA = 25;
                    ZoomB = 10;
                    timeWait = 1.5f;
                    StartCoroutine(TransitionCamera());*/

                    IncidentIndexRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexAir.ToString("0.00"); //IndexAir
                    IncidentIndexRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    IndexNField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexAir.ToString("0.00"); //IndexAir
                    IndexNField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    IncidentHeightField.transform.GetChild(0).gameObject.GetComponent<Text>().text = InitialHeight.ToString("0.00mm"); //InitialHeight
                    IncidentHeightField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    IncidentAngleField.transform.GetChild(0).gameObject.GetComponent<Text>().text = (Mathf.Deg2Rad * (float)InitialAngleOfRay).ToString("0.000"); //InitialSlope
                    IncidentAngleField.transform.GetChild(0).gameObject.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    IncidentAngleField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    DistanceField.transform.GetChild(0).gameObject.GetComponent<Text>().text = DistanceOfContact.ToString("0.00mm"); //Distance
                    DistanceField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    RadiusField.transform.GetChild(0).gameObject.GetComponent<Text>().text = Radius.ToString("0.00mm");   //Radius
                    RadiusField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    SecondIndexField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexMaterial.ToString("0.00"); //IndexMaterial
                    SecondIndexField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    AnswerHeightRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);

                    IncidentAngleRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0F, 0F, 0F, 255F);
                    break;

                case "IsHeight":        
                    if(CounterSurface == 0)
                    {
                        CameraNewPos = new Vector3(RayLocation.x, RayLocation.y, -3);
                        ZoomB = 16;
                    }
                    else
                    {
                        CameraNewPos = new Vector3(RayLocation.x + 1, RayLocation.y, -3);
                        ZoomB = 9;
                    }
                    
                    //MainCamera.transform.localPosition = new Vector3(MainCamera.transform.localPosition.x, MainCamera.transform.localPosition.y, 0);
                    //MainCameraMove.orthographicSize = 10;
                    ZoomA = MainCameraMove.orthographicSize;
                    timeWait = 0;
                    StartCoroutine(TransitionCamera());

                    print("In height case");
                    IncidentAngleField.transform.GetChild(0).gameObject.GetComponent<Text>().text = (Mathf.Deg2Rad * (float)InitialAngleOfRay).ToString("0.000"); //InitialSlope
                    IncidentHeightField.transform.GetChild(0).gameObject.GetComponent<Text>().text = InitialHeight.ToString("0.00mm"); //InitialHeight
                    DistanceField.transform.GetChild(0).gameObject.GetComponent<Text>().text = DistanceOfContact.ToString("0.00mm"); //Distance
                    IndexNField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexAir.ToString("0.00"); //IndexAir
                    break;

                case "IsSlope":
                    print("InSlope case");
                    ZoomA = MainCameraMove.orthographicSize;
                    if (CounterSurface == 0)
                    {
                        CameraNewPos = new Vector3(RayLocation.x + 1, RayLocation.y, -3);
                        ZoomB = 5;
                        SecondIndexField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexMaterial.ToString("0.00"); //IndexMaterial
                        IncidentIndexRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexAir.ToString("0.00"); //IndexAir
                        IndexNField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexAir.ToString("0.00"); //IndexAir
                        print("inside");
                    }
                    else
                    {
                        CameraNewPos = new Vector3(RayLocation.x +3, RayLocation.y, -3);
                        print("out");
                        ZoomB = 10;
                        SecondIndexField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexAir.ToString("0.00"); //IndexMaterial
                        IncidentIndexRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexMaterial.ToString("0.00"); //IndexAir
                        IndexNField.transform.GetChild(0).gameObject.GetComponent<Text>().text = IndexMaterial.ToString("0.00"); //IndexAir
                    }
                    timeWait = 0f;
                    StartCoroutine(TransitionCamera());

                    IncidentAngleRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().text = (Mathf.Deg2Rad * (float)InitialAngleOfRay).ToString("0.000"); //RefractSlope                                      
                    RadiusField.transform.GetChild(0).gameObject.GetComponent<Text>().text = Radius.ToString("0.00mm");   //Radius
                    AnswerHeightRefractField.transform.GetChild(0).gameObject.GetComponent<Text>().text = TransferHeight.ToString("0.00mm");   //TransferHeight
                    
                    break;

                case "End":
                    ParameterMenu.GetComponent<Animator>().SetBool("toggleMenu", false);
                    ParameterButton.SetBool("toggleMenu", false);
                    EquationPanel.GetComponent<Animator>().SetBool("toggleMenu", false);
                    GetComponent<Animator>().SetBool("TrigBounce", true);
                    CameraNewPos = new Vector3(-137, -20, -3);
                    ZoomA = MainCameraMove.orthographicSize;
                    ZoomB = 36;
                    TransitionTime = 0.5f;
                    StartCoroutine(TransitionCamera());
                    break;
            }
        }

        private void SetRayPosition()
        {
            int NumOfPos = IncidentRay.GetComponent<LineRenderer>().positionCount;
            Vector3 LastRayPos = IncidentRay.transform.TransformPoint(IncidentRay.GetComponent<LineRenderer>().GetPosition(NumOfPos - 1));
            MostRecentRayPos = LastRayPos;
            Vector3 RayDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (float)InitialAngleOfRay), Mathf.Sin(Mathf.Deg2Rad * (float)InitialAngleOfRay));
            IncidentRay.GetComponent<LineRenderer>().positionCount = NumOfPos + 1;

            if (CounterSurface == 0)
            {
                //reflect
               /* if(ListLens[CurrentLens].name == "ConvexMirror(Clone)" || ListLens[CurrentLens].name == "ConcaveMirror(Clone)")
                {
                    if(ListLens[CurrentLens].name == "ConcaveMirror(Clone)")
                        RayDirection = new Vector3(-RayDirection.x, RayDirection.y);
                    if(ListLens[CurrentLens].name == "ConvexMirror(Clone)")
                }*/
                //Get the next point of contact on same lens
                Ray ray = new Ray(MostRecentRayPos, RayDirection);
                Physics.Raycast(ray.GetPoint(12), -ray.direction, out RayHit);
                print(ray.GetPoint(12) + "pt on ray");                
                IncidentRay.GetComponent<LineRenderer>().SetPosition(NumOfPos, IncidentRay.transform.InverseTransformPoint(RayHit.point));
                CounterSurface = 1;
                QuizPanelText.text = "Great! Now solve for the new height of the ray.";
            }
            else
            {
                //Get the next point of contact on next lens
                Physics.Raycast(MostRecentRayPos, RayDirection, out RayHit);
                if (RayHit.collider == null)
                {
                    QuizPanelText.text = "The trace is done, the ray does not collide with any more lenses!";
                    Ray ray = new Ray(MostRecentRayPos, RayDirection);
                    IncidentRay.GetComponent<LineRenderer>().SetPosition(NumOfPos, IncidentRay.transform.InverseTransformPoint(ray.GetPoint(50)));
                    EndQuiz();                    
                }
                else
                {
                    CurrentLens++;
                    UpdateRadius();
                    QuizPanelText.text = "Great! Now solve for the new height of the ray.";
                    IncidentRay.GetComponent<LineRenderer>().SetPosition(NumOfPos, IncidentRay.transform.InverseTransformPoint(RayHit.point));
                    CounterSurface = 0;
                }
            }            
        }
        private void UpdateRadius()
        {
            float ScaleZ = ListLens[CurrentLens].transform.localScale.z;

            if (ListLens[CurrentLens].name == "ConvexLens(Clone)")
            {
                float ScaleAdjust = 140 - (ScaleZ - 17) * 3.5f;
                Radius = decimal.Round((decimal)(ScaleAdjust), 1, System.MidpointRounding.AwayFromZero);
            }
            else if (ListLens[CurrentLens].name == "ConcaveLens(Clone)")
            {
                float ScaleAdjust = 150 - (ScaleZ - 1.5f) * 25;
                Radius = decimal.Round((decimal)ScaleAdjust, 1, System.MidpointRounding.AwayFromZero);
            }
            else if (ListLens[CurrentLens].name == "ConvexMirror(Clone)" || ListLens[CurrentLens].name == "ConcaveMirror(Clone)")
            {
                float ScaleAdjust = 140 - (ScaleZ - 7) * 5.5f;
                Radius = decimal.Round((decimal)ScaleAdjust, 1, System.MidpointRounding.AwayFromZero);
            }
        }
        private void EndQuiz()
        {
            EndQuizCheck = true;
            Cases = "End";
        }

        public bool CalculateAnswer()
        {
             if (Cases.Equals("IsHeight") || Cases.Equals("Start"))
             {
                if (Cases.Equals("IsHeight")) {
                    int NumOfPos = IncidentRay.GetComponent<LineRenderer>().positionCount;
                    Vector3 PosOfLastPoint = IncidentRay.GetComponent<LineRenderer>().GetPosition(NumOfPos - 2);
                    print(PosOfLastPoint + "pos of last point");
                    Vector3 ConvPosPoint = IncidentRay.transform.TransformPoint(PosOfLastPoint);
                    print(ConvPosPoint + "pos of last point converted");
                    DistanceOfContact = decimal.Round((decimal)Mathf.Abs((RayHit.point.x - ConvPosPoint.x)) * 2, 2, System.MidpointRounding.AwayFromZero);
                }                
                print(InitialHeight + " h1");
                print(DistanceOfContact + " d");
                print(IndexAir + " n1");
                print(decimal.Round((decimal)Mathf.Deg2Rad * InitialAngleOfRay, 3, System.MidpointRounding.AwayFromZero) + " A");

                RoundAngle = decimal.Round((decimal)Mathf.Deg2Rad * InitialAngleOfRay, 3, System.MidpointRounding.AwayFromZero);
                TrueAnswerHeight = decimal.Round((InitialHeight + (DistanceOfContact) * RoundAngle), 2, System.MidpointRounding.AwayFromZero);

                print(TrueAnswerHeight + " theanswer");
                bool CheckNum = Mathf.Abs(((float)TrueAnswerHeight - (float)AnswerUser)) < 0.1f;
                if (CheckNum)
                {                       
                     isAnswerCorrect = true;
                     TransferHeight = AnswerUser;
                }
                else
                {
                    isAnswerCorrect = false;
                }
             }
             else
             {
                if (CurrentLens == 2 && (ListLens[CurrentLens].name == "ConvexMirror(Clone)" || ListLens[CurrentLens].name == "ConcaveMirror(Clone)"))
                {
                    print("in here");
                    //Mirror reflection keeps the same angle as the angle of incidence
                    RoundAngle = decimal.Round((decimal)Mathf.Deg2Rad * InitialAngleOfRay, 3, System.MidpointRounding.AwayFromZero);
                    bool CheckNum = Mathf.Abs(((float)RoundAngle - (float)AnswerUser)) < 0.01f;
                    if (CheckNum)
                    {
                        isAnswerCorrect = true;
                        InitialHeight = TransferHeight;
                    }
                }
                else
                {
                    

                    print(decimal.Round((decimal)Mathf.Deg2Rad * InitialAngleOfRay, 3, System.MidpointRounding.AwayFromZero) + " A");
                    print(IndexAir + " n1");
                    print(TransferHeight + " h2");
                    print(IndexMaterial + " n2");
                    print(Radius*SignConvention() + "R");

                    RoundAngle = decimal.Round((decimal)Mathf.Deg2Rad * InitialAngleOfRay, 3, System.MidpointRounding.AwayFromZero);
                    TrueAnswerSlope = decimal.Round((((RoundAngle * IndexAir) - ((TransferHeight * (IndexMaterial - IndexAir) / (Radius*SignConvention())))) / IndexMaterial), 3, System.MidpointRounding.AwayFromZero);
                    print(TrueAnswerSlope + " theanswerslope");
                    bool CheckNum = Mathf.Abs(((float)TrueAnswerSlope - (float)AnswerUser)) < 0.01f;
                    if (CheckNum)
                    {
                        isAnswerCorrect = true;
                        InitialAngleOfRay = decimal.Round((decimal)Mathf.Rad2Deg * AnswerUser, 3, System.MidpointRounding.AwayFromZero);
                        print(InitialAngleOfRay + "conv user angle");
                        print(decimal.Round((decimal)Mathf.Rad2Deg * AnswerUser, 3, System.MidpointRounding.AwayFromZero) + "conv user angle rad");
                        InitialHeight = TransferHeight;
                    }
                    else
                    {
                        isAnswerCorrect = false;
                    }
                }
             }           
         return isAnswerCorrect;
        }

        private void DisplayPrompt(string text, float duration)
        {
            HintPromptAnimator.SetBool("DisplayPrompt", true);
            HintPromptAnimator.SetBool("DisplayPrompt", true);
            HintPromptText.text = text;
            StartCoroutine(DestroyMessagePrompt(duration));
        }
        public int SignConvention()
        {
            int signConvention = 1;
            if (ListLens[CurrentLens].name == "ConvexLens(Clone)")
                if (CounterSurface == 0)
                    signConvention = 1;
                else
                    signConvention = -1;
            else if (ListLens[CurrentLens].name == "ConcaveLens(Clone)")
                if (CounterSurface == 0)
                    signConvention = -1;
                else
                    signConvention = 1;
            return signConvention;
        }

        IEnumerator DestroyMessagePrompt(float time)
        {
            if (isUICoroutineExecuting)
                yield break;

            isUICoroutineExecuting = true;

            yield return new WaitForSeconds(time);

            HintPromptAnimator.SetBool("DisplayPrompt", false);
            HintPromptText.gameObject.GetComponent<Animator>().SetBool("DisplayPrompt", false);

            isUICoroutineExecuting = false;
        }

        IEnumerator TransitionCamera()
        {
            yield return new WaitForSeconds(timeWait);

            float t = 0.0f;
            Vector3 StartingPos = MainCamera.transform.position;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / TransitionTime);
                MainCamera.transform.position = Vector3.Lerp(StartingPos, CameraNewPos, t);
                MainCameraMove.orthographicSize = Mathf.Lerp(ZoomA, ZoomB, t);
                yield return 0;
            }
        }
    }
}
