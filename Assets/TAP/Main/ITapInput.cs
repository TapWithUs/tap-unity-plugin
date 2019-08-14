using System.Collections;
using System.Collections.Generic;
using System;

public interface ITapInput
{
    event Action OnBluetoothTurnedOn;
    event Action OnBluetoothTurnedOff;
    event Action<string, string, int> OnTapConnected;
    event Action<string> OnTapDisconnected;
    event Action<string> OnControllerModeStarted;
    event Action<string> OnTextModeStarted;
    event Action<string, int> OnTapInputReceived;
    event Action<string, int, int, bool> OnMouseInputReceived;
    event Action<string[]> OnConnectedTapsReceived;
    event Action<string, int> OnModeReceived;

    void EnableDebug();
    void DisableDebug();
    void StartControllerMode(string tapIdentifier);
    void StartTextMode(string tapIdentifier);
}
