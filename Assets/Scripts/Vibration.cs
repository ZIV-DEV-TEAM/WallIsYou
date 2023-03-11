using UnityEngine;

public class Vibration
{
    private bool _isVibrationOn = true;
    private const string Vibrate = "Vibrate";

    public void OnVibration(bool enable)
    {
        _isVibrationOn = enable;
        PlayerPrefs.SetInt(Vibrate, enable ? 1 : 0);
    }

    public Vibration()
    {
        if (PlayerPrefs.HasKey(Vibrate))
        {
            _isVibrationOn = PlayerPrefs.GetInt(Vibrate) != 0;
        }
    }

    public void TriggerVibrate()
    {
        if (!_isVibrationOn)
            return;
        Handheld.Vibrate();
    }
}