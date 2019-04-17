using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementCamera : MonoBehaviour {
    private static readonly float PanSpeed = 1.92f;
    private static readonly float ZoomSpeedMouse = 15f;

    private static readonly float[] BoundsX = new float[] { -220f, -100f };
    private static readonly float[] BoundsY = new float[] { -70f, 25f };
    private static readonly float[] ZoomBounds = new float[] { 1f, 35f };
    private bool Checked;

    private Camera cam;

    private Vector3 lastPanPosition;

    private int panFingerId; // Rest for Touch Only
    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;
    private readonly float ZoomSpeed = 0.08f;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        #if UNITY_IOS || UNITY_ANDROID 
            HandleTouch();
        #else
            HandleMouse();
        #endif
    }

    void HandleTouch()
    {
        print("movin Mob");
        if (Input.touchCount == 1)
        {
            // Panning
            wasZoomingLastFrame = false;

            // If the touch began, capture its position and its finger ID.
            // Otherwise, if the finger ID of the touch doesn't match, skip it.
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                print("not over ui");
                lastPanPosition = touch.position;
                panFingerId = touch.fingerId;
                Checked = true;
            }
            else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved && Checked)
            {
                print("entered");
                PanCamera(touch.position);
            }
            /*else if(touch.fingerId == panFingerId && touch.phase == TouchPhase.Ended)
            {
                Checked = false;
            }*/

        }
        else if (Input.touchCount == 2)
        {
            // Zooming
            Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
            if (!wasZoomingLastFrame)
            {
                lastZoomPositions = newPositions;
                wasZoomingLastFrame = true;
            }
            else
            {
                Touch FirstTouch = Input.GetTouch(0);
                Touch SecondTouch = Input.GetTouch(1);

                Vector2 FirstTouchPrevPos = FirstTouch.position - FirstTouch.deltaPosition;
                Vector2 SecondTouchPrevPos = SecondTouch.position - SecondTouch.deltaPosition;

                float PrevDeltaMag = (FirstTouchPrevPos - SecondTouchPrevPos).magnitude;
                float TouchDeltaMag = (FirstTouch.position - SecondTouch.position).magnitude;

                float DeltaMagDiff = PrevDeltaMag - TouchDeltaMag;

                cam.orthographicSize += DeltaMagDiff * ZoomSpeed;
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, 1);
                cam.orthographicSize = Mathf.Min(cam.orthographicSize, 30);
            }
        }
        else
        {
            wasZoomingLastFrame = false;
            Checked = false;
        }        
    }

    void HandleMouse()
    {
        print("movin PC");
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed *cam.orthographicSize, offset.y * PanSpeed *cam.orthographicSize, 0);//Change with ortho size
        
        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.y = Mathf.Clamp(transform.position.y, BoundsY[0], BoundsY[1]);
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }    
}