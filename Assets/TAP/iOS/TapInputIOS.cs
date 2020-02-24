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
    public event Action<string, TapAirGesture> OnAirGestureInputReceived;
    public event Action<string, bool> OnTapChangedAirGestureState;
	public event Action<string, RawSensorData> OnRawSensorDataReceived;
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
	private static extern void TAPKit_setUnityCallbackForAirGestured(Action<string,int> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForChangedAirGestureState(Action<string, bool> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setUnityCallbackForRawSensorDataReceived(Action<string, string, string> c);

	[DllImport("__Internal")]
	private static extern void TAPKit_setControllerMode(string identifier);

	[DllImport("__Internal")]
	private static extern void TAPKit_setTextMode(string identifier);

	[DllImport("__Internal")]
	private static extern void TAPKit_setControllerWithMouseHIDMode(string identifier);

	[DllImport("__Internal")]
	private static extern void TAPKit_setRawSensorMode(string identifier, int devAccel, int imuGyro, int imuAccel);

	[DllImport("__Internal")]
	private static extern void TAPKit_vibrate(string identifier, string durationsString, string delimeter);

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
	private delegate void ThreeStringsParamDelegate(string s1, string s2, string s3);
	private delegate void StringAndBoolParamDelegate(string s1, bool b1);
	private delegate void TwoStringsAndIntParamDelegate(string s1, string s2, int i1);
	public override void OnInit()
	{
		TAPKit_setUnityCallbackForTapped (onTapped);
		TAPKit_setUnityCallbackForMoused (onMoused);
		TAPKit_setUnityCallbackForTapConnected (onTapConnected);
		TAPKit_setUnityCallbackForTapDisconnected (onTapDisconnected);
		TAPKit_setUnityCallbackForBluetoothState (onBluetoothState);
		TAPKit_setUnityCallbackForAirGestured(onAirGestured);
	    TAPKit_setUnityCallbackForChangedAirGestureState(onTapChangedAirGestureState);
	    TAPKit_setUnityCallbackForRawSensorDataReceived(onRawSensorDataReceived);
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

    [MonoPInvokeCallback(typeof(StringAndIntParamDelegate))]
    private static void onAirGestured(string identifier, int gesture)
    {
        if (TapInputIOS.Instance.OnAirGestureInputReceived != null)
        {
			if (Enum.IsDefined(typeof(TapAirGesture), gesture))
            {
				TapInputIOS.Instance.OnAirGestureInputReceived(identifier, (TapAirGesture)gesture);
            }

		}
	}



    [MonoPInvokeCallback(typeof(ThreeStringsParamDelegate))]
    private static void onRawSensorDataReceived(string identifier, string data, string delimeter)
    {
        if (TapInputIOS.Instance.OnRawSensorDataReceived != null)
        {
        	RawSensorData rsData = RawSensorData.makeFromString(data, delimeter); 
            if (rsData != null)
            {
				TapInputIOS.Instance.OnRawSensorDataReceived(identifier, rsData);
            }
        }
    }

    [MonoPInvokeCallback(typeof(StringAndBoolParamDelegate))]
    private static void onTapChangedAirGestureState(string identifier, bool isAirGesture)
    {
        if (TapInputIOS.Instance.OnTapChangedAirGestureState != null)
        {
			TapInputIOS.Instance.OnTapChangedAirGestureState(identifier, isAirGesture);
        }
    }

	[MonoPInvokeCallback(typeof(TwoStringsAndIntParamDelegate))]
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


	public void EnableDebug() { 
		TAPKit_enableDebug ();
	}

	public void DisableDebug() { 
		TAPKit_disableDebug ();
	}


	public void StartControllerMode(string identifier) {
		TAPKit_setControllerMode (identifier);
	}

	public void StartTextMode(string identifier) {
		TAPKit_setTextMode (identifier);
	}

	public void StartControllerWithMouseHIDMode(string tapIdentifier)
    {
		TAPKit_setControllerWithMouseHIDMode(tapIdentifier);
    }

	public void StartRawSensorMode(string tapIdentifier, int deviceAccelerometerSensitivity, int imuGyroSensitivity, int imuAccelerometerSensitivity)
    {
		TAPKit_setRawSensorMode(tapIdentifier, deviceAccelerometerSensitivity, imuGyroSensitivity, imuAccelerometerSensitivity);
    }

	public void Vibrate(string tapIdentifier, int[] durations)
    {
		string durationsString = "";
		string delimeter = "^";
        for (int i=0; i<durations.Length; i++)
        {
			durationsString = durationsString + durations[i].ToString();
            if (i < durations.Length-1)
            {
				durationsString = durationsString + delimeter;
            }
        }
		TAPKit_vibrate(tapIdentifier, durationsString, delimeter);
    }

#warning to do
    void SetDefaultControllerMode(bool applyToConnectedTaps);
    void SetDefaultTextMode(bool applyToConnectedTaps);
    void SetDefaultControllerWithMouseHIDMode(bool applyToConnectedTaps);
    bool isAnyTapInAirGestureState();
    bool isAnyTapSupportsAirGestures();
}

#endif
