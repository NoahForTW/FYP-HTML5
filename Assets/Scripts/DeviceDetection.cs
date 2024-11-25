using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Application.isMobilePlatform) // is mobile
        {
            //force landscape
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = !Screen.fullScreen;
        }
    }

    // Update is called once per frame
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

    }
}
