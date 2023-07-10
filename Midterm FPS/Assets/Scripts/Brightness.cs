using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessLayer layer;
    public PostProcessProfile brightness;
    AutoExposure exposure;
    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        if (PlayerPrefs.HasKey("brightness"))
            AdjustBrightness(PlayerPrefs.GetFloat("brightness"));
        else
            AdjustBrightness(brightnessSlider.value);
    }

    public void AdjustBrightness(float val)
    {
        if (val != 0)
        {
            exposure.keyValue.value = val;
            PlayerPrefs.SetFloat("brightness", val);
        }
        else
        {
            exposure.keyValue.value = 0.05f;
            PlayerPrefs.SetFloat("brightness", .05f);
        }
    }
}
