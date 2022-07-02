using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceManager : Singleton<ResourceManager>
{
	private readonly string URL = "";
	private DownloadHandler dataHandler = null;

	protected override IEnumerator Start()
	{
		yield return base.Start();

		UnityWebRequest request = UnityWebRequest.Get(URL);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
	}

	public T Load<T>(string path) where T : Object
	{
		return Resources.Load<T>(path);
	}

	public void Unload(Object obj)
	{
		Resources.UnloadAsset(obj);
	}
}
