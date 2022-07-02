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

	public void LoadMap(SceneManager.ENUM_SCENE_MAP map)
	{
		SceneManager.Instance.LoadSceneMap(map, OnLoadMap);
	}

	private void OnLoadMap()
	{
		currMapRoot = FindObjectOfType<MapRoot>();
		currMapRoot.transform.SetParent(transform);

		IsMapLoaded = true;
	}
}
