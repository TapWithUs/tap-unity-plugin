using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapInputAndroid : MonoBehaviour {

	private const string GAME_OBJECT_NAME = "TapInputAndroid";
	private const Char ARGS_SEPERATOR = '|';

	public event Action OnBluetoothTurnedOn;
	public event Action OnBluetoothTurnedOff;
	public event Action<string, string> OnDeviceConnected;
	public event Action<string> OnDeviceDisconnected;
	public event Action<string, string> OnNameRead;
	public event Action<string> OnControllerModeStarted;
	public event Action<string> OnTextModeStarted;
	public event Action<string, int> OnTapInputReceived;

	private static TapInputAndroid instance;
	public static TapInputAndroid Instance { get { return instance; } }

	private AndroidJavaObject tapUnityAdapter;

	private void Awake()
	{
		Debug.LogError ("Awake");
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}

		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unityPlayer.GetStatic <AndroidJavaObject>("currentActivity"); 
		AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

		tapUnityAdapter = new AndroidJavaObject("com.tapwithus.tapunity.TapUnityAdapter", context);
		EnableDebug ();
		#endif
	}

	private void OnApplicationPause(bool isPaused) {
		Debug.LogError ("OnApplicationPause " + isPaused);
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (isPaused) {
			tapUnityAdapter.Call ("pause");
		} else {
			tapUnityAdapter.Call ("resume");
		}
		#endif
	}

	private void OnDestroy()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapUnityAdapter.Call ("destroy");
		#endif
	}

	public void EnableDebug()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapUnityAdapter.Call ("enableDebug");
		#endif
	}

	public void DisableDebug()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapUnityAdapter.Call ("disableDebug");
		#endif
	}

	public void StartControllerMode(string tapIdentifier)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapUnityAdapter.Call ("startControllerMode", tapIdentifier);
		#endif
	}

	public void StartTextMode(string tapIdentifier)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapUnityAdapter.Call ("startTextMode", tapIdentifier);
		#endif
	}

	private void onBluetoothTurnedOn() {
		if (OnBluetoothTurnedOn != null) {
			OnBluetoothTurnedOn ();
		}
	}

	private void onBluetoothTurnedOff() {
		if (OnBluetoothTurnedOff != null) {
			OnBluetoothTurnedOff ();
		}
	}

	private void onDeviceConnected(string tapIdentifier)
	{
		tapUnityAdapter.Call("readName", tapIdentifier);
		
//		if (OnDeviceConnected != null) {
//			OnDeviceConnected (tapIdentifier);
//		}
	}

	private void onDeviceDisconnected(string tapIdentifier)
	{
		if (OnDeviceDisconnected != null) {
			OnDeviceDisconnected (tapIdentifier);
		}
	}

	private void onNameRead(string args) {
		if (OnDeviceConnected != null) {
			string[] argParts = args.Split (ARGS_SEPERATOR);
			OnDeviceConnected (argParts [0], argParts [1]);
		}
	}

	private void onControllerModeStarted(string tapIdentifier) {
		if (OnControllerModeStarted != null) {
			OnControllerModeStarted (tapIdentifier);
		}
	}

	private void onTextModeStarted(string tapIdentifier) {
		if (OnTextModeStarted != null) {
			OnTextModeStarted (tapIdentifier);
		}
	}

	private void onTapInputReceived(string data)
	{
		if (OnTapInputReceived != null) {
			string[] argParts = data.Split (ARGS_SEPERATOR);

			int d = 0;
			Int32.TryParse (argParts[1], out d);
			OnTapInputReceived (argParts[0], d);
		}
	}
}
