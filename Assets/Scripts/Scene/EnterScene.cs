using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScene : BaseScene
{
	protected override IEnumerator Start()
	{
		yield return base.Start();

		// �켱 �ʿ��� �Ŵ����� Initialize
		ResourceManager.Instance.Initialize();

		while (!ResourceManager.Instance.IsReadyResource)
			yield return null;

		SoundManager.Instance.Initialize();

		SceneManager.Instance.ChangeScene(SceneManager.ENUM_SCENE.Main);
	}
}
