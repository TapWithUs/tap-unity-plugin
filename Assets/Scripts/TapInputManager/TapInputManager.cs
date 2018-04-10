using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;

public class TapInputManager : MonoBehaviour {

	public static Action<TapVector> TapInputCallback;

	static TapInputManager()
	{
		const string goname = "TapInputManager";
		GameObject go = GameObject.Find(goname);
		if (go == null)
		{
			go = new GameObject(goname);
			go.AddComponent(typeof (TapInputManager));
			DontDestroyOnLoad(go);
		}

		const string tapInputAndroidS = "TapInputAndroid";
		GameObject tapInputAndroid = GameObject.Find(tapInputAndroidS);
		if (tapInputAndroid == null)
		{
			tapInputAndroid = new GameObject(tapInputAndroidS);
			tapInputAndroid.AddComponent(typeof (TapInputAndroid));
			DontDestroyOnLoad(tapInputAndroid);
		}
	}


#if UNITY_IOS

	delegate void IntParamDelegate(int tapcode);

#endif

	void Start() {
		registerPlatformInputController ();

	}


	// Initialization
	void registerPlatformInputController() {
		
#if UNITY_IOS

		iOSTapInput.registerTapCallback(userTapped);
		iOSTapInput.Activate(true);
		Debug.Log ("iOS Tap Plugin registered");

#elif UNITY_ANDROID 

//		TapInputAndroid.Instance.EnableDebug ();
		TapInputAndroid.Instance.OnDeviceConnected += OnDeviceConnected;
		TapInputAndroid.Instance.OnDeviceDisconnected += OnDeviceDisconnected;
		TapInputAndroid.Instance.OnNotificationReceived += OnNotificationReceived;

#endif

	}

#if UNITY_IOS

	[MonoPInvokeCallback(typeof(IntParamDelegate))]
	static void userTapped(int tapCode) {
		Debug.Log ("Tap Registered" + tapCode);
		tapRegistered (new TapVector (tapCode));
	}

#elif UNITY_ANDROID 

	private void OnDeviceConnected(string macAddress)
	{
		Debug.Log ("Test OnDeviceConnected " + macAddress);
		TapInputAndroid.Instance.Subscribe (macAddress);
	}
	
	private void OnDeviceDisconnected(string macAddress)
	{
		Debug.Log ("Test OnDeviceDisconnected " + macAddress);
	}

	private void OnNotificationReceived(int data)
	{
		Debug.Log ("Test OnNotificationReceived " + data);
		tapRegistered (new TapVector (data));
	}

#endif

	static void tapRegistered(TapVector tap) {
		if (TapInputCallback!=null) {
			TapInputCallback (tap);
		}
	}
}