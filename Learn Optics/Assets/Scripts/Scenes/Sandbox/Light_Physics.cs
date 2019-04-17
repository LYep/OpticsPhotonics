using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * This class is responsible for handling when light beams collide with optical elements (light, lenses, mirrors)
 * It uses small angle approximations to calculate the angles at which light is refracted. Refer to refraction at spherical
 * surfaces for the exact math (University Physics CH. 34-3)
 **/

public class Light_Physics : MonoBehaviour
{

    private RaycastHit OpticalElementHit;
    private Transform laser;
    private LineRenderer lightBeamRenderer;
    private float focal_length;
    private float n_Index;
    private float lightBeamWidth;
    private bool isReflective = false;
    private bool isCurved = true;
    private bool enableDebugLines;
    private float scalingFactor;
    int NumOpticalElements;
    GameObject ActiveLens;
    int CollisionCounts;
    Vector3 RayDirection;
    Slider IndexRefraction;
    Slider IndexRefractionLens;
    float NIndexRay;
    float NIndexLens;
    float AngleOfOriginalDirection;
    List<Vector3> VectorList;
    public void Start()
    {
        VectorList = new List<Vector3>();
        VectorList.Add(Vector3.right);
        ActiveLens = GameObject.Find("LensButton");
        lightBeamRenderer = GetComponent<LineRenderer>();
        lightBeamRenderer.enabled = true;
        lightBeamRenderer.useWorldSpace = false; //Sometimes this setting is enabled. This ensures that on start, it is disabled. Though, it should be disabled in the hierarchy.
        lightBeamWidth = 0.25F;

        lightBeamRenderer.startWidth = lightBeamWidth;
        lightBeamRenderer.endWidth = lightBeamWidth;
        enableDebugLines = false;
        //In case the RaycastHit fails to get optical properties, they are initialized here
        isReflective = false;
        isCurved = true;
        CollisionCounts = 0;
        scalingFactor = 100000000;
        //PreRefractedAngle = Vector3.right;
        IndexRefraction = GameObject.Find("NIndexSliderRay").GetComponent<Slider>();
        IndexRefractionLens = GameObject.Find("NIndexSlider").GetComponent<Slider>();
        NIndexRay = IndexRefraction.value;
        NIndexLens = IndexRefractionLens.value;
        bugAvoid();
    }

    public void Update()
    {
        //This must be set to a high number since LineRenderers draw lines between two points. If the point is too close and the user translates the light-emitter, it will appear as though
        //the focal point of the optical element is moving around.
        controls(); //compile specific
        if (lightBeamRenderer.enabled)
        {
            shootLightBeam();
        }
        DebugLines(enableDebugLines);
    }

    //Responsible for handling user input and moving/rotating around the light-beam emitter accordingly
    public void controls()
    {
        if (Input.GetKey(KeyCode.P))
        {
            focal_length += 0.1f;
        }
        if (Input.GetKey(KeyCode.O))
        {
            focal_length -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lightBeamRenderer.enabled = !lightBeamRenderer.enabled;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            enableDebugLines = !enableDebugLines;       //toggle debug lines
        }
    }
    //Used to avoid intialization bug
    void bugAvoid()
    {
        lightBeamRenderer.positionCount = 2;

        if (Physics.Raycast(transform.position, transform.right, out OpticalElementHit))
        {
            if (OpticalElementHit.collider.gameObject.GetComponent<Properties_Optical>().isReflective)
            {
                lightBeamRenderer.SetPosition(1, new Vector3(OpticalElementHit.distance, 0, 0));
                lightBeamRenderer.SetPosition(2, calculateReflectedDirection());
            }
            else
            {
                lightBeamRenderer.SetPosition(1, new Vector3(OpticalElementHit.distance, 0, 0));
                lightBeamRenderer.SetPosition(2, calculateDirection());
            }
        }
        else
        {
            lightBeamRenderer.SetPosition(1, new Vector3(1000, 0, 0));
        }
    }
    public void shootLightBeam()
    {
        NumOpticalElements = ActiveLens.GetComponent<ActivateLens>().LensCounter;

        if (Physics.Raycast(transform.position, transform.right, out OpticalElementHit) == false)
        {
            lightBeamRenderer.positionCount = 2;
            lightBeamRenderer.SetPosition(1, new Vector3(1000, 0, 0)); 
        }
        else
        {
            //Get optical properties from object hit
            if (OpticalElementHit.collider != null)
            {
                isReflective = OpticalElementHit.transform.gameObject.GetComponent<Properties_Optical>().isReflective;
                isCurved = OpticalElementHit.transform.gameObject.GetComponent<Properties_Optical>().isCurved;
                focal_length = OpticalElementHit.transform.gameObject.GetComponent<Properties_Optical>().focal_length;
                n_Index = OpticalElementHit.transform.gameObject.GetComponent<Properties_Optical>().n_Index;
                NIndexRay = IndexRefraction.value;
                NIndexLens = IndexRefractionLens.value;
            }
            if (NumOpticalElements < 2)
            {
                lightBeamRenderer.positionCount = 3;
                if (!isReflective)
                {
                    if (Physics.Raycast(transform.position, transform.right, out OpticalElementHit))
                    {
                        //Sets positions of the vertexes of the line renderer (lightBeamRenderer)
                        lightBeamRenderer.SetPosition(1, new Vector3(OpticalElementHit.distance, 0, 0));
                        lightBeamRenderer.SetPosition(2, calculateDirection());
                       /* CollisionCounts = 1;
                        Ray ray = new Ray(OpticalElementHit.point, CalculateAngleSnell());

                        lightBeamRenderer.SetPosition(2, lightBeamRenderer.transform.InverseTransformPoint(ray.GetPoint(50)));   */
                        //Debug.DrawRay(OpticalElementHit.point, 1000 * lightBeamRenderer.transform.TransformDirection(calculateDirection()), Color.blue);
                    }
                }

                else if (isReflective)
                {
                    if (Physics.Raycast(transform.position, transform.right, out OpticalElementHit))
                    {
                        lightBeamRenderer.SetPosition(1, new Vector3(OpticalElementHit.distance, 0, 0));
                        lightBeamRenderer.SetPosition(2, calculateReflectedDirection());
                    }
                   /* else
                    {
                        lightBeamRenderer.SetPosition(1, new Vector3(1000, 0, 0));
                    }*/
                }
            }
            else
            {
                CollisionCounts = 1;
                MultipleTrace();
            }
        }
    }
    private Vector3 CalculateAngleSnellMirror()
    {
        Vector3 PreRefractedAngle = VectorList[CollisionCounts -1];
        float RayDegreeToNormal = Vector3.Angle(PreRefractedAngle, OpticalElementHit.normal);
        print("Angle between ray and optical hit normal " + RayDegreeToNormal);
        float IndexAir = 1;
        float IndexLens = 1.5f;

        //Going from lens to air, not air to lens, so divide lens by air
        float CalculateRefraction = (IndexLens / IndexAir) * Mathf.Sin(Mathf.Deg2Rad * RayDegreeToNormal);
        float RefractedAngleToNormal = Mathf.Rad2Deg * Mathf.Asin(CalculateRefraction);
        print("angle of refracted angle " + RefractedAngleToNormal);

        Vector3 DirectionFromNormal = new Vector3(Mathf.Cos(Mathf.Deg2Rad * RefractedAngleToNormal), Mathf.Sin(Mathf.Deg2Rad * RefractedAngleToNormal));
        float AngleBetweenNormalAndAxis = AngleBetween(OpticalElementHit.normal, Vector3.right);
        print(AngleBetweenNormalAndAxis + "angle of normal and axis");

        float FinalAngle = RefractedAngleToNormal - AngleBetweenNormalAndAxis;
        print(FinalAngle + "the final angle");

        Vector3 DirectionForRaycast = new Vector3(Mathf.Cos(Mathf.Deg2Rad * FinalAngle), Mathf.Sin(Mathf.Deg2Rad * FinalAngle));
        VectorList.Insert(CollisionCounts, DirectionForRaycast);
        print(DirectionForRaycast + "direction of raycast");

        return 100 * DirectionForRaycast;
    }
    private Vector3 CalculateAngleSnell()
    {
        //This works perfect but there is no thin lens assumption, will be used for ray tracing on both surfaces of lens
        print(lightBeamRenderer.transform.TransformPoint(lightBeamRenderer.GetPosition(CollisionCounts - 1)) + "Pos in space");
        print(OpticalElementHit.point + "Pos in space point");

        Vector3 CalculatedPreAngle = OpticalElementHit.point - lightBeamRenderer.transform.TransformPoint(lightBeamRenderer.GetPosition(CollisionCounts - 1));
  
        float RayDegreeToAxis = Vector3.Angle(CalculatedPreAngle, Vector3.right);
        print("Ray to axis " + RayDegreeToAxis);
        float RayDegreeToNormal = Vector3.Angle(CalculatedPreAngle, OpticalElementHit.normal);
        print("Angle between ray and optical hit normal " + RayDegreeToNormal);
        //float IndexAir = NIndexRay;
        float IndexAir = 1;
        float IndexLens = 2.5f;

        float CalculateRefraction = (IndexAir / IndexLens) * Mathf.Sin(Mathf.Deg2Rad * RayDegreeToNormal);
        float RefractedAngleToNormal = Mathf.Rad2Deg * Mathf.Asin(CalculateRefraction);
        print("angle of refracted angle " + RefractedAngleToNormal);

        Vector3 DirectionFromNormal = new Vector3(Mathf.Cos(Mathf.Deg2Rad * RefractedAngleToNormal), Mathf.Sin(Mathf.Deg2Rad * RefractedAngleToNormal));
        float AngleBetweenNormalAndAxis = Vector3.Angle(OpticalElementHit.normal, Vector3.right);
        print(AngleBetweenNormalAndAxis + "angle of normal and axis");

        float NormalizedAngleOfNormal = 180 - AngleBetweenNormalAndAxis;
        float FinalAngle = 0;

        if (Vector3.Cross(CalculatedPreAngle, OpticalElementHit.normal).z < 0)
        {
            //Ray above Normal
            if (Vector3.Cross(OpticalElementHit.normal, Vector3.right).z < 0) //Normal Below X axis
                FinalAngle = -(RefractedAngleToNormal + NormalizedAngleOfNormal);
            else
                FinalAngle = NormalizedAngleOfNormal - RefractedAngleToNormal;
        }
        else
        {
            //Ray below Normal
            if (Vector3.Cross(OpticalElementHit.normal, Vector3.right).z < 0) //Normal Below X axis
                FinalAngle = RefractedAngleToNormal - NormalizedAngleOfNormal;
            else
                FinalAngle = RefractedAngleToNormal + NormalizedAngleOfNormal;
        }

        print(FinalAngle + "the final angle");

        Vector3 DirectionForRaycast = new Vector3(Mathf.Cos(Mathf.Deg2Rad * FinalAngle), Mathf.Sin(Mathf.Deg2Rad * FinalAngle));
        VectorList.Insert(CollisionCounts, DirectionForRaycast);
        return 100 * DirectionForRaycast;
    }
    private void MultipleTrace()
    {
        Vector3 PreCalculatedDirection = OpticalElementHit.point - lightBeamRenderer.transform.TransformPoint(lightBeamRenderer.GetPosition(CollisionCounts - 1));
        Vector3 PrePosition = lightBeamRenderer.transform.TransformPoint(lightBeamRenderer.GetPosition(CollisionCounts - 1));
        AngleOfOriginalDirection = AngleBetween(Vector3.right, PreCalculatedDirection);

        while (Physics.Raycast(PrePosition, PreCalculatedDirection, out OpticalElementHit) && CollisionCounts <= NumOpticalElements)
        {
            lightBeamRenderer.SetPosition(CollisionCounts, lightBeamRenderer.transform.InverseTransformPoint(OpticalElementHit.point));
            Vector3 RefractedThinDirection;
            if (!OpticalElementHit.collider.transform.gameObject.GetComponent<Properties_Optical>().isReflective)
                RefractedThinDirection = calculateDirection();
            else
                RefractedThinDirection = calculateReflectedDirection();

            PrePosition = OpticalElementHit.point + new Vector3(0.5f, 0, 0);
            PreCalculatedDirection = lightBeamRenderer.transform.TransformDirection(RefractedThinDirection); //transform LE direction to world direction

            CollisionCounts++; //going from 1 to 2, we are still colliding with 1 element
            lightBeamRenderer.positionCount = CollisionCounts + 1;
        }
        /*Ray ray = new Ray(OpticalElementHit.point, PreCalculatedDirection);
        lightBeamRenderer.SetPosition(CollisionCounts, lightBeamRenderer.transform.InverseTransformPoint(ray.GetPoint(50)));*/
        lightBeamRenderer.SetPosition(CollisionCounts, 100* lightBeamRenderer.transform.InverseTransformDirection(PreCalculatedDirection));
    }

    //Calculates the direction taken by the refracted beam of light
    //Refer to diagram in Readme folder for explanation on variable nomenclature
    public Vector3 calculateReflectedDirection()
    {
        Vector3 traj_in;
        if (NumOpticalElements > 1)
            traj_in = OpticalElementHit.point - transform.TransformPoint(lightBeamRenderer.GetPosition(CollisionCounts - 1));
        else
            traj_in = OpticalElementHit.point - transform.position;

        float angle_incident = AngleBetween(traj_in, -OpticalElementHit.normal);
        float angle_reflected = 2 * angle_incident;
        if (NumOpticalElements > 1)
        {
            //take previous refraction and original angle and cancel it out
            float InitialAngle = AngleBetween(traj_in, Vector3.right);
            angle_reflected = angle_reflected + InitialAngle + AngleOfOriginalDirection;
        }
        RayDirection = scalingFactor / 100000 * new Vector3(-Mathf.Cos(Deg2Rad(angle_reflected)), -Mathf.Sin(Deg2Rad(angle_reflected)));
        //Debug.DrawRay(transform.TransformPoint(lightBeamRenderer.GetPosition(1)), 100 * (lightBeamRenderer.transform.TransformDirection(RayDirection)), Color.black);
        return RayDirection;
    }
    public Vector3 calculateDirection()
    {
        Vector3 traj_in;
        if (NumOpticalElements > 1)
            traj_in = OpticalElementHit.point - lightBeamRenderer.transform.TransformPoint(lightBeamRenderer.GetPosition(CollisionCounts - 1));
        else
            traj_in = OpticalElementHit.point - transform.position;

        if (OpticalElementHit.collider.gameObject.GetComponent<Properties_Optical>().isCurved)
        {

            float angle_alpha = -AngleBetween(traj_in, Vector3.right);

            float angle_a = -AngleBetween(traj_in, -OpticalElementHit.normal);
           // float angle_aa = AngleBetween(traj_in, OpticalElementHit.normal); 

            float angle_phi = AngleBetween(-OpticalElementHit.normal, Vector3.right);
            float angle_btemp = (NIndexRay / NIndexLens) * Mathf.Sin(Deg2Rad(angle_a)); 
            if (angle_btemp > 1)
                angle_btemp = 1;
            if (angle_btemp < -1)
                angle_btemp = -1;
            float angle_b = Mathf.Asin(angle_btemp); //this is returning radians, not degrees
           
            // float angle_bb = Mathf.Asin((NIndexRay / n_Index) * Mathf.Sin(Deg2Rad(angle_aa)));

            float angle_beta = angle_phi - 6*(angle_b);  //This is subtracting degrees with radians

            if (NumOpticalElements > 1)
            {
                //take previous refraction and original angle and cancel it out
                float InitialAngle = AngleBetween(traj_in, Vector3.right);
                angle_beta = angle_beta + InitialAngle + AngleOfOriginalDirection;        
            }

            //float angle_betaa = angle_phi - angle_bb;
            RayDirection = scalingFactor / 100000 * new Vector3(Mathf.Cos(Deg2Rad(angle_beta)), -Mathf.Sin(Deg2Rad(angle_beta)));
        }

        else
        {
            float angle_a = -AngleBetween(traj_in, -OpticalElementHit.normal);
            float angle_beta = Mathf.Asin(1 / n_Index * Mathf.Sin(Deg2Rad(angle_a)));

            //Asin is returning rad, not degrees!!!
            RayDirection = scalingFactor * new Vector3(Mathf.Cos(angle_beta), -Mathf.Sin(angle_beta));
        }
        return RayDirection;
    }

    public float Deg2Rad(float d)
    {
        return d * Mathf.Deg2Rad;
    }

    // Finds the angle between two vectors. Determines the sign via cross product.
    // If Vector3 From is above Vector3 To, the angle is positive and vice versa
    public float AngleBetween(Vector3 From, Vector3 To)
    {
        float angle = Vector3.Angle(From, To);
        if (Vector3.Cross(From, To).z < 0)
        {
            angle *= -1;
        }
        return angle;
    }

    //Creates various axes and lines that are useful for debugging. I used these more to make sure that my math made sense.
    public void DebugLines(bool isEnabled)
    {
        if (isEnabled)
        {
            //x-axis at hit point
            Debug.DrawRay(OpticalElementHit.point, 1000 * Vector3.right, Color.grey);
            Debug.DrawRay(OpticalElementHit.point, 1000 * Vector3.left, Color.grey);

            //normal line to hit point (passes through center of curve)
            Debug.DrawRay(OpticalElementHit.point, 1000 * OpticalElementHit.normal, Color.black);
            Debug.DrawRay(OpticalElementHit.point, 1000 * -OpticalElementHit.normal, Color.black);

            //x-axis at gameobject position
            Debug.DrawRay(transform.position, 1000 * Vector3.right, Color.gray);
            Debug.DrawRay(transform.position, 1000 * Vector3.left, Color.gray);
        }
    }


    //Old Broken Function
   /* void ProcessCast()
    {
        //Instead of depending on one line renderer, utilize a new ALR per ray hit
        //Allows easier computing at the cost of more objects
        lightBeamRenderer.positionCount = NumOpticalElements + 2;
        count = 1;
        dir = transform.position;
        dir2 = transform.right;
        while (Physics.Raycast(dir, dir2, out OpticalElementHit) && count <= NumOpticalElements)
        {
            // Debug.DrawRay(OpticalElementHit.point, 1000 * (dir2), Color.blue);
            if (count == 1)
            {
                lightBeamRenderer.SetPosition(1, new Vector3(OpticalElementHit.distance, 0, 0));
            }
            else
            {
                lightBeamRenderer.SetPosition(count, lightBeamRenderer.transform.InverseTransformPoint(OpticalElementHit.point));
                //print(OpticalElementHit.point + "point of second hit");
            }

            //print(OpticalElementHit.collider.name + "name of collider");
            if (OpticalElementHit.collider.gameObject.GetComponent<Properties_Optical>().isReflective)
            {
                RayDirection = (calculateReflectedDirection());
                dir2 = RayDirection;
            }

            else
            {
                RayDirection = calculateDirection();
                dir2 = lightBeamRenderer.transform.TransformDirection(RayDirection);
            }
            //adding 0.5 to x to ensure raycast starts within obj
            dir = OpticalElementHit.point + new Vector3(0.5F, 0, 0);
            // Debug.DrawRay(OpticalElementHit.point, 1000*(dir2), Color.green);

            count++;
            finalC = count;
        }
        //set final vertex to direction of ray after no more impacts
        lightBeamRenderer.SetPosition(finalC, (RayDirection));
        lightBeamRenderer.positionCount = count + 1;
        //Debug.DrawRay( , 1000 * lightBeamRenderer.transform.TransformDirection(dir2), Color.green);
    }*/
}
