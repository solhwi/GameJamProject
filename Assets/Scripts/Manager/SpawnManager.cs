using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
	public bool IsSpawnEnded
	{
		get;
		private set;
	} = false;
}
