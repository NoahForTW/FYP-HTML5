using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    // [DllImport("__Internal")]
    // private static extern void DetectDeviceInfo();

    // private void Start()
    // {
    //     #if UNITY_WEBGL && !UNITY_EDITOR
    //     DetectDeviceInfo(); // Call the JavaScript function
    //     #else
    //     Debug.LogError("Device detection only works in a WebGL build.");
    //     #endif
    // }

    // // Called from JavaScript
    // public void OnDeviceInfoDetected(string deviceType)
    // {
    //     Debug.LogError($"Device detected: {deviceType}");
    //     // Add any specific actions based on the device type here
    // }
    void Update()
    {
        #if UNITY_WEBGL
            Debug.LogError("Running on WebGL.");

            #if UNITY_IOS || UNITY_ANDROID
                Debug.LogError("WebGL on a Mobile platform.");
            #else
                Debug.LogError("WebGL on a Desktop platform.");
            #endif

        #else
            Debug.LogError("Not running on WebGL.");
        #endif
    }

}

