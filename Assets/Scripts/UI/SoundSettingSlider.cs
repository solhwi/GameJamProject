using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingSlider : MonoBehaviour
{
	public void OnValueChangedBgmSlider()
    {
        float volume = GetComponent<Slider>().value;
        SoundManager.Instance.SetBGMVolume(volume);
    }

    public void OnValueChangeSfxSlider()
    {
        float volume = GetComponent<Slider>().value;
        SoundManager.Instance.SetSFXVolume(volume);
    }

}
