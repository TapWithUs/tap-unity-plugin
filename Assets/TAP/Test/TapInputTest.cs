using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapInputTest : MonoBehaviour 
{

    public Text LogText;

    private ITapInput tapInputManager;

	void Start() 
    {

        tapInputManager = TapInputManager.Instance;

        tapInputManager.OnTapInputReceived += onTapped;
        tapInputManager.OnBluetoothTurnedOn += onBluetoothOn;
        tapInputManager.OnBluetoothTurnedOff += onBluetoothOff;
        tapInputManager.OnTapConnected += onTapConnected;
        tapInputManager.OnTapDisconnected += onTapDisconnected;
        tapInputManager.OnMouseInputReceived += onMoused;

        tapInputManager.EnableDebug ();
        
	}
    
    private void Log(string text)
    {
        if (LogText != null) {
            LogText.text += string.Format("{0}\n", text);
        }
    }

	void onMoused(string identifier, int vx, int vy, bool isMouse) 
    {
		Debug.Log("Moused with identifier " + identifier + ", velocity = (" + vx + "," + vy + "), isMouse " + isMouse);
        Log("Moused with identifier " + identifier + ", velocity = (" + vx + "," + vy + "), isMouse " + isMouse);
	}

	void onTapped(string identifier, int combination) 
    {
		Debug.Log ("UNITY TAP CALLBACK --- onTapped : " + identifier + ", " + combination);
		bool[] arr = TapCombination.toFingers (combination);
		Debug.Log ("UNITY TAP CALLBACK --- onTapped, fingers : thumb " + arr [0] + ", index " + arr [1] + ", middle " + arr [2] + ", ring " + arr [3] + ", pinky " + arr [4]);
        Log("UNITY TAP CALLBACK --- onTapped : " + identifier + ", " + combination);
	}

	void onTapConnected(string identifier, string name, int fw) 
    {
		Debug.Log ("UNITY TAP CALLBACK --- onTapConnected : " + identifier + ", " + name + ", FW: " + fw);
        Log("UNITY TAP CALLBACK-- - onTapConnected : " + identifier + ", " + name);

	}

	void onTapDisconnected(string identifier) 
    {
		Debug.Log ("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
        Log("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
	}

	void onBluetoothOn() 
    {
		Debug.Log ("UNITY TAP CALLBACK --- onBluetoothOn");
	}

	void onBluetoothOff() 
    {
		Debug.Log("UNITY TAP CALLBACK --- onBluetoothOff");
	}

}