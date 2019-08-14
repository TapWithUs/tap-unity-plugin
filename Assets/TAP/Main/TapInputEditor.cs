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
    public event Action<string[]> OnConnectedTapsReceived;
    public event Action<string, int> OnModeReceived;
   
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
        if (OnControllerModeStarted != null) {
            OnControllerModeStarted(tapIdentifier);
        }
    }

    public void StartTextMode(string tapIdentifier)
    {
        if (OnTextModeStarted != null) {
            OnTextModeStarted(tapIdentifier);
        }
    }
}
