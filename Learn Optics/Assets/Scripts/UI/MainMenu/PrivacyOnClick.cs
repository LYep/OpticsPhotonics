using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyOnClick : MonoBehaviour
{
    private string URL;
    // Start is called before the first frame update
    private void Start()
    {
        URL = "https://sites.google.com/view/optics-and-photonics/home";
    }
    public void PrivacyClick()
    {
        Application.OpenURL(URL);
    }
}
