using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapInputTest : MonoBehaviour 
{

    public Text LogText;

    private ITapInput tapInputManager;

    private bool mouseHIDEnabled;

    private string connectedTapIdentifier="";

	void Start() 
    {

        tapInputManager = TapInputManager.Instance;

        tapInputManager.OnTapInputReceived += onTapped;
        tapInputManager.OnTapConnected += onTapConnected;
        tapInputManager.OnTapDisconnected += onTapDisconnected;
        tapInputManager.OnMouseInputReceived += onMoused;
        tapInputManager.OnAirGestureInputReceived += onAirGestureInputReceived;
        tapInputManager.OnTapChangedAirGestureState += onTapChangedState;
        tapInputManager.OnRawSensorDataReceived += onRawSensorDataReceived;
        tapInputManager.EnableDebug ();
        mouseHIDEnabled = false;
        
	}
    
    private void Log(string text)
    {
        if (LogText != null) {
            LogText.text += string.Format("{0}\n", text);
        }
        Debug.Log(text);
    }

	void onMoused(string identifier, int vx, int vy, bool isMouse) 
    {
        Log("onMoused" + identifier + ", velocity = (" + vx + "," + vy + "), isMouse " + isMouse);
	}

	void onTapped(string identifier, int combination) 
    {
				bool[] arr = TapCombination.toFingers (combination);
        Log("onTapped : " + identifier + ", " + combination);
	}

	void onTapConnected(string identifier, string name, int fw)
    {
        Debug.Log("onTapConnected : " + identifier + ", " + name + ", FW: " + fw);
        Log("onTapConnected : " + identifier + ", " + name);
        this.connectedTapIdentifier = identifier;
    }

    void onTapDisconnected(string identifier)
    {
        Debug.Log("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
        Log("UNITY TAP CALLBACK --- onTapDisconnected : " + identifier);
        if (identifier.Equals(this.connectedTapIdentifier))
        {
            this.connectedTapIdentifier = "";
        }
    }

    
    void onAirGestureInputReceived(string tapIdentifier, TapAirGesture gesture)
    {
        
        Log("OnAirGestureInputReceived: " + tapIdentifier + ", " + gesture.ToString());
    }

    void onTapChangedState(string tapIdentifier, bool isAirGesture)
    {
        Log("onTapChangedState: " + tapIdentifier + ", " + isAirGesture.ToString());
        
    }

    void onRawSensorDataReceived(string tapIdentifier, RawSensorData data)
    {
        //RawSensorData Object has a timestamp, type and an array points(x,y,z).
        if (data.type == RawSensorData.DataType.Device)
        {
            // Fingers accelerometer.
            // Each point in array represents the accelerometer value of a finger (thumb, index, middle, ring, pinky).
            Vector3 thumb = data.GetPoint(RawSensorData.iDEV_THUMB);

            if (thumb != null) 
            {
                // Do something with thumb.x, thumb.y, thumb.z
            }
            // Etc... use indexes: RawSensorData.iDEV_THUMB, RawSensorData.iDEV_INDEX, RawSensorData.iDEV_MIDDLE, RawSensorData.iDEV_RING, RawSensorData.iDEV_PINKY
        }
        else if (data.type == RawSensorData.DataType.IMU)
        {
            // Refers to an additional accelerometer on the Thumb sensor and a Gyro (placed on the thumb unit as well).
            Vector3 gyro = data.GetPoint(RawSensorData.iIMU_GYRO);
            if (gyro != null)
            {
                // Do something with gyro.x, gyro.y, gyro.z
            }
            // Etc... use indexes: RawSensorData.iIMU_GYRO, RawSensorData.iIMU_ACCELEROMETER
        }
        // -------------------------------------------------
        // -- Please refer readme.md for more information --
        // -------------------------------------------------
    }
}