using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapInputTest : MonoBehaviour {


	void OnEnable () {
		TapInputManager.TapInputCallback += tapped;
	}

	void OnDisable () { 
		TapInputManager.TapInputCallback -= tapped;
	}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void tapped(TapVector x) {
		Debug.Log ("TAPPED ------> " + x.arr[0] + "." + x.arr[1] + "." + x.arr[2] + "." + x.arr[3] + "." + x.arr[4]); 
	}
}
