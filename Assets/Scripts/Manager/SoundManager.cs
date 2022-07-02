using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPack
{
	public enum SFXTag
	{

	}

	public enum BGMTag
	{
		stage1 = 0,
		stage2 = 1,
		stage3 = 2,
		stage4 = 3,
		stage5 = 4,
	}

	private Dictionary<SFXTag, AudioClip> sfxDictionary = new Dictionary<SFXTag, AudioClip>()
	{

	};

	private Dictionary<BGMTag, AudioClip> bgmDictionary = new Dictionary<BGMTag, AudioClip>()
	{
		{ BGMTag.stage2, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage2}")},
		{ BGMTag.stage3, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage3}")},
		{ BGMTag.stage4, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage4}")},
	};

	public AudioClip GetBGMClip(BGMTag tag)
	{
		AudioClip clip;

		if (bgmDictionary.TryGetValue(tag, out clip))
		{
			return clip;
		}

		return null;
	}

	public AudioClip GetSFXClip(SFXTag tag)
	{
		AudioClip clip;

		if (sfxDictionary.TryGetValue(tag, out clip))
		{
			return clip;
		}

		return null;
	}
}

public class SoundManager : Singleton<SoundManager>
{
	private float sfxVolume = 0.0f;
	private float bgmVolume = 0.0f;

	SoundPack soundPack = null;
	AudioSource bgmSource = null;
	Coroutine bgmCoroutine = null;

	public enum SoundStatus
	{
		None = 0, // ²¨Á® ÀÖÀ½
		Play = 1, // ÇÃ·¹ÀÌ Áß
		Change = 2, // À½¾Ç ¹Ù²î´Â Áß
		Exit = 3, // À½¾Ç ²¨Áö´Â Áß
	}

	SoundStatus currBGMStatus = SoundStatus.None;

	public override void Initialize()
	{
		base.Initialize();

		if(bgmSource == null)
			bgmSource = gameObject.AddComponent<AudioSource>();

		soundPack = new SoundPack();
		PlayBGM(SoundPack.BGMTag.stage3);
	}

	public void PlaySFX(GameObject owner, SoundPack.SFXTag tag)
	{
		var audioSource = owner.GetComponent<AudioSource>();
		if (audioSource == null)
			audioSource = owner.AddComponent<AudioSource>();

		var clip = soundPack.GetSFXClip(tag);

		if(clip != null)
		{
			audioSource.volume = sfxVolume;
			audioSource.PlayOneShot(clip);
		}
	}

	public void PlayBGM(SoundPack.BGMTag tag)
	{
		var clip = soundPack.GetBGMClip(tag);

		if (clip != null)
		{
			bgmSource.clip = clip;
			bgmSource.loop = true;
			bgmSource.volume = bgmVolume;
			bgmSource.Play();
		}
	}

	public void ChangeBGM(SoundPack.BGMTag tag)
	{
		var clip = soundPack.GetBGMClip(tag);
		bgmCoroutine = StartCoroutine(OnChangeBGMCoroutine(clip));
	}

	private IEnumerator OnChangeBGMCoroutine(AudioClip clip)
	{
		float delayTime = bgmVolume;

		while (delayTime > 0.0f)
		{
			yield return null;
			delayTime -= Time.deltaTime;
			bgmSource.volume = delayTime;
		}

		bgmSource.Stop();
		yield return null;

		while (delayTime < bgmVolume)
		{
			yield return null;
			delayTime += Time.deltaTime;
			bgmSource.volume = delayTime;
		}

		bgmSource.clip = clip;
		bgmSource.volume = delayTime;
		bgmSource.Play();
	}

	public void SetSFXVolume(float volume)
	{
		sfxVolume = volume;
	}

	public void SetBGMVolume(float volume)
	{
		bgmVolume = volume;
		bgmSource.volume = volume;
	}
}
