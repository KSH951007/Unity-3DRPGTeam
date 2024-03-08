using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
	[SerializeField] private AudioMixer bgmMixer;
	[SerializeField] private AudioMixer sfxMixer;
	[SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
	private float bgmVolume;
	private float sfxVolume;

	// Update is called once per frame
	public void BGMVolumeControl()
    {
		bgmVolume = bgmSlider.value;

		bgmMixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
    }

	public void SFXVolumeControl()
	{
		sfxVolume = sfxSlider.value;

		sfxMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
	}
}
