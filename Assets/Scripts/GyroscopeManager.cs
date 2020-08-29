using UnityEngine;
using System.Collections;

public static class GyroscopeManager
{
    private static bool gyroInitialized = false;

    public static bool HasGyroscope
    {
        get
        {
            return SystemInfo.supportsGyroscope;
        }
    }


    public static float GetXFlatTilt()
    {
        if (!gyroInitialized)
        {
            InitGyro();
        }
        return HasGyroscope ? Input.gyro.gravity.x * 50f : 0f;
    }


    public static void InitGyro()
    {
        if (HasGyroscope)
        {
            Input.gyro.enabled = true;              // enable the gyroscope
            Input.gyro.updateInterval = 0.1267f;    // set the update interval to its highest value of 60 Hz
        }
    }
}
