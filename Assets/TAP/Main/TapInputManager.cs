using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;

public class TapInputManager : Singleton<TapInputManager>, ITapInput {

    private ITapInput tapInput;
    
// suppress "is not used" warning
#pragma warning disable 0067

    public event Action OnBluetoothTurnedOn;
    public event Action OnBluetoothTurnedOff;
    public event Action<string, string, int> OnTapConnected;
    public event Action<string> OnTapDisconnected;
    public event Action<string> OnControllerModeStarted;
    public event Action<string> OnTextModeStarted;
    public event Action<string, int> OnTapInputReceived;
    public event Action<string, int, int, bool> OnMouseInputReceived;
    public event Action<string[]> OnConnectedTapsReceived;
    public event Action<string, int> OnModeReceived;
    public event Action<string, TapAirGesture> OnAirGestureInputReceived;
    public event Action<string, bool> OnTapChangedAirGestureState;
    public event Action<string, RawSensorData> OnRawSensorDataReceived;
#pragma warning restore 0067

    public override void OnInit()
    {

#if UNITY_EDITOR
        tapInput = TapInputEditor.Instance;

#elif UNITY_ANDROID
        tapInput = TapInputAndroid.Instance;

#elif UNITY_IOS
        tapInput = TapInputIOS.Instance;

//#elif UNITY_STANDALONE_WIN
//        tapInput = TapInputStandaloneWin.Instance;

#endif

        tapInput.OnTapConnected += onTapConnected;
        tapInput.OnTapDisconnected += onTapDisconnected;
        tapInput.OnTapInputReceived += onTapped;
        tapInput.OnBluetoothTurnedOn += onBluetoothTurnedOn;
        tapInput.OnBluetoothTurnedOff += onBluetoothTurnedOff;
        tapInput.OnMouseInputReceived += onMoused;
        tapInput.OnAirGestureInputReceived += onAirGestureInputReceived;
        tapInput.OnTapChangedAirGestureState += onTapChangedAirGestureState;
        tapInput.OnRawSensorDataReceived += onRawSensorDataReceived;
    }

    

    private void onTapConnected(string identifier, string name, int fw)
    {
        if (OnTapConnected != null) {
            OnTapConnected(identifier, name, fw);
        }
    }

    private void onTapDisconnected(string identifier)
    {
        if (OnTapDisconnected != null) {
            OnTapDisconnected(identifier);
        }
    }

    private void onTapped(string identifier, int combination)
    {
        if (OnTapInputReceived != null) {
            OnTapInputReceived(identifier, combination);
        }
    }

	private void onMoused(string identifier, int vx, int vy, bool isMouse)
    {
        if (OnMouseInputReceived != null) {
            OnMouseInputReceived(identifier, vx, vy, isMouse);
        }
    }

    private void onBluetoothTurnedOn()
    {
        if (OnBluetoothTurnedOn != null) {
            OnBluetoothTurnedOn();
        }
    }

    private void onBluetoothTurnedOff()
    {
        if (OnBluetoothTurnedOff != null) {
            OnBluetoothTurnedOff();
        }
    }

    private void onAirGestureInputReceived(string tapIdentifier, TapAirGesture gesture)
    {
        if (OnAirGestureInputReceived != null)
        {
            OnAirGestureInputReceived(tapIdentifier, gesture);
        }
    }

    private void onRawSensorDataReceived(string tapIdentifier, RawSensorData rawSensorData)
    {
        if (OnRawSensorDataReceived != null)
        {
            OnRawSensorDataReceived(tapIdentifier, rawSensorData);
        }
    }

    private void onTapChangedAirGestureState(string tapIdentifier, bool isAirGesture)
    {
        if (OnTapChangedAirGestureState != null)
        {
            OnTapChangedAirGestureState(tapIdentifier, isAirGesture);
        }
    }

    public void EnableDebug()
    {
        tapInput.EnableDebug();
    }

    public void DisableDebug()
    {
        tapInput.DisableDebug();
    }

    public void StartControllerMode(string tapIdentifier)
    {
        tapInput.StartControllerMode(tapIdentifier);

    }

    public void StartTextMode(string tapIdentifier)
    {
        tapInput.StartTextMode(tapIdentifier);
        
    }

    public void StartControllerWithMouseHIDMode(string tapIdentifier)
    {
        tapInput.StartControllerWithMouseHIDMode(tapIdentifier);
        
    }

    public void StartRawSensorMode(string tapIdentifier, int deviceAccelerometerSensitivity, int imuGyroSensitivity, int imuAccelerometerSensitivity) 
    {
        tapInput.StartRawSensorMode(tapIdentifier, deviceAccelerometerSensitivity, imuGyroSensitivity, imuAccelerometerSensitivity);
    }

    public void SetDefaultControllerMode(bool applyToConnectedTaps)
    {
        tapInput.SetDefaultControllerMode(applyToConnectedTaps);
    }

    public void SetDefaultTextMode(bool applyToConnectedTaps)
    {
        tapInput.SetDefaultTextMode(applyToConnectedTaps);
    }

    public void SetDefaultControllerWithMouseHIDMode(bool applyToConnectedTaps)
    {
        tapInput.SetDefaultControllerWithMouseHIDMode(applyToConnectedTaps);
    }

    public void Vibrate(string tapIdentifier, int[] durations)
    {
        tapInput.Vibrate(tapIdentifier, durations);
    }

    public bool isAnyTapInAirGestureState()
    {
        return tapInput.isAnyTapInAirGestureState();
    }

    public bool isAnyTapSupportsAirGestures()
    {
        return tapInput.isAnyTapSupportsAirGestures();
    }

    //private void PerformTapAction(string tapIdentifier, Action<string> action)
    //{
    //    if (tapIdentifier.Equals(TapInputManager._ALL_TAPS_))
    //    {
    //        foreach (string identifier in this.taps)
    //        {
    //            action(identifier);
    //        }
    //    } else
    //    {
    //        action(tapIdentifier);
    //    }
    //}
}