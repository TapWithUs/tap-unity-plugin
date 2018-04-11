using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapInputTest : MonoBehaviour {


//	void OnEnable () {
//		TapInputManager.TapInputCallback += tapped;
//	}
//
//	void OnDisable () { 
//		TapInputManager.TapInputCallback -= tapped;
//	}
//
	private string _identifier = "";
		
	// Use this for initialization
	void Start () {

		TapInputManager.OnTapped += onTapped;
		TapInputManager.OnBluetoothTurnedOn += onBluetoothOn;
		TapInputManager.OnBluetoothTurnedOff += onBluetoothOff;
		TapInputManager.OnTapConnected += onTapConnected;
		TapInputManager.OnTapDisconnected += onTapDisconnected;
		TapInputManager.setDebugLogging (true);
		TapInputManager.startTAPInputManager ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onTapped(string identifier, int combination) {
		Debug.Log ("UNITY TAP CALLBACK --- onTapped : " + identifier + ", " + combination);
		bool[] arr = TapCombination.toFingers (combination);
		Debug.Log ("UNITY TAP CALLBACK --- onTapped, fingers : thumb " + arr [0] + ", index " + arr [1] + ", middle " + arr [2] + ", ring " + arr [3] + ", pinky " + arr [4]);
	}

	void onTapConnected(string identifier, string name) {
		Debug.Log ("UNITY TAP CALLBACK --- onTapConnected : " + identifier + ", " + name);
		_identifier = identifier;
	}

	void onTapDisconnected(string identifier) {
		Debug.Log ("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
	}

	void onBluetoothOn() {
		Debug.Log ("UNITY TAP CALLBACK --- onBluetoothOn");
	}

	void onBluetoothOff() {
		Debug.Log("UNITY TAP CALLBACK --- onBluetoothOff");
	}

	void setControllerMode() {
		TapInputManager.setControllerMode (_identifier);
	}

	void setTextMode() {
		TapInputManager.setTextMode (_identifier);
	}
}
