using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;
using System;
public class TapInputIOS : MonoBehaviour {

	#if UNITY_IOS 

	public static event Action<string,int> OnTapped;
	public static event Action<string> OnTapDisconnected;
	public static event Action<string, string> OnTapConnected;
	public static event Action OnBluetoothTurnedOn;
	public static event Action OnBluetoothTurnedOff;



	[DllImport("__Internal")]
	private static extern void TAPKit_start();

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForTapped(Action<string,int> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForBluetoothState(Action<bool> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForTapConnected (Action<string,string> c);

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



	[MonoPInvokeCallback(typeof(BoolParamDelegate))] 
	private static void onBluetoothState(bool poweredOn) {
		if (poweredOn) {
			if (OnBluetoothTurnedOn != null) {
				OnBluetoothTurnedOn ();
			} 	
		} else {
			if (OnBluetoothTurnedOff != null) {
				OnBluetoothTurnedOff ();
			} 	
		}
	}
	[MonoPInvokeCallback(typeof(StringAndIntParamDelegate))]
	private static void onTapped(string identifier, int combination) {
		if (OnTapped != null) {
			OnTapped (identifier, combination);
		}
	}
	[MonoPInvokeCallback(typeof(StringAndStringParamDelegate))]
	private static void onTapConnected(string identifier, string name) {
		if (OnTapConnected != null) {
			OnTapConnected (identifier, name);
		}
	}

	[MonoPInvokeCallback(typeof(StringParamDelegate))]
	private static void onTapDisconnected(string identifier) {
		if (OnTapDisconnected != null) {
			OnTapDisconnected (identifier);
		}
	}

	public static void startTAP() {
		TAPKit_setUnityCallbackForTapped (onTapped);
		TAPKit_setUnityCallbackForTapConnected (onTapConnected);
		TAPKit_setUnityCallbackForTapDisconnected (onTapDisconnected);
		TAPKit_setUnityCallbackForBluetoothState (onBluetoothState);
		TAPKit_start ();
	}

	public static void setDebugLogging(bool enabled) {
		if (enabled) {
			TAPKit_enableDebug ();
		} else {
			TAPKit_disableDebug ();
		}
	}

	public static void setControllerMode(string identifier) {
		TAPKit_setControllerMode (identifier);
	}

	public static void setTextMode(string identifier) {
		TAPKit_setTextMode (identifier);
	}

	#endif
}
