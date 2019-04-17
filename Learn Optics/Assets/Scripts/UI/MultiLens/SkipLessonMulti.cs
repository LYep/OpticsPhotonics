using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLessonMulti : MonoBehaviour {

    Scene CurrentScene;
    GameObject PromptPanel;

    // Use this for initialization
    void Start()
    {
        CurrentScene = SceneManager.GetActiveScene();
    }

    public void OnClickSkip()
    {

    }
}
