using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScene : BaseScene
{
	protected override IEnumerator Start()
	{
		GameManager.Instance.Initialize();

		yield return base.Start();

		GameManager.GameStopTrigger();

		var currStage = GameManager.Instance.currStage;
		MapManager.Instance.LoadMap((SceneManager.ENUM_SCENE_MAP)(int)currStage);

		while(!MapManager.Instance.IsMapLoaded)
			yield return null;

		float delayTime = 1.0f;
		yield return new WaitForSeconds(delayTime); // 잠시 대기... 이 때 시네마틱 등이 필요할 수 있다.

		UnitManager.Instance.Spawn();

		GameManager.GameStartTrigger();

		while (!UnitManager.Instance.IsSpawnEnded)
			yield return null;
	}

}
