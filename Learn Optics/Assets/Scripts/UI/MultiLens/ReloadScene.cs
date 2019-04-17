using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    Scene CurrentScene;
	// Use this for initialization
	void Start () {
        CurrentScene = SceneManager.GetActiveScene();
	}

    public void OnClickReload()
    {
        SceneManager.LoadScene(CurrentScene.name);
    }
}
