using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 태그 타입, Unity의 태그는 사용하지 않을 예정
/// </summary>
/// 
[Serializable]
public enum ENUM_TAG_TYPE
{
	Untagged = 0,
	Interactable = 1,
	Friendly = 2,
	Enemy = 3,
	Tower = 4,
	Bullet = 5,
}

public abstract class CommandParam
{

}

public class AttackParam : CommandParam
{
	public CollisionObject target;
	public int damage;
}

public class MoveParam : CommandParam
{
	public int speed;
}

public class IdleParam : CommandParam
{

}

public class HitParam : CommandParam
{
	public int damage;
}

public class DieParam : CommandParam
{

}

public class RotateParam : CommandParam
{
	public Vector2 rotateVec;
}

public enum ObjectID
{
	punc = 1,
	pacman = 2,
	ghost = 3,
	dino = 4,
	tetris = 5,
	redmush = 6,
	star = 7,
	kirby = 9,
	mstrball = 10,
	orgmush = 11,
	slime = 12,
	goblin = 13,
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

public class CollisionObject : MonoBehaviour
{
	public ObjectID id;
	public ENUM_TAG_TYPE tagType = ENUM_TAG_TYPE.Untagged;

	public enum ObjectStatus
	{
		Idle = 0,
		Move = 1,
		Attack = 2,
		Hit = 3,
		Die = 4,
	}

	ObjectStatus currActiveStatus = ObjectStatus.Idle;

	public enum CollisionType
	{
		Box = 0,
		Capsule = 1,
		Polygon = 2,
	}

	private Rigidbody2D rigid = null;
	private Collider2D col = null;

	public bool IsBoss = false;
	protected int currHp = 0;

	public virtual void Initialize(CollisionType type)
	{
		if (col != null || rigid != null)
			return;

		switch(type)
		{
			case CollisionType.Box:
				col = gameObject.AddComponent<BoxCollider2D>();
				break;

			case CollisionType.Capsule:
				col = gameObject.AddComponent<CapsuleCollider2D>();
				break;

			case CollisionType.Polygon:
				col = gameObject.AddComponent<PolygonCollider2D>();
				break;
		}

		col.isTrigger = true;

		rigid = gameObject.AddComponent<Rigidbody2D>();

		rigid.drag = 0.0f;
		rigid.angularDrag = 0.0f;
		rigid.freezeRotation = true;
		rigid.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

		Idle(); // 시작엔 Idle로 세팅
	}

	public virtual void Idle(CommandParam param = null)
	{
		rigid.velocity = Vector2.zero;

		currActiveStatus = ObjectStatus.Idle;
	}

	public virtual void Move(CommandParam param)
	{
		if (tagType != ENUM_TAG_TYPE.Enemy)
			return;

		if (currActiveStatus == ObjectStatus.Die)
			return;

		int speed = 0;

		if(IsBoss)
		{
			var data = ResourceManager.Instance.GetBossData(id);
			speed = data.moveSpeed;
		}
		else
		{
			var data = ResourceManager.Instance.GetEnemyData(id);
			speed = data.moveSpeed;
		}
		
		rigid.MovePosition(Vector2.right * speed);

		currActiveStatus = ObjectStatus.Move;
	}

	public virtual void Attack(CommandParam param)
	{
		if (currActiveStatus == ObjectStatus.Die)
			return;

		currActiveStatus = ObjectStatus.Attack;
	}

	public virtual void Hit(CommandParam param)
	{
		var _param = param as HitParam;
		if (_param == null)
			return;

		currHp -= _param.damage;
		if (currHp < 0)
			currHp = 0;

		UnitManager.Instance.ExecuteCommand(this, ActiveObject.ObjectStatus.Die, new DieParam());

		currActiveStatus = ObjectStatus.Hit;
 	}

	public virtual void Die(CommandParam param)
	{
		if (currActiveStatus != ObjectStatus.Hit)
			return;
	}

	public virtual void Rotate(CommandParam param)
	{
		if (currActiveStatus == ObjectStatus.Die)
			return;

		var _param = param as RotateParam;
		Vector2 rotateVec = _param.rotateVec;

		transform.Rotate(rotateVec);	
	}
}
