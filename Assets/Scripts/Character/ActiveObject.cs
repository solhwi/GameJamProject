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

	// �ε����� �ʰ� ���� �����ϴ� ��� ä��
	[SerializeField] Animator anim = null;

	public override void Init(CollisionType type)
	{
		base.Init(type);
		Idle(); // ���ۿ� Idle�� ����
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
