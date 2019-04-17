﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.AnimatedLineRenderer
{
    public class ImageArrowGeneration : MonoBehaviour
    {

        //Thin lens approximation of the object-image relationship: 1/do + 1/di = 1/f
        //Where do is object distance, di is image distance, and f is the focal length of the lens. An arbitrary length will be picked for the purposes of this learning module.

        public float FocalLength;
        public float ObjectDistance;
        public float ImageDistance;
        public float Magnification;
        public bool isConcave;
        public bool isReflective;
        public bool isConcaveReflective;
        public bool isConvexReflective;
        float SpriteBounds;
        private Vector3 InitialScale;
        private SpriteRenderer spriteRenderer;
        GameObject ObjectArrow;
        GameObject OpticalElement;
        bool Quizzing;
        bool isSelected;
        Vector3 origin;

        void Start()
        {
            InitializeObjects();
            
        }

        //Note that all distances are relative to the lens, which is located at Root.transform.position
        void Update()
        {
            //Keep this in update because in the learn mirrors scene, the optical element will change at runtime, so these need to be updated.
            ObjectArrow = GameObject.Find("ObjectArrow");
            OpticalElement = GameObject.FindGameObjectWithTag("OpticalElement");

            //If we have a convex mirror or a concave lens, focal length is negative
            if (OpticalElement.GetComponent<Properties_Optical>().isConvexReflective || (!OpticalElement.GetComponent<Properties_Optical>().isReflective && OpticalElement.GetComponent<Properties_Optical>().isConcave))
            {
                FocalLength = -Mathf.Abs(GameObject.Find("F1").transform.position.x - OpticalElement.transform.position.x);

            }
            else
            {
                FocalLength = Mathf.Abs(GameObject.Find("F1").transform.position.x - OpticalElement.transform.position.x);
            }

            isConcaveReflective = GameObject.FindGameObjectWithTag("OpticalElement").GetComponent<Properties_Optical>().isConcaveReflective;
            isConvexReflective = GameObject.FindGameObjectWithTag("OpticalElement").GetComponent<Properties_Optical>().isConvexReflective;

        }
        private void checkSelected()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider && Input.touchCount >= 1)
            {
                isSelected = true;

            }
            else
            {
                isSelected = false;
            }
        }

        public void ResetALR()
        {
            ObjectArrow.GetComponent<ObjectArrowControls>().ResetALRs();
        }

        public void CalculatePosition()
        {
            ObjectArrow = GameObject.Find("ObjectArrow");
            OpticalElement = GameObject.FindGameObjectWithTag("OpticalElement");
            ObjectDistance = OpticalElement.transform.position.x - ObjectArrow.transform.position.x;
            if (isConcave)
            {
                FocalLength = -1 * Mathf.Abs(FocalLength);
            }

            ImageDistance = Mathf.Abs(1 / (1 / ObjectDistance - 1 / FocalLength));
            Magnification = InitialScale.y * Mathf.Abs(ImageDistance / ObjectDistance);     
            transform.localScale = new Vector3(InitialScale.x, Magnification, InitialScale.z);
        }

        public void SetPosition()
        {
            //if mirror
            if(isReflective)
            {
                //If concave mirror
                if (isConcaveReflective)
                {
                    spriteRenderer.flipY = true;
                    transform.position = new Vector3(OpticalElement.transform.position.x - ImageDistance, OpticalElement.transform.position.y - Magnification * 1.89F);
                    //If Object is inside focal length of mirror
                    if (Mathf.Abs(ObjectArrow.transform.position.x - OpticalElement.transform.position.x) < 15)
                    {
                        spriteRenderer.flipY = false;
                        transform.position = new Vector3(OpticalElement.transform.position.x + ImageDistance, OpticalElement.transform.position.y + Magnification * 1.89F);
                    }
                }

                //if convex mirror
                else if (isConvexReflective)
                {
                    spriteRenderer.flipY = false;
                    transform.position = new Vector3(OpticalElement.transform.position.x + ImageDistance, OpticalElement.transform.position.y + Magnification * 1.89F);
                }
                
                //if plane mirror
                else
                {
                    transform.position = new Vector3(OpticalElement.transform.position.x + Mathf.Abs(ObjectArrow.transform.position.x - OpticalElement.transform.position.x), OpticalElement.transform.position.y + 4.84F);
                    transform.localScale = InitialScale;
                }
            }
            if (!isReflective)
            {
                if (isConcave)
                {
                    spriteRenderer.flipY = false;
                    transform.position = new Vector3(OpticalElement.transform.position.x - ImageDistance, OpticalElement.transform.position.y + Magnification * 2);
                }
                else
                {
                    print(FocalLength + " focal length");
                    print(ObjectDistance + "object distance");
                    if (ObjectDistance < FocalLength)
                    {
                        print("entered");
                        spriteRenderer.flipY = false;
                        transform.position = new Vector3(GameObject.Find("Root").transform.position.x - (ImageDistance), GameObject.Find("Root").transform.position.y + Magnification * 2, 0);
                    }
                    else
                    {
                        print("entered2");
                        spriteRenderer.flipY = true;
                        transform.position = new Vector3(GameObject.Find("Root").transform.position.x + (ImageDistance), GameObject.Find("Root").transform.position.y - Magnification * 2, 0);

                    }
                }
            }
        }

        public Vector3 GetPosition()
        {
            if (isConcave)
            {
                spriteRenderer.flipY = false;
                return new Vector3(OpticalElement.transform.position.x - ImageDistance, OpticalElement.transform.position.y + Magnification * 2);
            }
            else
            {

                if (ObjectDistance < FocalLength)
                {
                    spriteRenderer.flipY = false;
                    return new Vector3(GameObject.Find("Root").transform.position.x - ImageDistance, GameObject.Find("Root").transform.position.y + Magnification * 2, 0);
                }
                else
                {
                    spriteRenderer.flipY = true;
                    return new Vector3(GameObject.Find("Root").transform.position.x + ImageDistance, GameObject.Find("Root").transform.position.y - Magnification * 2, 0);
                }
            }

        }

        public Vector3 GetScale()
        {
            return new Vector3(InitialScale.x, Magnification, InitialScale.z);
            ;
        }

        public void InitializeObjects()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Magnification = 1;
            InitialScale = transform.localScale;
            SpriteBounds = spriteRenderer.bounds.size.y;
            origin = Camera.main.gameObject.transform.position;
            OpticalElement = GameObject.FindGameObjectWithTag("OpticalElement");
            Quizzing = false;

        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}


