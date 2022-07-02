using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
	protected override void Awake()
	{
		base.Awake();

		// 우선 필요한 매니저들 Initialize
		ResourceManager.Instance.Initialize();
		SoundManager.Instance.Initialize();
	}
}
