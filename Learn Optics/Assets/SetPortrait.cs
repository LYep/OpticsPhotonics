﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPortrait : MonoBehaviour {

	void Start ()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	
}
