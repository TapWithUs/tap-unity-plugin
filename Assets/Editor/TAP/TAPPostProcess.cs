using System.IO;
using UnityEngine;
using UnityEditor;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnityEditor.Callbacks;
using System.Collections;

public class TAPPostProcess {

#if UNITY_IOS
	[PostProcessBuildAttribute(0)]
	public static void OnPostprocessBuild (BuildTarget buildTarget, string pathToBuiltProject)
	{
		if (buildTarget != BuildTarget.iOS) {
			return;
		}

		Debug.Log ("TAP PostProcessBuild projectPath = " + pathToBuiltProject);
		// Initialize PbxProject
		var projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		PBXProject pbxProject = new PBXProject ();
		pbxProject.ReadFromFile (projectPath);
		string targetGuid = pbxProject.TargetGuidByName ("Unity-iPhone");
		Debug.Log ("TAP PostProcessBuild pbxproject initialized");

		pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
		pbxProject.SetBuildProperty (targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

		Debug.Log ("TAP PostProcessBuild pbxproject set properties");

		string unityBridgeFrameworkPath = "Frameworks/TAP/iOS/TAPKitUnityBridge.framework";
		string guidTAPKitUnityBridge = pbxProject.FindFileGuidByProjectPath (unityBridgeFrameworkPath);
		if (guidTAPKitUnityBridge != null && guidTAPKitUnityBridge != "") {
			pbxProject.AddFileToEmbedFrameworks (targetGuid, guidTAPKitUnityBridge);
		} else { 
			Debug.LogError ("Error adding TAPKitUnityBridge to embed frameworks");
		}


		//string FrameworksPluginsPath = "Frameworks/TAP/iOS/";
		/*
		// Add embed frameworks :
		string FrameworksPluginsPath = "Plugins/iOS/TAP";
//		string TAPKitFrameworkName = "TAPKit.framework";
		string TAPKitUnityBridgeFrameworkName = "TAPKitUnityBridge.framework";




//		string guidTAPKit = pbxProject.FindFileGuidByProjectPath ("Frameworks/" + FrameworksPluginsPath + "/" + TAPKitFrameworkName);
		//string guidTAPKitUnityBridge = pbxProject.FindFileGuidByProjectPath ("Frameworks/" + FrameworksPluginsPath + "/" + TAPKitUnityBridgeFrameworkName);
		string guidTAPKitUnityBridge = pbxProject.FindFileGuidByProjectPath ("Frameworks/" + FrameworksPluginsPath + "/" + TAPKitUnityBridgeFrameworkName);

		Debug.Log ("TAP PostProcessBuild TAPKitUnityBridge guid = " + guidTAPKitUnityBridge);
//		if (guidTAPKit == "") {
//			Debug.LogError (TAPKitFrameworkName + " Framework not found. make sure it's in the correct folder: " + FrameworksPluginsPath);
//		} else 

		if (guidTAPKitUnityBridge == null || (guidTAPKitUnityBridge != null && guidTAPKitUnityBridge == "")) {
			Debug.LogError (TAPKitUnityBridgeFrameworkName + " Framework not found. make sure it's in the correct folder: " + FrameworksPluginsPath);
		} else {
//			pbxProject.AddFileToEmbedFrameworks (targetGuid, guidTAPKit);
			pbxProject.AddFileToEmbedFrameworks (targetGuid, guidTAPKitUnityBridge);
			Debug.Log ("TAP PostProcessBuild pbxproject Added embed frameworks");
		}
		*/
		File.WriteAllText (projectPath, pbxProject.WriteToString ());
	}

#endif



}
