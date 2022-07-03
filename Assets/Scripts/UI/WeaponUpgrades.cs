using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    public void UpgradeWeapon(int num)
	{
		var unit = UnitManager.Instance.GetUnit(num);

		if(unit != null)
		{
			unit.Upgrade(num);
		}
	}
}
