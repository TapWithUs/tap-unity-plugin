using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapInputAndroid : MonoBehaviour {

	/// <summary>
	/// Will be called when a TAP device is connected.
	/// </summary>
	public event Action<string> OnDeviceConnected;

	/// <summary>
	/// Will be called when a TAP device is disconnected.
	/// </summary>
	public event Action<string> OnDeviceDisconnected;

	/// <summary>
	/// Will be called when a notification is successfully subscribed.
	/// </summary>
//	public event Action<string> OnNotificationSubscribed;

	/// <summary>
	/// Will be called when a notification is received.
	/// </summary>
	public event Action<int> OnNotificationReceived;

	private static TapInputAndroid instance;
	public static TapInputAndroid Instance { get { return instance; } }

	private AndroidJavaObject tapBluetoothUnityAdapter;

	private void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}
	}

	private void Start()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unityPlayer.GetStatic <AndroidJavaObject>("currentActivity"); 
		AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

		tapBluetoothUnityAdapter = new AndroidJavaObject("tapwithus.com.tapunity.TapUnityAdapter", context);
		EnableDebug ();
		#endif
	}

	private void OnDestroy()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapBluetoothUnityAdapter.Call ("onDestroy");
		#endif
	}

	public void EnableDebug()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapBluetoothUnityAdapter.Call ("enableDebug");
		#endif
	}

	public void DisableDebug()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapBluetoothUnityAdapter.Call ("disableDebug");
		#endif
	}

	/// <summary>
	/// Establishes the connections with TAP devices.
	/// </summary>
	public void EstablishConnections()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapBluetoothUnityAdapter.Call ("establishConnections");
		#endif
	}

	/// <summary>
	/// Subscribes for TAP data notification for the specified TAP device.
	/// </summary>
	/// <param name="macAddress">Mac address.</param>
	public void Subscribe(string macAddress)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		tapBluetoothUnityAdapter.Call ("subscribe", macAddress);
		#endif
	}

	private void onDeviceConnected(string macAddress)
	{
		if (OnDeviceConnected != null) {
			OnDeviceConnected (macAddress);
		}
	}

	private void onDeviceDisconnected(string macAddress)
	{
		if (OnDeviceDisconnected != null) {
			OnDeviceDisconnected (macAddress);
		}
	}

//	private void onNotificationSubscribed(string macAddress)
//	{
//		if (OnNotificationSubscribed != null) {
//			OnNotificationSubscribed (macAddress);
//		}
//	}

	private void onNotificationReceived(string data)
	{
		int d = 0;
		Int32.TryParse (data, out d);

		if (OnNotificationReceived != null) {
			OnNotificationReceived (d);
		}
	}
}
