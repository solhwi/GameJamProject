using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
	[SerializeField] Image hpImage;

	private void Update()
	{
		PlayerHPbar();
	}

	public void PlayerHPbar()
	{
		int hp = UnitManager.Instance.MyTowerHp; //캐릭터 hp를 받아옴
		hpImage.fillAmount =  (float)hp / (float)UnitManager.Instance.MyTowerMaxHp;
	}
}
