using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이지 않으며, 주기적으로 탄을 발사
/// </summary>

[System.Serializable]
public enum UpgradeType
{
	DamageUp = 0,
	SpeedUp = 1,
	BulletIncrease = 2,
}

public class FriendlyUnit : ActiveObject
{
	public float cooltime = 0.0f;

	private Dictionary<UpgradeType, int> currLevelDictionaryByUpgradeType = new Dictionary<UpgradeType, int>()
	{
		{UpgradeType.DamageUp, 1 },
		{UpgradeType.SpeedUp, 1},
		{UpgradeType.BulletIncrease, 1 }
	};

	public override void Initialize(CollisionType type, bool isTrigger = true)
	{
		base.Initialize(type, isTrigger);

		tagType = ENUM_TAG_TYPE.Friendly;
	}

	public override void Idle(CommandParam param = null)
	{
		base.Idle(param);
	}

	public override void Attack(CommandParam param)
	{
		if(cooltime > 0.0f)
			return;

		base.Attack(param);
		cooltime = 2.0f;

		var g = ResourceManager.Instance.Load<GameObject>("Prefabs/Bullet");
		g = Instantiate(g);
		var bullet = g.GetComponent<Bullet>();

		int level = currLevelDictionaryByUpgradeType[UpgradeType.DamageUp];
		var data = ResourceManager.Instance.GetFriendlyDataByLevel(level);
		if (data == null)
			return;

		if(currLevelDictionaryByUpgradeType[UpgradeType.BulletIncrease] > 1)
		{
			bullet.TrippleShot(data.damage);
		}
		else
		{
			bullet.Shot(data.damage);
		}

		UnitManager.Instance.ExecuteCommand(this, ObjectStatus.Idle);
	}

	protected void Update()
	{
		int level = currLevelDictionaryByUpgradeType[UpgradeType.SpeedUp];

		var data = ResourceManager.Instance.GetFriendlyDataByLevel(level);
		if (data == null)
			return;
 
		cooltime -= Time.deltaTime * data.attackSpeed;
	}

	public void Upgrade(UpgradeType type)
	{
		int level = currLevelDictionaryByUpgradeType[type];

		if (level >= 3)
			return;

		var data = ResourceManager.Instance.GetFriendlyDataByLevel(level);
		if (data == null)
			return;
		
		if(GameManager.CurrGold > data.price)
		{
			GameManager.Instance.AddGold(-data.price);
		}

		currLevelDictionaryByUpgradeType[type]++;
	}
}
