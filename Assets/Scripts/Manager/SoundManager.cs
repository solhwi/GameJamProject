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

	}

	private Dictionary<SFXTag, AudioClip> sfxDictionary = new Dictionary<SFXTag, AudioClip>()
	{

	};

	private Dictionary<BGMTag, AudioClip> bgmDictionary = new Dictionary<BGMTag, AudioClip>()
	{

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
	SoundPack soundPack = new SoundPack();

	private float sfxVolume = 0.0f;
	private float bgmVolume = 0.0f;

	AudioSource bgmSource = null;

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
	}
	protected override void OnAwakeInstance()
	{

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

		if(clip != null)
		{
			bgmSource.clip = clip;
			bgmSource.volume = bgmVolume;
			bgmSource.Play();
		}
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
