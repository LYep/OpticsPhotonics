using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterAnswer : MonoBehaviour {

    public GameObject EnterButton;
    InputField AnswerField;
	// Use this for initialization
	void Start () {
        AnswerField = GetComponent < InputField>();
        AnswerField.onEndEdit.AddListener(fieldValue =>
        {
            if (Input.GetKeyDown(KeyCode.Return) ||Input.GetKeyDown(KeyCode.KeypadEnter))
                EnterButton.GetComponent<Button>().onClick.Invoke();
        });
	}		
}
