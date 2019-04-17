using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOpen : MonoBehaviour {

    public GameObject TutorialPanel;

    public void OpenTutorialOnClick()
    {
        TutorialPanel.SetActive(true);
        TutorialPanel.GetComponent<TutorialMainMenu>().OpenTutorial();
    }
}
