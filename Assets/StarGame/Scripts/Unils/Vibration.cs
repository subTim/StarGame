
public static class Vibration
{
    public static void Vibrate()
    {
#if UNITY_ANDROID
        //if (UserData.Instance.VibrationOn)
        //{
            //Taptic.Light();
            Taptic.Medium();
        //}
#else
        if (UserData.Instance.VibrationOn)
        {
            Lofelt.NiceVibrations.HapticPatterns.PlayEmphasis(amplitude, frequency);
            //Lofelt.NiceVibrations.HapticPatterns.PlayPattern(Lofelt.NiceVibrations.HapticPatterns.Medium);
        }
#endif
    }
}
