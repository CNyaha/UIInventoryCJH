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
        damageText.text = $"���ݷ�\n{character.Damage}";
        defenceText.text = $"����\n{character.Defence}";
        healthText.text = $"ü��\n{character.MaxHp}/{character.CurrentHP}";
        strengthText.text = $"��\n{character.Strength}";
        agilityText.text = $"��ø\n{character.Agility}";
        intelligenceText.text = $"����\n{character.Intelligence}";
        criticalCanceText.text = $"ġ��Ÿ Ȯ��\n{character.CriticalChance}%";
        criticalDamageText.text = $"ũ��Ƽ�� ������\n{character.CriticalDamage}%";
    }


}
