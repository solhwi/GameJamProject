using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScene : BaseScene
{
	protected override void Awake()
	{
		base.Awake();
	}

	protected override IEnumerator Start()
	{
		GameManager.Instance.Initialize();

		yield return base.Start();

		MapManager.Instance.Initialize();

		while(!MapManager.Instance.IsMapLoaded)
			yield return null;

		GameManager.GameStopTrigger();

		float delayTime = 1.0f;
		yield return new WaitForSeconds(delayTime); // 잠시 대기... 이 때 시네마틱 등이 필요할 수 있다.

		SpawnManager.Instance.Initialize();

		while (!SpawnManager.Instance.IsSpawnEnded)
			yield return null;

		GameManager.GameStartTrigger();
	}
}
