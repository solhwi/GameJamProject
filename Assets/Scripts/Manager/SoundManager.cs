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
		None = 0, // ���� ����
		Play = 1, // �÷��� ��
		Change = 2, // ���� �ٲ�� ��
		Exit = 3, // ���� ������ ��
	}

	SoundStatus currSoundStatus = SoundStatus.None;

	protected override void OnAwakeInstance()
	{
		
	}
}
