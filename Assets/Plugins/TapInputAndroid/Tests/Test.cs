using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private TapInputAndroid tapInputAndroid;

	void Start () {
		Debug.Log ("Test start");

		tapInputAndroid = TapInputAndroid.Instance;
		tapInputAndroid.EnableDebug ();

		tapInputAndroid.OnBluetoothTurnedOn += OnBluetoothTurnedOn;
		tapInputAndroid.OnBluetoothTurnedOff += OnBluetoothTurnedOff;
		tapInputAndroid.OnDeviceConnected += OnDeviceConnected;
		tapInputAndroid.OnDeviceDisconnected += OnDeviceDisconnected;
		tapInputAndroid.OnNameRead += OnNameRead;
		tapInputAndroid.OnControllerModeStarted += OnControllerModeStarted;
		tapInputAndroid.OnTextModeStarted += OnTextModeStarted;
		tapInputAndroid.OnTapInputReceived += OnTapInputReceived;
	}

	private void OnBluetoothTurnedOn() {
		Debug.Log ("Test OnBluetoothTurnedOn");
	}

	private void OnBluetoothTurnedOff() {
		Debug.Log ("Test OnBluetoothTurnedOff");
	}

	private void OnDeviceConnected(string tapIdentifier)
	{
		Debug.Log ("Test OnDeviceConnected " + tapIdentifier);
		tapInputAndroid.StartControllerMode (tapIdentifier);
	}

	private void OnDeviceDisconnected(string tapIdentifier)
	{
		Debug.Log ("Test OnDeviceDisconnected " + tapIdentifier);
	}

	private void OnNameRead(string tapIdentifier, string name) {
		Debug.Log ("Test OnNameRead " + tapIdentifier + " " + name);
	}

	private void OnControllerModeStarted(string tapIdentifier) {
		Debug.Log ("Test OnControllerModeStarted " + tapIdentifier);
	}

	private void OnTextModeStarted(string tapIdentifier) {
		Debug.Log ("Test OnTextModeStarted " + tapIdentifier);
	}

	private void OnTapInputReceived(int data)
	{
		Debug.Log ("Test OnTapInputReceived " + data);
	}
}