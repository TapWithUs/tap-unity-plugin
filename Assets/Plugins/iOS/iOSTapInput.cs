using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class iOSTapInput : MonoBehaviour {


	#if UNITY_IOS

	[DllImport ("__Internal")]
	private static extern void _activate (bool state);

	[DllImport("__Internal")]
	private static extern void _registerUnityTapCallback (Action<int> tCallback);

	public static void Activate(bool state) {
		_activate (state);
	}

	public static void registerTapCallback (Action<int> tCallback) {
		_registerUnityTapCallback(tCallback);
	}

	#endif

	static iOSTapInput()
	{
		const string goname = "iOSTapInputManager";
		GameObject go = GameObject.Find(goname);
		if (go == null)
		{
			go = new GameObject(goname);
			go.AddComponent(typeof (iOSTapInput));
			DontDestroyOnLoad(go);
		}
	}
}
