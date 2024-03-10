using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundWindow : MonoBehaviour
{
	private float originalBGMVolume = 0.5f;
	private float originalEffectVolume = 0.5f;
	[SerializeField] private Slider BGMSlider;
	[SerializeField] private Slider EffectSlider;

	private void OnEnable()
	{
		originalBGMVolume = BGMSlider.value;
		originalEffectVolume = EffectSlider.value;
	}

	private void Update()
	{
		if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
		{
			gameObject.SetActive(false);
		}
	}

	public void ClickApplyButton()
	{
        SoundManager.instance.PlaySound("Apply");
        gameObject.SetActive(false);
	}

	public void ClickCancelButton()
	{
		BGMSlider.value = originalBGMVolume;
		EffectSlider.value = originalEffectVolume;
        SoundManager.instance.PlaySound("Cancel");
        gameObject.SetActive(false);
	}
}
