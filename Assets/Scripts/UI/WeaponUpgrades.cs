using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    public void UpgradeWeapon(UpgradeType type)
	{
		var unit = UnitManager.Instance.GetFriendlyUnit();

		if(unit != null)
		{
			unit.Upgrade(type);
		}
	}
}
