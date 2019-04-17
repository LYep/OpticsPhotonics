#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

//Very rudimentary method of collecting feedback. If more data is required to be collected, just adjust the feedback class here to include that data. It's all written to a JSON and 
//pushed to a firebase database right now. 

public class FeedbackButtonScript : MonoBehaviour
{
    InputField NameIF, GradeIF, FeedbackIF;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    DatabaseReference reference;
    Feedback CleanUserFeedback;
    private bool success;
    void Start()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        //Initialize Input Fields
        NameIF = GameObject.Find("NameIF").GetComponent<InputField>();
        GradeIF = GameObject.Find("GradeIF").GetComponent<InputField>();
        FeedbackIF = GameObject.Find("FeedbackIF").GetComponent<InputField>();
        success = false;

        //Authentication so only users can write/read to database (and unity editor)
        //Disable if testing in unity to prevent lockout from database (limited amount of calls for anon)
        //Change rules to public access in database if disabled
        /*auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });*/

        //Updated version to check dependencies
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    //Change the url to some NJIT firebase database or something
    void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://learn-optics.firebaseio.com");
        reference = FirebaseDatabase.DefaultInstance.RootReference;  
    }

    //allow users to send without name, push data onto database
    //Send feedback to firebase database as JSON
    public void OnClick()
    {
        success = false;
        CheckFeedback();

        if (success)
        {
            NameIF.text = "";
            GradeIF.text = "";
            FeedbackIF.text = "";
            GameObject.Find("CloseFeedbackPanelButton").GetComponent<Button>().onClick.Invoke();
        }
    }

    public void CheckFeedback()
    {
        string userName = NameIF.text;
        string userGrade = GradeIF.text;
        string userFeedback = FeedbackIF.text;

        if (string.IsNullOrEmpty(userGrade))
        {
            print("test error");
            GameObject error = GameObject.Find("GradeError");
            error.GetComponent<Text>().enabled = true;
            error.GetComponent<Animator>().SetTrigger("TrigError");
        }
        else if (string.IsNullOrEmpty(userFeedback))
        {
            GameObject error = GameObject.Find("FeedbackError");
            error.GetComponent<Text>().enabled = true;
            error.GetComponent<Animator>().SetTrigger("TrigFBError");
        }
        else if (string.IsNullOrEmpty(userName))
        {
            CleanUserFeedback = new Feedback("0", GradeIF.text, FeedbackIF.text);
            SendToDatabase(CleanUserFeedback);
        }
        else
        {
            CleanUserFeedback = new Feedback(userName, GradeIF.text, FeedbackIF.text);
            SendToDatabase(CleanUserFeedback);
        }
    }
    void SendToDatabase(Feedback checkedFeedback)
    {
        string FeedbackJSONFile = JsonUtility.ToJson(checkedFeedback);
       // reference.Child("feedback").Child(NameIF.text).SetRawJsonValueAsync(FeedbackJSONFile); //overrides users of same name
        reference.Child("Feedback").Push().SetRawJsonValueAsync(FeedbackJSONFile);
        success = true;
    }
}

[System.Serializable]
public class Feedback
{
    public string UserFeedback;
    public string UserName;
    public string UserGrade;   
    public string DateAndTime;
    public string LocalTime;
    public string DeviceType;
    public string DeviceModel;

    public Feedback(string name, string grade, string feedback)
    {
        UserName = name;
        UserGrade = grade;
        UserFeedback = feedback;
        DateAndTime = DateTime.UtcNow.ToString();
        LocalTime = DateTime.Now.ToLongTimeString();

        if(SystemInfo.deviceType == UnityEngine.DeviceType.Handheld)
        {
            DeviceType = "Handheld";
        }
        else if (SystemInfo.deviceType == UnityEngine.DeviceType.Desktop)
        {
            DeviceType = "Desktop";
        }
        else if (SystemInfo.deviceType == UnityEngine.DeviceType.Console)
        {
            DeviceType = "Console";
        }
        else if (SystemInfo.deviceType == UnityEngine.DeviceType.Unknown)
        {
            DeviceType = "Unknown";
        }

        DeviceModel = SystemInfo.deviceModel;
    }
}
#endif