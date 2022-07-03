using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    [SerializeField] Text goldText = null;

    // Update is called once per frame
    void Update()
    {
        goldText.text = GameManager.CurrGold.ToString();
    }
}
