using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDetection : MonoBehaviour
{
    public static DeviceDetection instance;

    public bool isMobilePlatform;
    void Start()
    {
        isMobilePlatform = Application.isMobilePlatform;
    }

    void Update()
    {
        /*        #if UNITY_WEBGL
                    Debug.LogError("Running on WebGL.");
                #if UNITY_IOS || UNITY_ANDROID
                    Debug.LogError("WebGL on a Mobile platform.");
                #else
                    Debug.LogError("WebGL on a Desktop platform.");
                #endif

                #else
                        Debug.LogError("Not running on WebGL.");
                #endif*/

        //Debug.LogError("Is it mobile" + Application.isMobilePlatform);

        if (Application.isMobilePlatform) // is mobile
        {
            //force landscape
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            Debug.LogError("orientation: " + Screen.orientation);
        }
    }
}
