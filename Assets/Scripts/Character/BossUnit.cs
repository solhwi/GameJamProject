using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : EnemyUnit
{
	public override void Init(CollisionType type)
	{
		base.Init(type);

		IsBoss = true;
	}
}
