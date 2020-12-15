# TAP Plugin For Unity

## What Is This ?

TAP Plugin for Unity allows you to build a Unity app that can receive input from TAP devices,
In a way that each tap is being interpreted as an array or fingers that are tapped, or a binary combination integer (explanation follows), Thus allowing the TAP device to act as a controller for your app!

## Compatible with

TAP Plugin for Unity is compatible with iOS and Android.
### We're working on completing the project to support the Windows Standalone platform.


## Integration

This repository contains a ready-to-go test project. You can however download just the "Assets" folder and integrate it with yours.

## TapInputManager.cs

This is the main class. The only class you'll need to get your app communicating with TAP Devices.
The class contains 5 events:

```
public static Action OnBluetoothTurnedOn;
public static Action OnBluetoothTurnedOff;
public static Action<string, int> OnTapped;
event Action<string, int, int, bool> OnMouseInputReceived; 
public static Action<string, string> OnTapConnected;
public static Action<string> OnTapDisconnected;
event Action<string, TapAirGesture> OnAirGestureInputReceived;
event Action<string, bool> OnTapChangedAirGestureState;
event Action<string, RawSensorData> OnRawSensorDataReceived;

```

### OnBluetoothTurnedOn

Called whenever bluetooth state is turned on. pretty straight-forward... 

### OnBluetoothTurnedOff

Called whenver bouetooth state is turned off... You can use this function to alert the user for example.

### OnTapConnected(string identifier, string name. int fw)

Called when a TAP device is connected to the mobile device, sending the TAP identifier, it's display name and the firmware version of the TAP.
Each TAP device has an identifier (a unique string) to allow you to keep track of all the taps that are connected
(if for example you're developing a multiplayer game, you need to keep track of the players).
This identifier is used in the rest of the events actions.
* The TAP Plugin does NOT scan for TAP devices, therefor the user must pair the TAP device beforehand.

### OnTapDisconnected(string identifier)

Called when a TAP device is disconnected , sending the TAP identifier.

### OnTapInputReceived(string identifier, int combination)

This is where magic will happen.
This function will tell you which TAP was being tapped (identifier:String), and which fingers are tapped (combination:UInt8)
Combination is an integer, between 1 and 31.
It's binary form represents the fingers that are tapped.
The LSB is thumb finger, the MSB (bit number 5) is the pinky finger.
For example: if combination equls 3 - it's binary form is 00101,
Which means that the thumb and the middle fingers are tapped.
For your convenience, you can convert the binary format into fingers boolean array (explanation follows)

### OnMouseInputReceived(string identifier, int vx, int vy, bool isMouse)

This event describe the mouse movement of a TAP.
vx,vy are the velocities of the movement. These values can be multiplied by a constant to simulate "Mouse sensitivity" in your app.
isMouse is a boolean that determines if the movement is true movement or falsely detected by the TAP.

### OnAirGestureInputReceived(string tapIdentifier, TapAirGesture gesture)
This event is being called when the user is doing Air Gesture (with TAP version 2.0).
```c#
public enum TapAirGesture
{
    OneFingerUp = 2,
    TwoFingersUp = 3,
    OneFingerDown = 4,
    TwoFingersDown = 5,
    OneFingerLeft = 6,
    TwoFingersLeft = 7,
    OnefingerRight = 8,
    TwoFingersRight = 9,
    IndexToThumbTouch = 10,
    MiddleToThumbTouch = 11
}
```
### OnTapChangedState(string tapIdentifier, bool isAirGesture)
This event is being called when the user enter or leaves AirGesture State.

### OnRawSensorDataReceived(string tapIdentifier, RawSensorData data)
This event is being called when a TAP is in Raw Sensor Mode .
This event is being called at a rate of 200 calls / minute (data stream).

RawSensorData Object has a timestamp, type and an array points(x,y,z).
type is RawSensorDataType enum:


```c# 
void onRawSensorDataReceived(string tapIdentifier, RawSensorData data)
{
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
}
```

IMU is the Gyro and Accelerometer sensors in the thumb unit.
Device is the Accelerometers sensors for each finger (Thumb, Index, Middle, Ring, Pinky).

## Converting a binary combination to fingers array

As said before, the tapped combination is an integer. to convert it to array of booleans:

```
bool[] fingers = TapCombination.toFingers (combination);
```

While:
fingers[0] indicates if the thumb is being tapped.
fingers[1] indicates if the index finger is being tapped.
fingers[2] indicates if the middle finger is being tapped.
fingers[3] indicates if the ring finger is being tapped.
fingers[4] indicates if the pinky finger is being tapped.

## TAPInputMode

Each TAP has a mode in which it works as.
Four modes available: 
CONTROLLER MODE (Default) 
    allows receiving the "tapped" and "moused" func callbacks in TAPKitDelegate with the fingers combination without any post-processing.
    
TEXT MODE 
    the TAP device will behave as a plain bluetooth keyboard, "tapped" and "moused" funcs in TAPKitDelegate will not be called.
CONTROLLER MODE WITH MOUSE HiD 
    Same as controller mode but allows the user to use the mouse also as a regular mouse input.
    Starting iOS 13, Apple added Assitive Touch feature. (Can be toggled within accessibility settings on iPhone).
    This adds a cursor to the screen that can be navigated using TAP device. 

RAW SENSOR DATA MODE
    This will stream the sensors (Gyro and Accelerometer) values. More or that later ...

When a TAP device is connected it is by default set to controller mode.

Changing tap mode:

```c#
void StartControllerMode(string tapIdentifier);
void StartTextMode(string tapIdentifier);
void StartControllerWithMouseHIDMode(string tapIdentifier);
void StartRawSensorMode(string tapIdentifier, int deviceAccelerometerSensitivity, int imuGyroSensitivity, int imuAccelerometerSensitivity);
```

## Raw Sensor Mode

n raw sensors mode, the TAP continuously sends raw data from the following sensors:
    1. Five 3-axis accelerometers on each finger ring.
    2. IMU (3-axis accelerometer + gyro) located on the thumb (**for TAP Strap 2 only**).
        
```c#
void StartRawSensorMode(string tapIdentifier, int deviceAccelerometerSensitivity, int imuGyroSensitivity, int imuAccelerometerSensitivity);
```
When puting TAP in Raw Sensor Mode, the sensitivities of the values can be defined by the developer.
deviceAccelerometer refers to the sensitivities of the fingers' accelerometers. Range: 1 to 4.
imuGyro refers to the gyro sensitivity on the thumb's sensor. Range: 1 to 4.
imuAccelerometer refers to the accelerometer sensitivity on the thumb's sensor. Range: 1 to 5.
Use zero for default sensitivity value.

[For more information about raw sensor mode click here](https://tapwithus.atlassian.net/wiki/spaces/TD/pages/792002574/Tap+Strap+Raw+Sensors+Mode)

## Vibrations/Haptic

Send Haptic/Vibration to TAP devices.
```c# 
void Vibrate(string tapIdentifier, int[] durations);
```

durations: An array of durations in the format of haptic, pause, haptic, pause ... You can specify up to 18 elements in this array. The rest will be ignored.
Each array element is defined in milliseconds.

Example:
```c#
tapInputManager.Vibrate(tapIdentifier, new int[] { 500, 100, 500 });
```
Will send two 500 milliseconds haptics with a 100 milliseconds pause in the middle.


## Debug Logs

You can easily enable/disable debug logs for the plugin:

```
// .. code
TapInputManager.setDebugLogging (true);
// .. code
TapInputManager.setDebugLogging (false);
```

## Example

This repository contains a test script: TapInputTest.cs that shows how TapInputManager should be used.

## Support

Please refer to the issues tab! :)

# iOS Special Build Notes

After building for iOS, two things should be done in your xcode project:

1) In your target settings, under General, 'deployment target' should be 8.0
2) You should drag TAPKitUnityBridge.framework (from Frameworks/Plugins/iOS/TAP) to the 'embed frameworks' section, in your target settings, under 'general'.s
3) In Build Settings -> "ALWAYS AMBED SWIFT STANDARD LIBRARIES" = YES.

# Have Fun!





