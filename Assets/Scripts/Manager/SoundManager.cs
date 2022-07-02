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

	public enum SoundStatus
	{
		None = 0, // ²¨Á® ÀÖÀ½
		Play = 1, // ÇÃ·¹ÀÌ Áß
		Change = 2, // À½¾Ç ¹Ù²î´Â Áß
		Exit = 3, // À½¾Ç ²¨Áö´Â Áß
	}

	SoundStatus currSoundStatus = SoundStatus.None;

	protected override void OnAwakeInstance()
	{
		
	}
}
