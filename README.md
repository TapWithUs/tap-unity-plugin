# TAP Plugin For Unity

## What Is This ?

TAP Plugin for Unity allows you to build a Unity app that can receive input from TAP devices,
In a way that each tap is being interpreted as an array or fingers that are tapped, or a binary combination integer (explanation follows), Thus allowing the TAP device to act as a controller for your app!

## Integration

This repository contains a ready-to-go test project. You can however download just the "Assets" folder and integrate it with yours.

## TapInputManager.cs

This is the main class. The only class you'll need to get your app communicating with TAP Devices.
The class contains 5 events:

```
public static Action OnBluetoothTurnedOn;
public static Action OnBluetoothTurnedOff;
public static Action<string, int> OnTapped;
public static Action<string, string> OnTapConnected;
public static Action<string> OnTapDisconnected;

```

### OnBluetoothTurnedOn

Called whenever bluetooth state is turned on. pretty straight-forward...

### OnBluetoothTurnedOff

Called whenver bouetooth state is turned off... You can use this function to alert the user for example.

### OnTapConnected(string identifier, string name)

Called when a TAP device is connected to the mobile device, sending the TAP identifier and it's display name.
Each TAP device has an identifier (a unique string) to allow you to keep track of all the taps that are connected
(if for example you're developing a multiplayer game, you need to keep track of the players).
This identifier is used in the rest of the events actions.
* The TAP Plugin does NOT scan for TAP devices, therefor the user must pair the TAP device beforehand.

### OnTapDisconnected(string identifier)

Called when a TAP device is disconnected , sending the TAP identifier.

### OnTapped(string identifier, int combination)

This is where magic will happen.
This function will tell you which TAP was being tapped (identifier:String), and which fingers are tapped (combination:UInt8)
Combination is an integer, between 1 and 31.
It's binary form represents the fingers that are tapped.
The LSB is thumb finger, the MSB (bit number 5) is the pinky finger.
For example: if combination equals 3 - it's binary form is 00101,
Which means that the thumb and the middle fingers were tapped.
For your convenience, you can convert the binary format into fingers boolean array (explanation follows)

## Subscribe to these events

```
// ... some cool code
TapInputManager.OnTapped += onTapped;
TapInputManager.OnBluetoothTurnedOn += onBluetoothOn;
TapInputManager.OnBluetoothTurnedOff += onBluetoothOff;
TapInputManager.OnTapConnected += onTapConnected;
TapInputManager.OnTapDisconnected += onTapDisconnected;
// ... some more cool code

void onTapped(string identifier, int combination) {
// ... some awsome code
}

void onTapConnected(string identifier, string name) {
}

void onTapDisconnected(string identifier) {
}

void onBluetoothOn() {
}

void onBluetoothOff() {
}

/// ...
```

## After subscribing to these events...

Right after subscribing to the events you desire, You should call:
```
TapInputManager.startTAPInputManager ();
```

This function tells TapInputManager to start it's engine.
Should be called once.. Usually in the main screen where you need the "tapConnected" callback

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
Two modes available:
CONTROLLER MODE (Default) - allows receiving the "tapped" action callback with the fingers combination without any post-processing.
TEXT MODE - the TAP device will behave as a plain bluetooth keyboard, "tapped" action will NOT be called.

When a TAP device is connected it is by default set to controller mode.

If you wish for a TAP to act as a bluetooth keyboard and allow the user to enter text input in your app, you can set the mode:


```
TapInputManager.setTextMode (identifier);
```

Just don't forget to switch back to controller mode after the user has entered the text :

```
TapInputManager.setControllerMode(identifier);
```

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

# iOS Special Notes

After building for iOS, two things should be done in your xcode project:

1) In your target settings, under General, 'deployment target' should be 8.0
2) You should drag TAPKitUnityBridge.framework (from Frameworks/Plugins/iOS/TAP) to the 'embed frameworks' section, in your target settings, under 'general'.s

# Have Fun!





