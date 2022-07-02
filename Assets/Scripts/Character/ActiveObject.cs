using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandParam
{
	
}

public class AttackParam : CommandParam
{

}

public class MoveParam : CommandParam
{

}

public class IdleParam : CommandParam
{

}

public class HitParam : CommandParam
{

}

public class ActiveObject : CollisionObject
{
	public enum ActiveStatus
	{
		Idle = 0,
		Move = 1,
		Attack = 2,
		Hit = 3
	}

	ActiveStatus currActiveStatus = ActiveStatus.Idle;

	// 로드하지 않고 직접 세팅하는 방법 채택
	[SerializeField] Animator anim = null;

	public override void Init(CollisionType type)
	{
		base.Init(type);
		Idle(); // 시작엔 Idle로 세팅
	}

	public virtual void Idle(CommandParam param = null)
	{

	}

	public virtual void Move(CommandParam param)
	{

	}

	public virtual void Attack(CommandParam param)
	{

	}

	public virtual void Hit(CommandParam param)
	{

	}
}
