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

public class DieParam : CommandParam
{

}

public enum ActiveObjectID
{
	punc = 1,
	pacman = 2,
	ghost = 3,
	dino = 4,
	tetris = 5,
	redmush = 6,
	star = 7,
	sonic = 8,
	kirby = 9,
	mstrball = 10,
	orgmush = 11,
	slime = 12,
	goblin = 13,
	zergling = 14,
	spaceship = 15,
	grnmush = 16,
	wrecking = 17,
	among = 18,
	boss1 = 19,
	boss2 = 20,
	boss3 = 21,
	boss4 = 22,
	boss5 = 23,
}

public class ActiveObject : CollisionObject
{
	public enum ActiveStatus
	{
		Idle = 0,
		Move = 1,
		Attack = 2,
		Hit = 3,
		Die = 4,
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

	public virtual void Die(CommandParam param)
	{

	}
}
