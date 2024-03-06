using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
	Slider slider;
	[SerializeField] TextMeshProUGUI volumeScale;
	[SerializeField] GameObject mute;
	[SerializeField] GameObject middle;
	[SerializeField] GameObject Loud;
	State state;

	private enum State
	{
		Mute,
		Quiet,
		Middle,
		Loud
	}

	private void Awake()
	{
		slider = GetComponentInChildren<Slider>();
		slider.value = 0.5f;
	}

	private void Start()
	{
		state = State.Middle;
	}

	private void Update()
	{
		int volume = (int)(slider.value * 100);
		volumeScale.text = $"{volume}";
		ChangeState(volume);
	}

	private void ChangeState(int volume)
	{
		switch (state)
		{
			case State.Mute:
				mute.SetActive(true);
				if (volume > 0)
				{
					mute.SetActive(false);
					state = State.Quiet;
				}
				break;
			case State.Quiet:
				if (volume == 0)
				{
					state = State.Mute;
				}
				else if (volume >= 40)
				{
					state = State.Middle;
				}
				break;

			case State.Middle:
				middle.SetActive(true);
				if (volume < 40)
				{
					middle.SetActive(false);
					state = State.Quiet;
				}
				else if (volume > 70)
				{
					state = State.Loud;
				}
				break;

			case State.Loud:
				Loud.SetActive(true);
				if (volume <= 70)
				{
					Loud.SetActive(false);
					state = State.Middle;
				}
				break;
		}
	}
}
