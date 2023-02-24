using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public static class Utils
{
    public static void UpdateFrameRate()
    {
        bool slow = false;
        //DeviceGeneration.iPhoneX removed
#if UNITY_IPHONE
         DeviceGeneration[] slowDevices = new DeviceGeneration[] { DeviceGeneration.iPhone3G,
            DeviceGeneration.iPhone3GS, DeviceGeneration.iPhone4, DeviceGeneration.iPhone4S,
            DeviceGeneration.iPhone5, DeviceGeneration.iPhone5C, DeviceGeneration.iPhone5S,
            DeviceGeneration.iPhone6, DeviceGeneration.iPhone6Plus, DeviceGeneration.iPhone6S,
            DeviceGeneration.iPhone6SPlus, DeviceGeneration.iPhone7, DeviceGeneration.iPhone7Plus,
            DeviceGeneration.iPhone8, DeviceGeneration.iPhoneSE1Gen, DeviceGeneration.iPhoneSE2Gen,
            DeviceGeneration.iPhone8Plus, DeviceGeneration.iPhoneXS,
            DeviceGeneration.iPadAir1, DeviceGeneration.iPadAir2, DeviceGeneration.iPadMini1Gen,
            DeviceGeneration.iPadMini2Gen, DeviceGeneration.iPad1Gen, DeviceGeneration.iPad2Gen,
            DeviceGeneration.iPad3Gen, DeviceGeneration.iPad4Gen, DeviceGeneration.iPad5Gen, DeviceGeneration.iPad6Gen,
            DeviceGeneration.iPad7Gen};

        for (int i = 0; i < slowDevices.Length; ++i)
        {
            if (Device.generation == slowDevices[i])
            {
                slow = true;
                break;
            }
        }
        Debug.Log("Device: " + Device.generation);
        Debug.Log("Slow: " + slow.ToString());
#elif UNITY_ANDROID
        if (SystemInfo.systemMemorySize < 500)
        {
            slow = true;
        }
        //Debug.Log("Device name: " + SystemInfo.deviceName + "; Device model: " + SystemInfo.deviceModel);
        //Debug.Log("Slow: " + slow.ToString());
#endif


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
    }
}