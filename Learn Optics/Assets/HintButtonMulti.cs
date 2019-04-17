using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.AnimatedLineRenderer
{
    public class HintButtonMulti : MonoBehaviour
    {
        GameObject GenerateQuizButton;
        private Animator PopUpPromptAnimator;
        private Animator PopUpPromptTextAnimator;
        private Animator PopUpButtonLeft;
        private Animator PopUpButtonRight;
        Button AwakeButton;
        private Text PopUpPromptText;
        public int counter;
        private bool isUICoroutineExecuting;
        private float MessageDuration;
        string CheckCase;
        int CounterLens;
        int TextLength;
        int currentText;
        string[] ArrayText;
        float CheckTime;
        float TimeToEnd;
        bool DoneText;

        void Start()
        {
            GenerateQuizButton = GameObject.Find("GenerateQuizButton");
            CheckCase = GenerateQuizButton.GetComponent<GenerateQuizMulti>().Cases;
            CounterLens = GenerateQuizButton.GetComponent<GenerateQuizMulti>().CounterSurface;
            PopUpPromptAnimator = GameObject.Find("HintPrompt").GetComponent<Animator>();
            PopUpPromptTextAnimator = PopUpPromptAnimator.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            PopUpButtonLeft = PopUpPromptAnimator.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>();
            PopUpButtonRight = PopUpPromptAnimator.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
            PopUpPromptText = PopUpPromptAnimator.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            AwakeButton = PopUpPromptAnimator.gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
            MessageDuration = 5;
            counter = 0;
            TextLength = 0;
            currentText = 0;
            ArrayText = new string[3];
            CheckTime = 0;
            DoneText = false;
        }
        private void Update()
        {
            if(CheckTime != 0 && (Time.time - CheckTime > TimeToEnd) && !DoneText)
            {
                print("ended" + TimeToEnd);
                PopUpPromptAnimator.SetBool("DisplayPrompt", false);
                PopUpPromptTextAnimator.SetBool("DisplayPrompt", false);
                AwakeButton.enabled = false;
                DoneText = true;
                if (TextLength >= 2)
                {
                    PopUpButtonLeft.SetBool("PopIn", false);
                    PopUpButtonRight.SetBool("PopIn", false);                                       
                }
            }
        }
        public void OnClickAwake()
        {
            CheckTime = Time.time;
            print("clicked");

        }
        public void OnClickArrowLeft()
        {            
            if (currentText > 0)
                currentText--;
            if (TextLength == 3)
            {
                if (currentText == 1)
                {
                    PopUpButtonLeft.SetBool("PopIn", true);
                    PopUpButtonRight.SetBool("PopIn", true);
                }
                else if (currentText == 0)
                {
                    PopUpButtonLeft.SetTrigger("PopOut");
                    PopUpButtonLeft.SetBool("PopIn", false);
                    PopUpButtonRight.SetTrigger("PopIn");
                }
            }
            else if (TextLength == 2)
            {
                if (currentText == 0)
                {
                    PopUpButtonLeft.SetBool("PopIn", false);
                    PopUpButtonLeft.SetTrigger("PopOut");
                    PopUpButtonRight.SetBool("PopIn", true);
                }
            }
            PopUpPromptText.text = ArrayText[currentText];
            CheckTime = Time.time;
        }
        public void OnClickArrowRight()
        {
            if (currentText < TextLength - 1)//1 -> 2 (3)
            {
                currentText++;
            }
            if (TextLength == 3)
            {               
                if (currentText == 2)
                {
                    PopUpButtonLeft.SetBool("PopIn", true);
                    PopUpButtonRight.SetTrigger("PopOut");
                    PopUpButtonRight.SetBool("PopIn", false);
                }
                else if (currentText == 1)
                {
                    PopUpButtonLeft.SetBool("PopIn", true);
                    PopUpButtonRight.SetBool("PopIn", true);
                }
            }
            else if(TextLength == 2)
            {
                if(currentText == 1)
                {
                    PopUpButtonLeft.SetBool("PopIn", true);
                    PopUpButtonRight.SetTrigger("PopOut");
                    PopUpButtonRight.SetBool("PopIn", false);
                }
            }
            PopUpPromptText.text = ArrayText[currentText];
            CheckTime = Time.time;
        }
        public void OnClick()
        {   CheckCase = GenerateQuizButton.GetComponent<GenerateQuizMulti>().Cases;
            CounterLens = GenerateQuizButton.GetComponent<GenerateQuizMulti>().CounterSurface;
            CheckTime = Time.time;
            AwakeButton.enabled = true;
            DoneText = false;
            string TextSend = "";
            switch (CheckCase)
            {               
                case "Start":
                    GenerateQuizButton.GetComponent<GenerateQuizMulti>().CalculateAnswer();
                    if (counter >= 4)
                    {
                        string answer = GenerateQuizButton.GetComponent<GenerateQuizMulti>().TrueAnswerHeight.ToString("0.00mm");
                        TextSend = "The answer is " + answer;
                        ArrayText[0] = TextSend;
                        TextLength = 1;
                        TimeToEnd = 5;
                        //DisplayPrompt(TextSend, MessageDuration);
                    }
                    else
                    {
                        if (counter == 3 || counter == 2)
                        {
                            string Distance = GenerateQuizButton.GetComponent<GenerateQuizMulti>().DistanceOfContact.ToString("0.00mm");
                            string Height = GenerateQuizButton.GetComponent<GenerateQuizMulti>().InitialHeight.ToString("0.00mm");
                            string Slope = GenerateQuizButton.GetComponent<GenerateQuizMulti>().RoundAngle.ToString("0.000");
                            TextSend = "Multiply the slope: " + Slope + " with the Distance: " + Distance + " and add the initial Height: " + Height;
                            ArrayText[0] = TextSend;
                            TextLength = 1;
                            TimeToEnd = 7;
                            //DisplayPrompt(TextSend, 10);
                        }
                        else
                        {
                            TextSend = "Take the distance from the start of the ray up to the point of contact with the lens and multiply it with the ray's ";
                            string TextExtra = "incident angle. Finally add the incident height.";
                            ArrayText[0] = TextSend;
                            ArrayText[1] = TextExtra;
                            TextLength = 2;
                            TimeToEnd = 10;
                            //DisplayPrompt("Take the distance from the start of the ray up to the point of contact with the lens and multiply it with the ray's incident angle." +
                            //         " Finally add the incident height.", 10);
                        }
                    }
                    break;

                 case "IsHeight":
                    GenerateQuizButton.GetComponent<GenerateQuizMulti>().CalculateAnswer();
                    if (counter >= 4)
                    {
                        string answer = GenerateQuizButton.GetComponent<GenerateQuizMulti>().TrueAnswerHeight.ToString("0.00mm");
                        TextSend = "The answer is " + answer;
                        ArrayText[0] = TextSend;
                        TextLength = 1;
                        TimeToEnd = 5;
                        //DisplayPrompt(TextSend, MessageDuration);
                    }
                    else
                    {
                        if (counter < 2)
                        {
                            TextSend = "Take the distance from the previous point of the ray and the current point and multiply it with ";
                            string TextExtra = "the the incident angle (slope). Then add the previous height to get the new height.";
                            ArrayText[0] = TextSend;
                            ArrayText[1] = TextExtra;
                            TextLength = 2;
                            TimeToEnd = 10;
                            // DisplayPrompt("Take the distance from the previous point of the ray and the current point and multiply it with the the incident angle (slope). " +
                            //           "Then add the previous height to get the new height.", 5);
                        }
                        else
                        {
                            string Distance = GenerateQuizButton.GetComponent<GenerateQuizMulti>().DistanceOfContact.ToString("0.00mm");
                            string Height = GenerateQuizButton.GetComponent<GenerateQuizMulti>().InitialHeight.ToString("0.00mm");
                            string Slope = GenerateQuizButton.GetComponent<GenerateQuizMulti>().RoundAngle.ToString("0.000");
                            TextSend = "Multiply the slope: " + Slope + " with the Distance: " + Distance + " and add the initial Height: " + Height;
                            ArrayText[0] = TextSend;
                            TextLength = 1;
                            TimeToEnd = 7;
                            //DisplayPrompt(TextSend, 5);
                        }
                    }
                    break;

                case "IsSlope":
                    GenerateQuizButton.GetComponent<GenerateQuizMulti>().CalculateAnswer();
                    if (counter >= 4)
                    {
                        string answer = GenerateQuizButton.GetComponent<GenerateQuizMulti>().TrueAnswerSlope.ToString("0.000");
                        TextSend = "The answer is " + answer;
                        ArrayText[0] = TextSend;
                        TextLength = 1;
                        TimeToEnd = 5;
                        // DisplayPrompt(TextSend, MessageDuration);
                    }
                    else
                    {
                        //0 Ray at leftside of lens, 1 at right side
                        switch (CounterLens)
                        {
                            case 0:
                                if (counter < 2)
                                {
                                    TextSend = "Take the current slope(Ui) and multiply it with the index of refraction of the air(Ni). Multiply the";
                                    string TextExtra = "transferred height of the ray(Yi) with the surface power of the lens then subtract from the previous calculation.";
                                    string TextExtra1 = "Surface power is the index of refraction of the lens(Nt) minus Ni divided by the radius.";
                                    ArrayText[0] = TextSend;
                                    ArrayText[1] = TextExtra;
                                    ArrayText[2] = TextExtra1;
                                    TextLength = 3;
                                    TimeToEnd = 10;
                                    //DisplayPrompt("Take the current slope(Ui) and multiply it with the index of refraction of the air (Ni). Multiply the transferred height of the ray (Yi) " +
                                    //"with the surface power of the lens then subtract from the previous calculation. Surface power is the index of refraction of the lens(Nt) minus Ni divided by the radius.", 5);
                                }
                            
                                else
                                {
                                    string IndexAir = GenerateQuizButton.GetComponent<GenerateQuizMulti>().IndexAir.ToString("0.00");
                                    string Angle = GenerateQuizButton.GetComponent<GenerateQuizMulti>().RoundAngle.ToString("0.000");
                                    string InitialHeight = GenerateQuizButton.GetComponent<GenerateQuizMulti>().TransferHeight.ToString("0.00mm");
                                    string IndexLens = GenerateQuizButton.GetComponent<GenerateQuizMulti>().IndexMaterial.ToString("0.00");
                                    string Radius = (GenerateQuizButton.GetComponent<GenerateQuizMulti>().Radius * GenerateQuizButton.GetComponent<GenerateQuizMulti>().SignConvention()).ToString("00.00mm");
                                    TextSend = "Multiply the index of refraction of air: " + IndexAir + " with the incident angle: " + Angle + " and subtract the ";
                                    string TextExtra = "initial Height: " + InitialHeight + " multiplied by the surface power (N2 - N1)/R which is (" + IndexLens + "-" + IndexAir + ")/" + Radius;
                                    ArrayText[0] = TextSend;
                                    ArrayText[1] = TextExtra;
                                    TextLength = 2;
                                    TimeToEnd = 7;
                                    //DisplayPrompt(TextSend, 5);
                                }
                        break;

                        case 1:
                                if (counter < 2)
                                {
                                    TextSend = "Take the current slope(Ui) and multiply it with the index of refraction of the lens (Ni). Multiply the";
                                    string TextExtra = "transferred height of the ray (Yi) with the surface power of the lens then subtract from the previous calculation.";
                                    string TextExtra1 = "Surface power is the index of refraction of the air(Nt) minus Ni divided by the radius.";
                                    ArrayText[0] = TextSend;
                                    ArrayText[1] = TextExtra;
                                    ArrayText[2] = TextExtra1;
                                    TextLength = 3;
                                    TimeToEnd = 10;
                                    //DisplayPrompt("Take the current slope(Ui) and multiply it with the index of refraction of the lens (Ni). Multiply the transferred height of the ray (Yi) " +
                                    //   "with the surface power of the lens then subtract from the previous calculation. Surface power is the index of refraction of the air(Nt) minus Ni divided by the radius.", 5);
                                }
                                else
                                {
                                    string IndexAir = GenerateQuizButton.GetComponent<GenerateQuizMulti>().IndexAir.ToString("0.00");
                                    string Angle = GenerateQuizButton.GetComponent<GenerateQuizMulti>().RoundAngle.ToString("0.000");
                                    string InitialHeight = GenerateQuizButton.GetComponent<GenerateQuizMulti>().TransferHeight.ToString("0.00mm");
                                    string IndexLens = GenerateQuizButton.GetComponent<GenerateQuizMulti>().IndexMaterial.ToString("0.00");
                                    string Radius = (GenerateQuizButton.GetComponent<GenerateQuizMulti>().Radius * GenerateQuizButton.GetComponent<GenerateQuizMulti>().SignConvention()).ToString("00.00mm");
                                    TextSend = "Multiply the index of refraction of lens: " + IndexLens + " with the incident angle: " + Angle + " and subtract the";
                                    string TextExtra = "initial Height: " + InitialHeight + " multiplied by the surface power (N2 - N1)/R which is (" + IndexAir + "-" + IndexLens + ")/" + Radius;
                                    ArrayText[0] = TextSend;
                                    ArrayText[1] = TextExtra;
                                    TextLength = 2;
                                    TimeToEnd = 7;
                                    //DisplayPrompt(TextSend, 5);
                                }
                                break;
                        }
                    }
                    break;
            }
            PopUpPromptAnimator.SetBool("DisplayPrompt", true);
            PopUpPromptTextAnimator.SetBool("DisplayPrompt", true);

            if (TextLength >= 2)
            {
                PopUpButtonLeft.Play("Idle");
                PopUpButtonLeft.SetBool("PopIn", false);
                PopUpButtonRight.SetBool("PopIn", true);
            }
            else
            {
                PopUpButtonRight.Play("Idle");
                PopUpButtonRight.SetBool("PopIn", false);
                PopUpButtonLeft.Play("Idle");
                PopUpButtonLeft.SetBool("PopIn", false);
            }
            //PopUpPromptText.fontSize = 12;
            currentText = 0;
            PopUpPromptText.text = ArrayText[0];
            counter++;
        }

        private void DisplayPrompt(string text, float duration)
        {
            PopUpPromptAnimator.SetBool("DisplayPrompt", true);
            PopUpPromptTextAnimator.SetBool("DisplayPrompt", true);
            //PopUpPromptText.fontSize = 12;
            PopUpPromptText.text = text;
            StartCoroutine(DestroyMessagePrompt(duration));
        }

        IEnumerator DestroyMessagePrompt(float time)
        {
            if (isUICoroutineExecuting)
                yield break;

            isUICoroutineExecuting = true;

            yield return new WaitForSeconds(time);

            PopUpPromptAnimator.SetBool("DisplayPrompt", false);
            PopUpPromptTextAnimator.SetBool("DisplayPrompt", false);

            isUICoroutineExecuting = false;
        }        
    }
}

