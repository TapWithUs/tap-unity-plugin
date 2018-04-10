using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private TapInputAndroid tapInputAndroid;

	void Start () {
		Debug.Log ("Test start");

		tapInputAndroid = TapInputAndroid.Instance;
		tapInputAndroid.EnableDebug ();

		tapInputAndroid.OnDeviceConnected += OnDeviceConnected;
		tapInputAndroid.OnDeviceDisconnected += OnDeviceDisconnected;
		tapInputAndroid.OnNotificationReceived += OnNotificationReceived;

		tapInputAndroid.EstablishConnections ();
	}

	private void OnDeviceConnected(string macAddress)
	{
		Debug.Log ("Test OnDeviceConnected " + macAddress);

		tapInputAndroid.Subscribe (macAddress);
	}

	private void OnDeviceDisconnected(string macAddress)
	{
		Debug.Log ("Test OnDeviceDisconnected " + macAddress);
	}

	private void OnNotificationSubscribed(string macAddress)
	{
		Debug.Log ("Test OnNotificationSubscribed " + macAddress);
	}

	private void OnNotificationReceived(int data)
	{
		Debug.Log ("Test OnNotificationReceived " + data);
	}
}