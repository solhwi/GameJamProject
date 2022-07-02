using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
	protected override void Awake()
	{
		base.Awake();

		// �켱 �ʿ��� �Ŵ����� Initialize
		ResourceManager.Instance.Initialize();
		SoundManager.Instance.Initialize();
	}
}
