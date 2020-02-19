using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapInputTest : MonoBehaviour 
{

    public Text LogText;

    private ITapInput tapInputManager;

    private bool mouseHIDEnabled;

	void Start() 
    {

        tapInputManager = TapInputManager.Instance;

        //tapInputManager.OnTapInputReceived += onTapped;
        //tapInputManager.OnBluetoothTurnedOn += onBluetoothOn;
        //tapInputManager.OnBluetoothTurnedOff += onBluetoothOff;
        //tapInputManager.OnTapConnected += onTapConnected;
        //tapInputManager.OnTapDisconnected += onTapDisconnected;
        //tapInputManager.OnMouseInputReceived += onMoused;
        tapInputManager.OnAirGestureInputReceived += onAirGestureInputReceived;
        tapInputManager.OnTapChangedState += onTapChangedState;
        tapInputManager.EnableDebug ();
        mouseHIDEnabled = false;
        
	}
    
    private void Log(string text)
    {
        if (LogText != null) {
            LogText.text += string.Format("{0}\n", text);
        }
        Debug.Log(text);
    }

	void onMoused(string identifier, int vx, int vy, bool isMouse) 
    {
        Log("onMoused" + identifier + ", velocity = (" + vx + "," + vy + "), isMouse " + isMouse);
	}

	void onTapped(string identifier, int combination) 
    {
				bool[] arr = TapCombination.toFingers (combination);
        Log("onTapped : " + identifier + ", " + combination);
	}

	//void onTapConnected(string identifier, string name, int fw) 
 //   {
	//	Debug.Log ("UNITY TAP CALLBACK --- onTapConnected : " + identifier + ", " + name + ", FW: " + fw);
 //       Log("UNITY TAP CALLBACK-- - onTapConnected : " + identifier + ", " + name);

	//}

	//void onTapDisconnected(string identifier) 
 //   {
	//	Debug.Log ("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
 //       Log("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
	//}

	//void onBluetoothOn() 
 //   {
	//	Debug.Log ("UNITY TAP CALLBACK --- onBluetoothOn");
	//}

	//void onBluetoothOff() 
 //   {
	//	Debug.Log("UNITY TAP CALLBACK --- onBluetoothOff");
	//}

    void onAirGestureInputReceived(string tapIdentifier, int gesture)
    {
        Log("OnAirGestureInputReceived: " + tapIdentifier + ", " + gesture.ToString());
    }

    void onTapChangedState(string tapIdentifier, int state)
    {
        Log("OnTapChangedState: " + tapIdentifier + ", " + state.ToString());
    }

    public void OnButtonMouseHIDClicked()
    {
        mouseHIDEnabled = !mouseHIDEnabled;
        tapInputManager.SetMouseHIDEnabledInRawModeForAllTaps(mouseHIDEnabled);
        Log("MouseHIDEnabled :" + mouseHIDEnabled.ToString());
    }

    public void OnButtonReadStateClicked()
    {
        tapInputManager.readAllTapsState();
    }

    public void OnButtonAnyStateClicked()
    {
        Log("Any Tap Supports AirMouse ? " + tapInputManager.IsAnyTapSupportsAirMouse());
    }

}