using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
	private MapRoot currMapRoot = null;
	public bool IsMapLoaded
	{
		get;
		private set;
	} = false;

	SceneManager.ENUM_SCENE_MAP currMap;

	public void LoadMap(SceneManager.ENUM_SCENE_MAP map)
	{
		currMap = map;
		SceneManager.Instance.LoadSceneMap(map, OnLoadMap);
	}

	private void OnLoadMap()
	{
		currMapRoot = FindObjectOfType<MapRoot>();

		if (currMapRoot == null)
		{
			Debug.LogError(currMapRoot);
			return;
		}
			
		currMapRoot.transform.SetParent(transform);
		currMapRoot.transform.position = new Vector3(0, 1, 0);

		SceneManager.Instance.UnloadSceneMap(currMap);

		IsMapLoaded = true;
	}
}
