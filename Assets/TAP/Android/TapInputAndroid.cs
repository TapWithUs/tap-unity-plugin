//#if UNITY_ANDROID && !UNITY_EDITOR

using UnityEngine;
using System;

public class TapInputAndroid : Singleton<TapInputAndroid>, ITapInput {

	public const int MODE_TEXT = 1;
	public const int MODE_CONTROLLER = 2;

	private const Char ARGS_SEPERATOR = '|';

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

	private AndroidJavaObject tapUnityAdapter;

    public override void OnInit()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        
        tapUnityAdapter = new AndroidJavaObject("com.tapwithus.tapunity.TapUnityAdapter", context);
        tapUnityAdapter.Call("resume");
    }

	private void OnApplicationPause(bool isPaused) {
		if (isPaused) {
			tapUnityAdapter.Call ("pause");
		} else {
			tapUnityAdapter.Call ("resume");
		}
	}

	private void OnDestroy()
	{
		tapUnityAdapter.Call ("destroy");
	}

	public void EnableDebug()
	{
		tapUnityAdapter.Call ("enableDebug");
	}

	public void DisableDebug()
	{
		tapUnityAdapter.Call ("disableDebug");
	}

	public void StartControllerMode(string tapIdentifier)
	{
		tapUnityAdapter.Call ("startControllerMode", tapIdentifier);
	}

	public void StartTextMode(string tapIdentifier)
	{
		tapUnityAdapter.Call ("startTextMode", tapIdentifier);
	}

	private void onBluetoothTurnedOn() {
		if (OnBluetoothTurnedOn != null) {
			OnBluetoothTurnedOn ();
		}
	}

	private void onBluetoothTurnedOff() {
		if (OnBluetoothTurnedOff != null) {
			OnBluetoothTurnedOff ();
		}
	}

	private void onTapConnected(string tapIdentifier)
	{
		tapUnityAdapter.Call ("getCachedTap", tapIdentifier);
    }

    private void onTapDisconnected(string tapIdentifier)
	{
		if (OnTapDisconnected != null) {
			OnTapDisconnected (tapIdentifier);
		}
	}

    private void onCachedTapRetrieved(string args)
    {
        if (OnTapConnected != null)
        {
            string[] argParts = args.Split(ARGS_SEPERATOR);

            int d = 0;
            Int32.TryParse(argParts[5], out d);

            OnTapConnected(argParts[0], argParts[1], d);
        }
    }

    private void onControllerModeStarted(string tapIdentifier) {
		if (OnControllerModeStarted != null) {
			OnControllerModeStarted (tapIdentifier);
		}
	}

	private void onTextModeStarted(string tapIdentifier) {
		if (OnTextModeStarted != null) {
			OnTextModeStarted (tapIdentifier);
		}
	}

	private void onTapInputReceived(string args)
	{
		if (OnTapInputReceived != null) {
			string[] argParts = args.Split (ARGS_SEPERATOR);

			int d = 0;
			Int32.TryParse (argParts[1], out d);

			OnTapInputReceived (argParts[0], d);
		}
	}

	private void onMouseInputReceived(string args)
	{
		if (OnMouseInputReceived != null) {
			string[] argParts = args.Split (ARGS_SEPERATOR);

			int dx = 0;
			Int32.TryParse (argParts[1], out dx);

			int dy = 0;
			Int32.TryParse (argParts[2], out dy);

            int p = 0;
            Int32.TryParse(argParts[3], out p);

            OnMouseInputReceived (argParts [0], dx, dy, p != 0);
		}
	}

	private void onConnectedTapsReceived(String tapsArg)
	{
		if (OnConnectedTapsReceived != null) {
			string[] taps = tapsArg.Split (ARGS_SEPERATOR);

			OnConnectedTapsReceived (taps);
		}
	}

	private void onModeReceived(String modeArg)
	{
		if (OnModeReceived != null) {
			string[] argParts = modeArg.Split (ARGS_SEPERATOR);
			int mode = 0;
			Int32.TryParse (argParts[1], out mode);
			OnModeReceived(argParts[0], mode);
		}
	}
}

//#endif