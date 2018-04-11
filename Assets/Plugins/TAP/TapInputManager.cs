
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;



public class TapInputManager : MonoBehaviour {

	public static Action<string, int> OnTapped;
	public static Action<string, string> OnTapConnected;
	public static Action<string> OnTapDisconnected;
	public static Action OnBluetoothTurnedOn;
	public static Action OnBluetoothTurnedOff;

	static TapInputManager()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		const string tapInputAndroidname = "TapInputAndroid";
		GameObject tapInputAndroid = GameObject.Find(tapInputAndroidname);
		if (tapInputAndroid == null)
		{
			tapInputAndroid = new GameObject(tapInputAndroidname);
			tapInputAndroid.AddComponent(typeof (TapInputAndroid));
			DontDestroyOnLoad(tapInputAndroid);
			TapInputAndroid.Instance.OnDeviceConnected += onTapConnected;
			TapInputAndroid.Instance.OnDeviceDisconnected += onTapDisconnected;
			TapInputAndroid.Instance.OnTapInputReceived += onTapped;
			TapInputAndroid.Instance.OnBluetoothTurnedOn += onBluetoothTurnedOn;
			TapInputAndroid.Instance.OnBluetoothTurnedOff += onBluetoothTurnedOff;
		}
		#endif

		#if UNITY_IOS && !UNITY_EDITOR
		const string tapinputiosname = "TapInputIOS";
		GameObject goios = GameObject.Find(tapinputiosname);
		if (goios == null)
		{
			Debug.Log("TapInputManager initializing iOS");
			goios = new GameObject(tapinputiosname);
			goios.AddComponent(typeof (TapInputIOS));
			DontDestroyOnLoad(goios);
			TapInputIOS.OnTapConnected += onTapConnected;
			TapInputIOS.OnTapDisconnected += onTapDisconnected;
			TapInputIOS.OnBluetoothTurnedOn += onBluetoothTurnedOn;
			TapInputIOS.OnBluetoothTurnedOff += onBluetoothTurnedOff;
			TapInputIOS.OnTapped += onTapped;
			Debug.Log("TapInputManager initializing iOS successful");

		}
		#endif

		const string goname = "TapInputManager";
		GameObject gomanager = GameObject.Find(goname);
		if (gomanager == null)
		{
			gomanager = new GameObject(goname);
			gomanager.AddComponent(typeof (TapInputManager));
			DontDestroyOnLoad(gomanager);
		}


	}



	public static void startTAPInputManager() {
		#if UNITY_IOS && !UNITY_EDITOR
		TapInputIOS.startTAP();
		#endif

	}

	private static void onTapConnected(string identifier, string name) {
		if (OnTapConnected != null) {
			OnTapConnected (identifier, name);
		}
	}

	private static void onTapDisconnected(string identifier) {
		if (OnTapDisconnected != null) {
			OnTapDisconnected (identifier);
		}
	}

	private static void onTapped(string identifier, int combination) {
		if (OnTapped != null) {
			OnTapped (identifier, combination);
		}
	}

	private static void onBluetoothTurnedOn() {
		if (OnBluetoothTurnedOn != null) {
			OnBluetoothTurnedOn ();
		}
	}

	private static void onBluetoothTurnedOff() {
		if (OnBluetoothTurnedOff != null) {
			OnBluetoothTurnedOff ();
		}
	}

	public static void setControllerMode(string identifier) {
		#if UNITY_IOS && !UNITY_EDITOR
		TapInputIOS.setControllerMode (identifier);
		#endif 


		#if UNITY_ANDROID && !UNITY_EDITOR 
		TapInputAndroid.Instance.StartControllerMode (identifier);
		#endif
	}

	public static void setTextMode(string identifier) {
		#if UNITY_IOS && !UNITY_EDITOR
		TapInputIOS.setTextMode(identifier);
		#endif

		#if UNITY_ANDROID && !UNITY_EDITOR 
		TapInputAndroid.Instance.StartTextMode (identifier);
		#endif
	}

	public static void setDebugLogging(bool enabled) {
		#if UNITY_IOS && !UNITY_EDITOR
		TapInputIOS.setDebugLogging(enabled);
		#endif

		#if UNITY_ANDROID && !UNITY_EDITOR
		if (enabled) {
		TapInputAndroid.Instance.EnableDebug();
		} else {
		TapInputAndroid.Instance.DisableDebug();	
		}
		#endif
	}

}