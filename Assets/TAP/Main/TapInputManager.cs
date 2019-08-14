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

#pragma warning restore 0067

    public override void OnInit()
    {

#if UNITY_EDITOR
        tapInput = TapInputEditor.Instance;

#elif UNITY_ANDROID
        tapInput = TapInputAndroid.Instance;

#elif UNITY_IOS
        tapInput = TapInputIOS.Instance;

#elif UNITY_STANDALONE_WIN
        tapInput = TapInputStandaloneWin.Instance;

#endif

        tapInput.OnTapConnected += onTapConnected;
        tapInput.OnTapDisconnected += onTapDisconnected;
        tapInput.OnTapInputReceived += onTapped;
        tapInput.OnBluetoothTurnedOn += onBluetoothTurnedOn;
        tapInput.OnBluetoothTurnedOff += onBluetoothTurnedOff;
        tapInput.OnMouseInputReceived += onMoused;
    }

    //    static TapInputManager()
    //    {

    //#if UNITY_ANDROID && !UNITY_EDITOR
    //        tapInput = TapInputAndroid.Instance;

    //        tapInput.OnDeviceConnected += onTapConnectedAndroidTemp;
    //        tapInput.OnDeviceDisconnected += onTapDisconnected;
    //        tapInput.OnTapInputReceived += onTapped;
    //        tapInput.OnBluetoothTurnedOn += onBluetoothTurnedOn;
    //        tapInput.OnBluetoothTurnedOff += onBluetoothTurnedOff;
    //        tapInput.OnMouseInputReceived += onMoused;
    //#endif

    //#if UNITY_IOS && !UNITY_EDITOR
    //		const string tapinputiosname = "TapInputIOS";
    //		GameObject goios = GameObject.Find(tapinputiosname);
    //		if (goios == null)
    //		{
    //			Debug.Log("TapInputManager initializing iOS");
    //			goios = new GameObject(tapinputiosname);
    //			goios.AddComponent(typeof (TapInputIOS));
    //			DontDestroyOnLoad(goios);
    //			TapInputIOS.OnTapConnected += onTapConnected;
    //			TapInputIOS.OnTapDisconnected += onTapDisconnected;
    //			TapInputIOS.OnBluetoothTurnedOn += onBluetoothTurnedOn;
    //			TapInputIOS.OnBluetoothTurnedOff += onBluetoothTurnedOff;
    //			TapInputIOS.OnTapped += onTapped;
    //			TapInputIOS.OnMoused += onMoused;
    //			Debug.Log("TapInputManager initializing iOS successful");


    //		}
    //#endif

    //#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

    //        const string tapinputstandalonewinename = "TapInputStandaloneWin";
    //        GameObject gostandalonewin = GameObject.Find(tapinputstandalonewinename);
    //        if (gostandalonewin == null)
    //        {

    //            Debug.Log("TapInputManager initializing standalone win");
    //            gostandalonewin = new GameObject(tapinputstandalonewinename);
    //            gostandalonewin.AddComponent(typeof(TAPInputStandaloneWin));
    //            DontDestroyOnLoad(gostandalonewin);
    //            TAPInputStandaloneWin.OnTapConnected += onTapConnected;
    //            TAPInputStandaloneWin.OnTapDisconnected += onTapDisconnected;
    //            TAPInputStandaloneWin.OnTapped += onTapped;
    //            TAPInputStandaloneWin.OnMoused += onMoused;
    //            Debug.Log("TapInputManager initializing standalone win successful");


    //        }
    //#endif

    //    const string goname = "TapInputManager";
    //    GameObject gomanager = GameObject.Find(goname);
    //    if (gomanager == null)
    //    {
    //        gomanager = new GameObject(goname);
    //        gomanager.AddComponent(typeof(TapInputManager));
    //        DontDestroyOnLoad(gomanager);
    //    }
    //}

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
}