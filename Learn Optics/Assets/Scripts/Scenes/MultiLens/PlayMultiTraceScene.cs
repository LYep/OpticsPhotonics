using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class is a mess, a lot of this was testing snell's law and how it can work in Unity
//Plan to redo the code done to calculate the ray tracing so it's more organized
namespace DigitalRuby.AnimatedLineRenderer {
    public class PlayMultiTraceScene : MonoBehaviour {

        Text panelText;
        Text panelTextInfo;
        Text panelTitle;
        Text SnellTextVar1;
        Text SnellTextVar2;
        Text SnellTextVar3;
        Text SnellTextVar4;
        Text RefractCross;
        public GameObject ProgrammableALR;
        public GameObject ConcaveFab;
        public GameObject MirrorConvexFab;
        public GameObject MirroConcaveFab;
        GameObject MainCamera;
        GameObject PromptPanel;
        GameObject continuePanelButton;
        GameObject SnellPanel;
        GameObject DemoRay;
        GameObject NormalRay;
        GameObject NormalRay2;
        GameObject ReflectedRay;
        GameObject ReflectedRay2;
        GameObject InitialOpticalElement;
        GameObject OpticalElementConcave;
        GameObject OpticalElementMirror;
        GameObject OverlaySnell;
        GameObject SnellInsideEquations;
        GameObject ObjectArrow;
        GameObject ArrowHeight;
        GameObject OverlayHeight;
        GameObject OverlayRefract;
        GameObject OverlayExit;
        GameObject OverlayDemo;
        GameObject OverlayDemoRefract;
        GameObject OverlayRadius;
        GameObject OverlayRadiusConcave;
        GameObject OverlayRadiusMirror;
        GameObject SurfacePowerPanel;
        GameObject ParameterPanel;
        GameObject EquationPanel;
        GameObject TransferEquationButton;
        GameObject GenerateQuizButton;
        GameObject SkipLessonButton;
        GameObject QuizPanel;
        GameObject HintPrompt;
        GameObject HintButton;
        GameObject RadiusRay;
        Image SnellPrincipal;
        Image SnellApprox;
        Image SnellHeight;
        Image SnellMirror;
        Image Refract;
        Image Transfer;
        Image Phi;
        Animator SnellPanelAnimator;
        Animator PromptPanelAnimator;
        Animator SnellImage;
        Animator ParameterButton;
        Animator Camera;
        Animator CrossOut;
        RaycastHit RayHit;
        RaycastHit RayHit2;
        public int counter;
        float timeWait;
        float tempVar;
        Vector3 RefractedAngle;
        Vector3 PreRefractedAngle;
        Vector3 pos;
        Vector3 pos2;
        Vector3 bpoint;
        Vector3 TestAngle;
        // Use this for initialization
        void Start() {
            InitializeObjects();
            counter = 0;

            CrossOut.gameObject.SetActive(false);
            SnellPrincipal.enabled = true;
            SnellApprox.enabled = false;
            SnellHeight.enabled = false;
            Transfer.enabled = false;
            Phi.enabled = false;
            SnellMirror.enabled = false;
            Refract.enabled = false;
            RefractCross.enabled = false;
            OverlaySnell.SetActive(false);
            OverlayHeight.SetActive(false);
            OverlayRefract.SetActive(false);
            OverlayExit.SetActive(false);
            OverlayDemo.SetActive(false);
            OverlayDemoRefract.SetActive(false);
            OverlayRadius.SetActive(false);
            OverlayRadiusConcave.SetActive(false);
            OverlayRadiusMirror.SetActive(false);
            SnellInsideEquations.SetActive(false);
            ObjectArrow.SetActive(false);
            ArrowHeight.SetActive(false);
            SurfacePowerPanel.SetActive(false);
            SurfacePowerPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            QuizPanel.SetActive(false);
            HintPrompt.SetActive(false);
            HintButton.SetActive(false);
            panelText.text = "Welcome to Paraxial Ray Tracing Lesson 2.";
            pos = InitialOpticalElement.transform.position;
        }
        private void Update()
        {
            // Debug.DrawRay(DemoRay.transform.position, 100 * Vector3.right, Color.blue);
            // Debug.DrawRay(RayHit.point, 100*RayHit.normal, Color.green);
            //Debug.DrawRay(new Vector3(RayHit.point.x+5, RayHit.point.y), 100*PreRefractedAngle, Color.red);
            //print(RayHit.point);
            Debug.DrawRay(bpoint, 100 * Vector3.right, Color.green);
            Debug.DrawRay(bpoint, 100 * RayHit.normal, Color.gray);
            Debug.DrawRay(bpoint, 100 * TestAngle, Color.black);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
            Debug.DrawRay(bpoint, 70*PreRefractedAngle, Color.blue);
        }
        //Stats: 78.5mm R1, Y1 10mm, Y2 29.188mm, d 50mm, 
        public void OnClick() 
        {
            switch (counter)
            {
                case 0:
                    GenerateQuizButton.SetActive(false);
                    continuePanelButton.transform.GetChild(0).GetComponent<Text>().text = "Continue";
                    panelText.text = "In order to trace through multiple surfaces, we need to understand a " +
                        "couple of equations.";
                    
                    DemoRay = Instantiate(ProgrammableALR, new Vector3(pos.x - 20, pos.y + 2), Quaternion.identity);
                    DemoRay.GetComponent<SetPoints>().InitializeALR();
                    DemoRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.07F;
                    DemoRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.07F;
                    DemoRay.GetComponent<SetPoints>().SetLinePoint(DemoRay.transform.position, 0);
                    //Hardcoded since this is just a demo.
                    Physics.Raycast(DemoRay.transform.position, new Vector3(50, 10), out RayHit);
                    
                    DemoRay.GetComponent<SetPoints>().SetLinePoint(RayHit.point, 1);

                    //print(AngleBetween(new Vector3(50, 10), new Vector3(5, 0))); //ray and axis
                    //11.3 degree angle between ray and axis
                    //print(AngleBetween(new Vector3(50, 10), RayHit.normal)); //ray and normal
                    //157.66 degree (180-157.66: 22.34) correct
                    //print(AngleBetween(Vector3.right, -RayHit.normal)); //normal and axis
                    //11.0288
                    //print(RayHit.normal); //-1, 0.2
                    //print(Mathf.Rad2Deg*CalculateRefractedAngle(AngleBetween(new Vector3(50, 10), RayHit.normal)));
                    //print(AngleBetween(new Vector3(50, 10), new Vector3(10, 0)) + tempVar);
                    //25.98 degree between refracted angle and axis
                    break;

                case 1:
                    //Calculate refracted angle
                    PromptPanelAnimator.SetTrigger("PopOut"); //could just overlay snell panel 
                    SnellPanelAnimator.SetTrigger("PopIn");
                    //Zoom in camera to point of contact, show angles.
                    Camera.SetTrigger("ZoomInSnell");

                    NormalRay = Instantiate(ProgrammableALR, RayHit.point, Quaternion.identity);
                    
                    //In order for line to appear in the direction of the normal, we need to use local space               
                    NormalRay.GetComponent<SetPoints>().InitializeALR();
                    NormalRay.GetComponent<LineRenderer>().useWorldSpace = false;
                    NormalRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.07F;
                    NormalRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.07F;
                    NormalRay.GetComponent<AnimatedLineRenderer>().StartColor = Color.black;
                    NormalRay.GetComponent<AnimatedLineRenderer>().EndColor = Color.black;
                    NormalRay.GetComponent<SetPoints>().SetLinePoint(100 * RayHit.normal, 0);  
                    NormalRay.GetComponent<SetPoints>().SetLinePoint(new Vector3(0,0,0), 1);
                    NormalRay.GetComponent<SetPoints>().SetLinePoint(100 * -RayHit.normal, 1);

                    //Create reflected Ray, with 14.6780 degrees between normal(calculated with snell)
                    ReflectedRay = Instantiate(ProgrammableALR, RayHit.point, Quaternion.identity);
                    ReflectedRay.GetComponent<SetPoints>().InitializeALR();
                    ReflectedRay.GetComponent<LineRenderer>().useWorldSpace = false;
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.07F;
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.07F;
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().StartColor = Color.red;
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().EndColor = Color.red;                  
                    ReflectedRay.GetComponent<SetPoints>().SetLinePoint(new Vector3(0, 0, 0), 0);
                    ReflectedRay.GetComponent<SetPoints>().SetLinePoint(CalculateRefractedRay(14.678F), 1);
                    //timeWait = 5;
                    //StartCoroutine(Wait());
                   // OverlaySnell.SetActive(true);
                    break;

                case 2:
                    panelTextInfo.text = "However, if we assume the rays make a small angle against the principal "
                        + "axis, then we can approximate Snell's Law as a linear relationship.";
                    SnellApprox.enabled = true;
                    SnellPrincipal.enabled = false;
                    SnellImage.SetTrigger("Bounce");
                    CrossOut.gameObject.SetActive(true);
                    CrossOut.SetTrigger("ScaleUp");
                    break;

                case 3:
                    OverlaySnell.SetActive(false);
                    NormalRay.SetActive(false);
                    ReflectedRay.SetActive(false);
                    Camera.SetTrigger("ZoomOutSnell");
                    panelTextInfo.text = "With this approximation, we can utilize two linear equations which makes ray tracing "
                        + "easy enough to do.";                                       
                    break;

                case 4:
                    ObjectArrow.SetActive(true);
                    ArrowHeight.SetActive(true);
                    Camera.SetTrigger("ZoomInHeight");
                    panelTitle.text = "Transfer Equation";
                    panelTextInfo.text = "We can calculate the height at which the ray will strike an optical lens by taking "
                        + "into account its angle of incidence and distance from its origin to the lens as well as the ray's " +
                        "height from the principal axis.";
                    SnellTextVar1.text = "y = height of ray";
                    SnellTextVar2.text = "d = distance from origin to hit";
                    SnellTextVar3.text = "u = slope of ray";
                    SnellTextVar4.text = "";
                    SnellApprox.enabled = false;
                    SnellHeight.enabled = true;
                    SnellHeight.gameObject.GetComponent<Animator>().SetTrigger("Bounce");
                    //show ray hitting lens, showing heights being affected.                                       
                    //OverlayHeight.SetActive(true);
                    ObjectArrow.GetComponent<Animator>().SetTrigger("ScaleUp");
                    ArrowHeight.GetComponent<Animator>().SetTrigger("ScaleUp");                   
                    break;

                case 5:
                    //Ensures previous overlay is cleared
                    OverlayHeight.SetActive(false);
                    Camera.SetTrigger("PanRefract");
                    panelTextInfo.text = "Once the height is found, we can calculate the direction or slope of the ray as it travels  "
                        + "through the second medium. We use nu as the approximation of the angles of incidence and reflection.";
                    panelTitle.text = "Refraction Equation";
                    SnellTextVar1.text = "y = height of ray at lens";
                    SnellTextVar2.text = "phi = surface power";
                    SnellTextVar3.text = "u = slope of ray";
                    SnellTextVar4.text = "R = radius of curvature";
                    Refract.enabled = true;
                    SnellHeight.enabled = false;                               
                    ObjectArrow.SetActive(false);
                    //ArrowHeight.SetActive(false);
                    NormalRay.SetActive(true);
                    ReflectedRay.SetActive(true);
                    Refract.GetComponent<Animator>().SetTrigger("Bounce");
                    // Refract.gameObject.GetComponent<Animator>().SetTrigger("Bounce");
                    break;

                case 6:
                    ArrowHeight.SetActive(false);
                    NormalRay.SetActive(false);
                    ReflectedRay.SetActive(false);
                    DemoRay.SetActive(false);
                    Camera.SetTrigger("PanRadius");
                    panelTextInfo.text = "Surface power will dictate how much the ray will deviate from its path. A surface with positive power " +
                        "will bend the ray towards the principal axis whereas a negative power will diverge the ray away.";
                    SurfacePowerPanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("KeepBounce", false);
                    SurfacePowerPanel.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("SlideUp");                                                          
                    break;
                //Insert explanation of positive and negative radii
                case 7:
                    OverlayRefract.SetActive(false); //Sometimes if user clicks too fast through tutorial, the function to clear the radius in CameraAnimation isn't called.
                    panelTextInfo.text = "The sign convention for radius is determined by the center of the lens. If the center of curvature " +
                        "is to the right of the surface, then the radius will be positive.";
                    OverlayRadius.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Bounce");
                    OverlayRadius.transform.GetChild(2).gameObject.GetComponent<Animator>().SetTrigger("Bounce");
                    RadiusRay = Instantiate(ProgrammableALR, new Vector3(pos.x - 10, pos.y + 1.5f), Quaternion.identity);
                    RadiusRay.GetComponent<SetPoints>().InitializeALR();
                    RadiusRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.07F;
                    RadiusRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.07F;
                    RadiusRay.GetComponent<SetPoints>().SetLinePoint(RadiusRay.transform.position, 0);
                    Physics.Raycast(RadiusRay.transform.position, new Vector3(50, 7), out RayHit);
                    RadiusRay.GetComponent<SetPoints>().SetLinePoint(RayHit.point, 1);
                    break;

                case 8:
                    Physics.Raycast(new Vector3(pos.x + 10, pos.y + 1), new Vector3(-50, 5), out RayHit);
                    RadiusRay.GetComponent<SetPoints>().SetLinePoint(RayHit.point, 1);
                    panelTextInfo.text = "If the center of curvature is to the left of the surface, " +
                        "then the radius will be negative. This assumes the ray is traveling left to right.";
                    OverlayRadius.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Bounce");
                    OverlayRadius.transform.GetChild(3).gameObject.GetComponent<Animator>().SetTrigger("Bounce");
                    break;
                case 9:
                    Vector3 spawn = new Vector3(InitialOpticalElement.transform.position.x + 15, InitialOpticalElement.transform.position.y);
                    OpticalElementConcave = Instantiate(ConcaveFab, spawn, Quaternion.Euler(new Vector3(0, 90, 0)));
                    Physics.Raycast((RadiusRay.GetComponent<LineRenderer>().GetPosition(2)), new Vector3(100, -5), out RayHit);
                    RadiusRay.GetComponent<SetPoints>().SetLinePoint(RayHit.point, 1);

                  //  RadiusRay.GetComponent<SetPoints>().SetLinePoint(new Vector3(-140, -19), 1);
                    panelTextInfo.text = "This ray tracing process is repeated until the ray reaches the principal axis " +
                        "and forms an image of the object.";
                    SurfacePowerPanel.GetComponent<Animator>().SetTrigger("SlideDown");                    
                    break;

                case 10:
                    panelTextInfo.text = "Concave lenses follow the same sign convention.";                                                
                    Camera.SetTrigger("PanRadius2");
                    break;

                case 11:
                    //change equation panel
                    OpticalElementConcave.SetActive(false);
                    RadiusRay.SetActive(false);
                    ArrowHeight.SetActive(true);
                    Camera.SetTrigger("PanRefract2");
                    ArrowHeight.GetComponent<Animator>().SetTrigger("ScaleUp2");
                    Refract.GetComponent<Animator>().SetTrigger("Idle2");
                    panelTitle.text = "Equations";
                    SnellTextVar1.enabled = false;
                    SnellTextVar2.enabled = false;
                    SnellTextVar3.enabled = false;
                    SnellTextVar4.enabled = false;
                    Transfer.enabled = true;
                    Phi.enabled = true;
                    panelTextInfo.text = "This ray will diverge to a new height and finally refract out of the lens with a new slope (away from the normal line).";
                    DemoRay.SetActive(true);
                    ReflectedRay.SetActive(true);
                    NormalRay.SetActive(false);//destroy

                    //This section of code can be used for sandbox to traverse through lenses
                    Vector3 apoint = ReflectedRay.GetComponent<LineRenderer>().GetPosition(1); //Current position is somewhere in space(not in lens)
                    bpoint = ReflectedRay.transform.TransformPoint(apoint);
                    print(apoint);
                    print(ReflectedRay.transform.TransformPoint(apoint) + "Transformed point");
                    Physics.Raycast(bpoint, -PreRefractedAngle, out RayHit);//necessary to find the normal of surface to update functions below

                    ReflectedRay.GetComponent<LineRenderer>().SetPosition(1, ReflectedRay.transform.InverseTransformPoint(RayHit.point)); //Cast direction backwards to find hit, setpos there

                    NormalRay2 = Instantiate(ProgrammableALR, RayHit.point, Quaternion.identity);            
                    NormalRay2.GetComponent<SetPoints>().InitializeALR();
                    NormalRay2.GetComponent<LineRenderer>().useWorldSpace = false;
                    NormalRay2.GetComponent<AnimatedLineRenderer>().StartWidth = 0.07F;
                    NormalRay2.GetComponent<AnimatedLineRenderer>().EndWidth = 0.07F;
                    NormalRay2.GetComponent<AnimatedLineRenderer>().StartColor = Color.black;
                    NormalRay2.GetComponent<AnimatedLineRenderer>().EndColor = Color.black;
                    NormalRay2.GetComponent<SetPoints>().SetLinePoint(100 * RayHit.normal, 0);
                    NormalRay2.GetComponent<SetPoints>().SetLinePoint(new Vector3(0, 0, 0), 1);
                    NormalRay2.GetComponent<SetPoints>().SetLinePoint(100 * -RayHit.normal, 1);

                    print(AngleBetween(RefractedAngle, RayHit.normal) + "angle betw ray normal");//7.79 degree
                    print(AngleBetween(RayHit.normal, Vector3.right) + "angle betw normal/axis");
                    print(CalculateRefractedRay(168.27F) + "angle between refract and axis");//360-0.29: 359.71 Also print will call/execute functions, good for updating
                    ReflectedRay.GetComponent<SetPoints>().SetLinePoint((RefractedAngle), 1); //sets next point somewhere in space in refractred direction
                   
                    // ReflectedRay.GetComponent<LineRenderer>().SetPosition(2, ReflectedRay.transform.InverseTransformPoint(RayHit.point));
                    //Show Animation of ray bouncing across surfaces.
                    break;

                case 12:
                    //Need to be careful here, scene can get wonkey if clicked through too fast
                    OverlayExit.SetActive(false);
                    ArrowHeight.SetActive(false);
                    OverlayRadiusConcave.SetActive(false);
                    Camera.SetTrigger("PanRight");
                    PromptPanelAnimator.SetTrigger("PopIn");
                    SnellPanelAnimator.SetTrigger("PopOut");
                    SurfacePowerPanel.SetActive(false);
                    
                    NormalRay2.SetActive(false); //Destroy
                    panelText.text = "We can continue propagating through multiple lenses, using the same equations.";
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.15F;
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.15F;

                    Vector3 cpoint = ReflectedRay.GetComponent<LineRenderer>().GetPosition(1); //Point of contact on lens
                    bpoint = ReflectedRay.transform.TransformPoint(cpoint);
                    Vector3 cast = new Vector3(bpoint.x + 2, bpoint.y,0); //This changes the angles, fix
                    Vector3 spawn2 = new Vector3(InitialOpticalElement.transform.position.x + 22, InitialOpticalElement.transform.position.y);

                    OpticalElementConcave = Instantiate(ConcaveFab, spawn2, Quaternion.Euler(new Vector3(0, 90, 0)));
                    Physics.Raycast(cast, PreRefractedAngle, out RayHit);

                    ReflectedRay.GetComponent<LineRenderer>().SetPosition(2, ReflectedRay.transform.InverseTransformPoint(RayHit.point));
                    ReflectedRay2 = Instantiate(ProgrammableALR, RayHit.point, Quaternion.identity);
                    ReflectedRay2.GetComponent<SetPoints>().InitializeALR();
                    ReflectedRay2.GetComponent<LineRenderer>().useWorldSpace = false;
                    ReflectedRay2.GetComponent<AnimatedLineRenderer>().StartWidth = 0.15F;
                    ReflectedRay2.GetComponent<AnimatedLineRenderer>().EndWidth = 0.15F;
                    ReflectedRay2.GetComponent<AnimatedLineRenderer>().StartColor = Color.red;
                    ReflectedRay2.GetComponent<AnimatedLineRenderer>().EndColor = Color.red;
                    ReflectedRay2.GetComponent<SetPoints>().SetLinePoint(new Vector3(0, 0, 0), 1);
                  
                    print(AngleBetween(new Vector3(1, 0), PreRefractedAngle)); //ray and axis order matters 
                    //-0.29 degree angle between ray and axis
                    print(AngleBetween(PreRefractedAngle, RayHit.normal)); //ray and normal
                    //-168.5 degree (180-168.5: 11.4624) 
                    //print(AngleBetween(Vector3.right, -RayHit.normal)); //normal and axix
                    //ReflectedRay2.GetComponent<SetPoints>().SetLinePoint(CalculateRefractedRay(11.46f), 1);

                    print(CalculateRefractedRay(11.46f));
                    Vector3 dpoint = ReflectedRay2.GetComponent<LineRenderer>().GetPosition(1);
                    bpoint = ReflectedRay2.transform.TransformPoint(RefractedAngle);
                    Vector3 fpoint = ReflectedRay2.GetComponent<LineRenderer>().GetPosition(0);
                    print(dpoint + " d point" + "fpoint" + fpoint);
                    //cast = new Vector3(bpoint.x + 10, bpoint.y);

                    Physics.Raycast(bpoint, -PreRefractedAngle, out RayHit);
                    print(RayHit.point);
                    print(RayHit.collider.name);
                    //ReflectedRay2.GetComponent<LineRenderer>().positionCount = 1;
                    ReflectedRay2.GetComponent<SetPoints>().SetLinePoint(ReflectedRay2.transform.InverseTransformPoint(RayHit.point), 1);
                    break;

                case 13:
                    panelText.text = "However, mirrors require a slight change in the refraction equation.";
                    Camera.SetTrigger("PanMirror");
                    Vector3 spawn3 = new Vector3(InitialOpticalElement.transform.position.x + 42, InitialOpticalElement.transform.position.y + 20);
                    OpticalElementMirror = Instantiate(MirrorConvexFab, spawn3, Quaternion.Euler(new Vector3(0, -90, 90)));

                    print(AngleBetween(new Vector3(1, 0), PreRefractedAngle)); //ray and axis 22.6DEGREE
                    print(AngleBetween(RayHit.normal,PreRefractedAngle)); //ray and normal 36.32 degree
                    print(CalculateRefractedRay(36.32f) + "calculaed refrac out of concave");
                    //print(CalculateAngleSnell(36.32f) + "using updated function");

                    Vector3 ConcavePoint = ReflectedRay2.GetComponent<LineRenderer>().GetPosition(1);
                    bpoint = ReflectedRay2.transform.TransformPoint(ConcavePoint);

                    Physics.Raycast(bpoint, CalculateAngleSnell(36.32f), out RayHit);
                    print(RayHit.point);
                    print(RayHit.collider.name);
                    ReflectedRay2.GetComponent<SetPoints>().SetLinePoint(ReflectedRay2.transform.InverseTransformPoint(RayHit.point), 1);
                    break;

                case 14:
                    SurfacePowerPanel.SetActive(true);
                    PromptPanelAnimator.SetTrigger("PopOut");
                    SnellPanelAnimator.SetTrigger("PopIn");
                    SurfacePowerPanel.GetComponent<Animator>().SetTrigger("SlideUp");
                    SurfacePowerPanel.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("SlideUp");
                    SurfacePowerPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                    SurfacePowerPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                    SnellPanel.transform.GetChild(3).localPosition += new Vector3(0, 19, 0);
                    panelTextInfo.text = "The refraction equation changes by only taking into account the height of the ray and the mirror's radius. " +
                        "Furthermore, when the ray of light bounces and travels from right to left, the indices of refraction switches signs.";
                    Transfer.enabled = false;
                    Phi.enabled = false;
                    RefractCross.enabled = true;                 
                    SnellTextVar1.enabled = true;
                    SnellTextVar1.text = "n isn't used since the ray stays in the same medium.";
                    SnellMirror.enabled = true;

                    print(AngleBetween(new Vector3(1, 0), TestAngle)); //ray and axis 
                    print(AngleBetween(TestAngle, RayHit.normal)); //48.98 degree ray and normal 
                    print(CalculateAngleMirror(48.98F)); //this angle

                    Vector3 MirrorPoint = ReflectedRay2.GetComponent<LineRenderer>().GetPosition(2);
                    bpoint = ReflectedRay2.transform.TransformPoint(MirrorPoint);

                    Ray ray = new Ray(bpoint, CalculateAngleMirror(48.98F));
                    ReflectedRay2.GetComponent<SetPoints>().SetLinePoint(ReflectedRay2.transform.InverseTransformPoint(ray.GetPoint(50)), 1);
                    break;

                case 15:
                    panelTextInfo.text = "As the ray travels from left to right, distance traveled is taken negative. " +
                        "The mirror's radius sign convention is the same as lenses. Convex mirrors take a positive radius. Concave mirrors will be negative.";
                    Camera.SetTrigger("PanMirror2");
                    
                    break;

                case 16:
                    //Camera.SetTrigger("ZoomInDouble");
                    OverlayRadiusMirror.SetActive(false);
                    PromptPanelAnimator.SetTrigger("PopIn");
                    SnellPanelAnimator.SetTrigger("PopOut");
                    SurfacePowerPanel.SetActive(false);
                    panelText.text = "Using the equations above and the parameter panel to the left can help guide you through the tracing.";
                    ParameterPanel.GetComponent<Animator>().SetBool("toggleMenu", true);
                    EquationPanel.GetComponent<Animator>().SetBool("toggleMenu", true);
                    ParameterButton.SetBool("toggleMenu", true);
                    ObjectArrow.SetActive(true);
                    ObjectArrow.GetComponent<Animator>().SetTrigger("ScaleUp");
                    break;

                case 17:
                    Camera.SetTrigger("DemoRay");
                    panelText.text = "The given parameters of each lens will be filled out as well as ray properties.";
                    EquationPanel.GetComponent<Animator>().SetBool("toggleMenu", false);
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().StartWidth = 0.07F;
                    ReflectedRay.GetComponent<AnimatedLineRenderer>().EndWidth = 0.07F;
                    break;

                case 18:
                    //Camera.SetTrigger("DemoRay");
                    //could create temp button for demo to avoid checking for demo status on each click
                    TransferEquationButton.GetComponent<OnTransferEdit>().Demo = true;
                    panelText.transform.localPosition -= new Vector3(0, 7, 0);
                    PromptPanel.transform.GetChild(3).localPosition -= new Vector3(69, 7, 0);
                    panelText.text = "Utilize the equations above to calculate the new heights and slopes of the ray for each surface hit. Edit the Transfer Equation to continue.";
                    EquationPanel.GetComponent<Animator>().SetBool("toggleMenu", true);
                    PromptPanel.transform.GetChild(0).gameObject.SetActive(false);                  
                    break;

                case 19:                   
                    panelText.text = "Fill in the variables and calculate the new height of the ray to continue.";
                    EquationPanel.GetComponent<CalculateEquations>().DemoHeight = true;
                    break;

                case 20:
                    ArrowHeight.SetActive(true);
                    Camera.SetTrigger("DemoRefract");
                    ArrowHeight.GetComponent<Animator>().SetTrigger("ScaleUpDemo");
                    panelText.text = "Now calculate the new slope of the ray to continue. Hit Return and use the Refraction Equation to find the slope.";
                    EquationPanel.GetComponent<CalculateEquations>().DemoRefract = true;                                   
                    break;

                case 21:
                    OverlayDemoRefract.SetActive(false);                                 
                    PromptPanel.transform.GetChild(3).gameObject.SetActive(false);                   
                    ArrowHeight.SetActive(false);
                    GenerateQuizButton.SetActive(true);
                    SkipLessonButton.SetActive(false);
                    Camera.SetTrigger("DoneLesson");
                    panelText.text = "You have now completed Lesson 2.";
                    GenerateQuizButton.GetComponent<Animator>().SetBool("TrigBounce", true);                    
                    break;
            }
            counter++;
        }

        private void InitializeObjects()
        {
            //pay attention to order of objects in heirarchy, using getChild to avoid using Find
            MainCamera = GameObject.Find("Main Camera");
            Camera = MainCamera.GetComponent<Animator>();
            MainCamera.GetComponent<MovementCamera>().enabled = false;
            InitialOpticalElement = GameObject.Find("ConvexLens");
            PromptPanel = GameObject.Find("PromptPanel");
            PromptPanelAnimator = PromptPanel.GetComponent<Animator>();
            panelText = PromptPanel.transform.GetChild(1).gameObject.GetComponent<Text>();
            continuePanelButton = PromptPanel.transform.GetChild(0).gameObject;
            SnellPanel = GameObject.Find("Snells");
            panelTitle = SnellPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
            SnellPrincipal = SnellPanel.transform.GetChild(1).gameObject.GetComponent<Image>();
            SnellTextVar1 = SnellPanel.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
            SnellTextVar2 = SnellPanel.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>();
            SnellTextVar3 = SnellPanel.transform.GetChild(2).GetChild(2).gameObject.GetComponent<Text>();
            SnellTextVar4 = SnellPanel.transform.GetChild(2).GetChild(3).gameObject.GetComponent<Text>();
            panelTextInfo = SnellPanel.transform.GetChild(3).gameObject.GetComponent<Text>();
            SnellApprox = GameObject.Find("SnellImageApprox").GetComponent<Image>();
            SnellPanelAnimator = SnellPanel.GetComponent<Animator>();
            SnellImage = SnellPanel.transform.GetChild(5).gameObject.GetComponent<Animator>();
            SnellHeight = SnellPanel.transform.GetChild(6).gameObject.GetComponent<Image>();
            Refract = SnellPanel.transform.GetChild(7).gameObject.GetComponent<Image>();
            Transfer = SnellPanel.transform.GetChild(9).gameObject.GetComponent<Image>();
            Phi = SnellPanel.transform.GetChild(10).gameObject.GetComponent<Image>();
            SnellMirror = SnellPanel.transform.GetChild(12).gameObject.GetComponent<Image>();
            RefractCross = SnellPanel.transform.GetChild(11).gameObject.GetComponent<Text>();
            OverlaySnell = GameObject.Find("OverlayEquations");           
            SnellInsideEquations = OverlaySnell.transform.GetChild(9).gameObject;
            CrossOut = OverlaySnell.transform.GetChild(12).gameObject.GetComponent<Animator>();
            ObjectArrow = GameObject.Find("ObjectArrow");
            ArrowHeight = GameObject.Find("ArrowHeight");
            OverlayHeight = GameObject.Find("OverlayEquationsHeight");
            OverlayRefract = GameObject.Find("OverlayEquationsRefract");
            OverlayExit = GameObject.Find("OverlayEquationsExit"); 
            OverlayDemo = GameObject.Find("OverlayDemo");
            OverlayRadius = GameObject.Find("OverlayRadius");
            OverlayRadiusConcave = GameObject.Find("OverlayRadiusConcave");
            OverlayDemoRefract = GameObject.Find("OverlayDemoRefract");
            OverlayRadiusMirror = GameObject.Find("OverlayRadiusMirror");
            SurfacePowerPanel = GameObject.Find("SurfacePowerPanel");
            ParameterPanel = GameObject.Find("Parameter Menu");
            EquationPanel = GameObject.Find("EquationPanel");
            TransferEquationButton = EquationPanel.transform.Find("SelectTransfer").gameObject;
            ParameterButton = GameObject.Find("MenuButton").GetComponent<Animator>();
            GenerateQuizButton = GameObject.Find("GenerateQuizButton");
            SkipLessonButton = PromptPanel.transform.GetChild(2).gameObject;
            QuizPanel = GameObject.Find("QuizPanel");
            HintPrompt = GameObject.Find("HintPrompt");
            HintButton = GameObject.Find("HintButton");
        }

        //This is the closest way to calculate the ray tracing. Once we have the angle between the ray and the normal at first contact hit (done by
        //doing a simple AngleBetween(RayDirection, RayHit.normal) and taking the positive angle below 180), send it to this function
        //We then do a simple calculation to find the refracted angle between the ray and normal, however this won't work because we need to raycast
        //out in a direction relative to the x axis, aka the principal axis. So we find the angle between the x-axis and the normal, and subtract 
        //that from the refracted angle to get the true angle needed to raycast out.
        //However that only works in certain cases, sometimes we need to add the angles, sometimes we need to multiply by -1. Finish a function that can handle
        //all of these possible cases and thats what will be used to calculate the dynamic ray tracing in sandbox
        private Vector3 CalculateAngleSnell (float RayDegreeToNormal)
        {  
            float IndexAir = 1;
            float IndexLens = 1.5f;

            //Going from lens to air, not air to lens, so divide lens by air
            float CalculateRefraction = (IndexLens / IndexAir) * Mathf.Sin(Mathf.Deg2Rad * RayDegreeToNormal);
            float RefractedAngleToNormal = Mathf.Rad2Deg * Mathf.Asin(CalculateRefraction); //60 deg

            Vector3 DirectionFromNormal = new Vector3(Mathf.Cos(Mathf.Deg2Rad * RefractedAngleToNormal), Mathf.Sin(Mathf.Deg2Rad * RefractedAngleToNormal));
            float AngleBetweenNormalAndAxis = AngleBetween(RayHit.normal, Vector3.right);
            print(AngleBetweenNormalAndAxis + "angle of normal and axis");

            float FinalAngle = RefractedAngleToNormal - AngleBetweenNormalAndAxis;
            print(FinalAngle + "the final angle");

            Vector3 DirectionForRaycast = new Vector3(Mathf.Cos(Mathf.Deg2Rad * FinalAngle), Mathf.Sin(Mathf.Deg2Rad * FinalAngle));
            TestAngle = DirectionForRaycast;
            print(DirectionForRaycast + "direction of raycast");

            return 100*DirectionForRaycast;
        }
        private Vector3 CalculateAngleMirror(float RayDegreeToNormal)
        {
            float AngleBetweenNormalAndAxis = -AngleBetween(Vector3.left, RayHit.normal);
            print(AngleBetweenNormalAndAxis + "angle betwen normal/axis");
            float FinalAngle = AngleBetweenNormalAndAxis + RayDegreeToNormal;

            Vector3 DirectionForRaycast = new Vector3(-Mathf.Cos(Mathf.Deg2Rad * FinalAngle), Mathf.Sin(Mathf.Deg2Rad * FinalAngle)); //Negative x for reflection
            TestAngle = DirectionForRaycast;
            print(DirectionForRaycast + "Direction of raycast");
            return 100 * DirectionForRaycast;
        }
        private float CalculateRefractedAngle(float initialDegree)
        {
            float InitialDegree = 180 - initialDegree;
            float indexN = 1;
            float indexSurface = 1.5F;
            float calculate = (indexN / indexSurface) * Mathf.Sin(Mathf.Deg2Rad*InitialDegree);
            tempVar = Mathf.Rad2Deg * Mathf.Asin(calculate);
            return Mathf.Asin(calculate);
        }

        private Vector3 CalculateRefractedRay(float RefractedDegree)
        {
            //to get the angle between the refracted ray and principal axis
            print(AngleBetween(Vector3.right, RayHit.normal) + "Angle between normal, axis");
            print(-AngleBetween(Vector3.right, -RayHit.normal) + "---Angle between normal, axis");
            float angleToRenderer = RefractedDegree - (-AngleBetween(Vector3.right, -RayHit.normal));
            print(angleToRenderer + "angletorenderer");

            PreRefractedAngle = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleToRenderer), Mathf.Sin(Mathf.Deg2Rad * angleToRenderer));
            RefractedAngle = 100 * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleToRenderer), Mathf.Sin(Mathf.Deg2Rad * angleToRenderer));

            print(RefractedAngle + "ref angle");
            print(PreRefractedAngle + "Preref angle");

            return RefractedAngle;
        }

        public float AngleBetween(Vector3 From, Vector3 To)
        {
            float angle = Vector3.Angle(From, To);
            print(angle + "calculation inside AB");
            if (Vector3.Cross(From, To).z < 0)
            {
                angle *= -1;
            }

            return angle;
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(timeWait);
        }
    }
}
