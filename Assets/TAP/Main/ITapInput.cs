using System.Collections;
using System.Collections.Generic;
using System;

public interface ITapInput
{
    event Action OnBluetoothTurnedOn;
    event Action OnBluetoothTurnedOff;
    event Action<string, string, int> OnTapConnected;
    event Action<string> OnTapDisconnected;
    event Action<string, int> OnTapInputReceived;
    event Action<string, int, int, bool> OnMouseInputReceived;
    event Action<string[]> OnConnectedTapsReceived;
    event Action<string, TapAirGesture> OnAirGestureInputReceived;
    event Action<string, bool> OnTapChangedAirGestureState;
    event Action<string, RawSensorData> OnRawSensorDataReceived;
    
    void EnableDebug();
    void DisableDebug();
    void StartControllerMode(string tapIdentifier);
    void StartTextMode(string tapIdentifier);
    void StartControllerWithMouseHIDMode(string tapIdentifier);
    void StartRawSensorMode(string tapIdentifier, int deviceAccelerometerSensitivity, int imuGyroSensitivity, int imuAccelerometerSensitivity);

    void Vibrate(string tapIdentifier, int[] durations);
    

}
