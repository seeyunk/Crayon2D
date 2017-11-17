using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolutionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (Screen.width, Screen.height, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
