using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandling : MonoBehaviour {
    GameObject GradeTextError;
    GameObject FeedbackTextError;

    private void Start()
    {
        GradeTextError = GameObject.Find("GradeError");
        FeedbackTextError = GameObject.Find("FeedbackError");
    }
    public void GradeChangeClearError()
    {
        GradeTextError.GetComponent<Text>().enabled = false;
    }
    public void FeedbackChangeClearError()
    {
        FeedbackTextError.GetComponent<Text>().enabled = false;
    }
}
