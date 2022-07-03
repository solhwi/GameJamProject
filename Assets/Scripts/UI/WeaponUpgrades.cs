using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    public void UpgradeWeapon(int type)
	{
		var unit = UnitManager.Instance.GetFriendlyUnit();

		if(unit != null)
		{
			unit.Upgrade((UpgradeType)type);
		}

		SoundManager.Instance.PlaySFX(this.gameObject, SoundPack.SFXTag.button);
	}

}
