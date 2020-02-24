using System;
using UnityEngine;

public class TapInputEditor : Singleton<TapInputEditor>, ITapInput
{

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
    public event Action<string, RawSensorData> OnRawSensorDataReceived;
    public event Action<string[]> OnConnectedTapsReceived;
    public event Action<string, int> OnModeReceived;
    public event Action<string, TapAirGesture> OnAirGestureInputReceived;
    public event Action<string, bool> OnTapChangedAirGestureState;

#pragma warning restore 0067

    public override void OnInit()
    {
        Debug.Log("TapInputEditor - OnInit");
    }
    
    public void EnableDebug()
    {
        Debug.Log("TapInputEditor - EnableDebug");
    }

    public void DisableDebug()
    {
        Debug.Log("TapInputEditor - DisableDebug");
    }

    public void StartControllerMode(string tapIdentifier)
    {
        
    }

    public void StartTextMode(string tapIdentifier)
    {
        
    }

    public void StartControllerWithMouseHIDMode(string tapIdentifier)
    {

    }

    public void StartRawSensorMode(string tapIdentifier, int deviceAccelerometerSensitivity, int imuGyroSensitivity, int imuAccelerometerSensitivity)
    {

    }

    public void Vibrate(string tapIdentifier, int[] durations)
    {

    }

    public void SetDefaultControllerMode(bool applyToConnectedTaps)
    {

    }

    public void SetDefaultTextMode(bool applyToConnectedTaps)
    {

    }

    public void SetDefaultControllerWithMouseHIDMode(bool applyToConnectedTaps)
    {

    }

    public bool isAnyTapInAirGestureState()
    {
        return false;
    }

    public bool isAnyTapSupportsAirGestures()
    {
        return false;
    }




}
