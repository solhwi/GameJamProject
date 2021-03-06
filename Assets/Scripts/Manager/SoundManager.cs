using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPack
{
	public enum SFXTag
	{
		hit,
		die,
		button,
		shot,
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
		{ SFXTag.hit, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage3}/base_damage3")},
		{ SFXTag.die, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage3}/monsterdie_3")},
		{ SFXTag.button, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage3}/buttonsound_3")},
		{ SFXTag.shot, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage3}/shootgun_3")},
	};

	private Dictionary<BGMTag, AudioClip> bgmDictionary = new Dictionary<BGMTag, AudioClip>()
	{
		{ BGMTag.stage2, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage2}")},
		{ BGMTag.stage3, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage3}")},
		{ BGMTag.stage4, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage4}")},
		{ BGMTag.stage5, ResourceManager.Instance.Load<AudioClip>($"Sounds/{BGMTag.stage5}")},
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
		None = 0, // ???? ????
		Play = 1, // ?÷??? ??
		Change = 2, // ???? ?ٲ??? ??
		Exit = 3, // ???? ?????? ??
	}

	SoundStatus currBGMStatus = SoundStatus.None;

	public override void Initialize()
	{
		base.Initialize();

		if(bgmSource == null)
			bgmSource = gameObject.AddComponent<AudioSource>();

		soundPack = new SoundPack();
		PlayBGM(SoundPack.BGMTag.stage5);
		SetBGMVolume(1.0f);
		SetSFXVolume(1.0f);
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
