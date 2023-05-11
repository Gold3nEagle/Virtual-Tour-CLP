using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
    }

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.instance.masterVolume * 10;
    }

    public void OnSliderValueChanged()
    {
        // TODO: save volume locally
        AudioManager.instance.MasterVolumeChanged(slider.value / 10);
    }
}
