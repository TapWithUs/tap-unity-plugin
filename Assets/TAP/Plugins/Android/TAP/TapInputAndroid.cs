using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapInputAndroid : MonoBehaviour {

	public const int MODE_TEXT = 1;
	public const int MODE_CONTROLLER = 2;

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
	public event Action<string[]> OnConnectedTapsReceived;
	public event Action<string, int> OnModeReceived;

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
		tapUnityAdapter.Call ("resume");
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

	private void onTapConnected(string tapIdentifier)
	{
		tapUnityAdapter.Call ("readName", tapIdentifier);
	}

	private void onTapDisconnected(string tapIdentifier)
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

	private void onTapInputReceived(string args)
	{
		if (OnTapInputReceived != null) {
			string[] argParts = args.Split (ARGS_SEPERATOR);

			int d = 0;
			Int32.TryParse (argParts[1], out d);
			OnTapInputReceived (argParts[0], d);
		}
	}

	private void onConnectedTapsReceived(String tapsArg)
	{
		if (OnConnectedTapsReceived != null) {
			string[] taps = tapsArg.Split (ARGS_SEPERATOR);

			OnConnectedTapsReceived (taps);
		}
	}

	private void onModeReceived(String modeArg)
	{
		if (OnModeReceived != null) {
			string[] argParts = modeArg.Split (ARGS_SEPERATOR);
			int mode = 0;
			Int32.TryParse (argParts[1], out mode);
			OnTapInputReceived (argParts[0], mode);
		}
	}
}
