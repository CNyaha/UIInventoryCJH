using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UIStatus : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI intelligenceText;
    public TextMeshProUGUI criticalCanceText;
    public TextMeshProUGUI criticalDamageText;

    public void SetCharacterInfo(Character character)
    {
        damageText.text = $"공격력\n{character.Damage}";
        defenceText.text = $"방어력\n{character.Defence}";
        healthText.text = $"체력\n{character.MaxHp}/{character.CurrentHP}";
        strengthText.text = $"힘\n{character.Strength}";
        agilityText.text = $"민첩\n{character.Agility}";
        intelligenceText.text = $"지능\n{character.Intelligence}";
        criticalCanceText.text = $"치명타 확률\n{character.CriticalChance}%";
        criticalDamageText.text = $"크리티컬 데미지\n{character.CriticalDamage}%";
    }


}
