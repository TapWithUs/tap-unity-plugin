#if UNITY_IOS && !UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;
using System;
public class TapInputIOS : Singleton<TapInputIOS>, ITapInput {

	public event Action<string,int> OnTapInputReceived;
	public event Action<string> OnTapDisconnected;
	public event Action<string, string, int> OnTapConnected;
	public event Action OnBluetoothTurnedOn;
	public event Action OnBluetoothTurnedOff;
	public event Action<string, int, int, bool> OnMouseInputReceived;
	public event Action<string> OnControllerModeStarted;
	public event Action<string> OnTextModeStarted;
    public event Action<string, int> OnAirGestureInputReceived;
    public event Action<string, int> OnTapChangedState;
	// WTF:
	public event Action<string[]> OnConnectedTapsReceived;
	public event Action<string, int> OnModeReceived;

	[DllImport("__Internal")]
	private static extern void TAPKit_start();

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForTapped(Action<string,int> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForMoused(Action<string,int,int,bool> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForBluetoothState(Action<bool> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForTapConnected (Action<string,string,int> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForTapDisconnected (Action<string> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setControllerMode(string identifier);

	[DllImport("__Internal")]
	private static extern void TAPKit_setTextMode(string identifier);

	[DllImport("__Internal")]
	private static extern void TAPKit_enableDebug();

	[DllImport("__Internal")]
	private static extern void TAPKit_disableDebug();

//	private delegate void NoParamDelegate();
	private delegate void BoolParamDelegate(bool b);
	private delegate void StringParamDelegate(string s);
	private delegate void StringAndIntParamDelegate(string s, int i);
	private delegate void StringAndStringParamDelegate(string s1, string s2);
	private delegate void StringAndTwoIntsParamDelegate(string s1, int i1, int i2);


	public override void OnInit()
	{
		TAPKit_setUnityCallbackForTapped (onTapped);
		TAPKit_setUnityCallbackForMoused (onMoused);
		TAPKit_setUnityCallbackForTapConnected (onTapConnected);
		TAPKit_setUnityCallbackForTapDisconnected (onTapDisconnected);
		TAPKit_setUnityCallbackForBluetoothState (onBluetoothState);
		TAPKit_start ();
	}

	[MonoPInvokeCallback(typeof(BoolParamDelegate))] 
	private static void onBluetoothState(bool poweredOn) {
		if (poweredOn) {
			if (TapInputIOS.Instance.OnBluetoothTurnedOn != null) {
				TapInputIOS.Instance.OnBluetoothTurnedOn ();
			} 	
		} else {
			if (TapInputIOS.Instance.OnBluetoothTurnedOff != null) {
				TapInputIOS.Instance.OnBluetoothTurnedOff ();
			} 	
		}
	}
	[MonoPInvokeCallback(typeof(StringAndIntParamDelegate))]
	private static void onTapped(string identifier, int combination) {
		if (TapInputIOS.Instance.OnTapInputReceived != null) {
			TapInputIOS.Instance.OnTapInputReceived (identifier, combination);
		}
	}


	[MonoPInvokeCallback(typeof(StringAndTwoIntsParamDelegate))]
	private static void onMoused(string identifier, int vx, int vy, bool isMouse) {
		if (TapInputIOS.Instance.OnMouseInputReceived != null) {
			TapInputIOS.Instance.OnMouseInputReceived(identifier, vx, vy, isMouse);
		}
	}

	[MonoPInvokeCallback(typeof(StringAndStringParamDelegate))]
	private static void onTapConnected(string identifier, string name, int fw) {
		if (TapInputIOS.Instance.OnTapConnected != null) {
			TapInputIOS.Instance.OnTapConnected (identifier, name, fw);
		}
	}

	[MonoPInvokeCallback(typeof(StringParamDelegate))]
	private static void onTapDisconnected(string identifier) {
		if (TapInputIOS.Instance.OnTapDisconnected != null) {
			TapInputIOS.Instance.OnTapDisconnected (identifier);
		}
	}

//	public static void startTAP() {
//		TAPKit_setUnityCallbackForTapped (onTapped);
//		TAPKit_setUnityCallbackForMoused (onMoused);
//		TAPKit_setUnityCallbackForTapConnected (onTapConnected);
//		TAPKit_setUnityCallbackForTapDisconnected (onTapDisconnected);
//		TAPKit_setUnityCallbackForBluetoothState (onBluetoothState);
//		TAPKit_start ();
//	}

	public void EnableDebug() { 
		TAPKit_enableDebug ();
	}

	public void DisableDebug() { 
		TAPKit_disableDebug ();
	}

//	public void setDebugLogging(bool enabled) {
//		if (enabled) {
//			TAPKit_enableDebug ();
//		} else {
//			TAPKit_disableDebug ();
//		}
//	}

	public void StartControllerMode(string identifier) {
		TAPKit_setControllerMode (identifier);
		if (TapInputIOS.Instance.OnControllerModeStarted != null) { 
			TapInputIOS.Instance.OnControllerModeStarted (identifier);
		}
	}

	public void StartTextMode(string identifier) {
		TAPKit_setTextMode (identifier);
		if (TapInputIOS.Instance.OnTextModeStarted != null) { 
			TapInputIOS.Instance.OnControllerModeStarted (identifier);
		}
	}

    void SetMouseHIDEnabledInRawModeForAllTaps(bool enable) 
    {
    }

    bool IsAnyTapInAirMouseState()
    {
        return false;
    }


    void readAllTapsState()
    {
    }

    public bool IsAnyTapSupportsAirMouse()
    {
        return false;
    }
	
}

#endif
