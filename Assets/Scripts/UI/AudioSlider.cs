using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public GameObject icon;
    Slider slider;
    Image iconIMG;

    private void OnEnable()
    {
        iconIMG = icon.GetComponent<Image>();
        RefreshIcon();

        slider = GetComponent<Slider>();
        slider.value = AudioManager.instance.masterVolume * 10;
    }

    public void OnSliderValueChanged()
    {
        // TODO: save volume locally
        AudioManager.instance.MasterVolumeChanged(slider.value / 10);
        RefreshIcon();
    }

    private void RefreshIcon()
    {
        if (AudioManager.instance.masterVolume == 0) iconIMG.sprite = Resources.Load<Sprite>("AudioIcons/speaker-off");
        else { iconIMG.sprite = Resources.Load<Sprite>("AudioIcons/speaker"); }
    }
}
