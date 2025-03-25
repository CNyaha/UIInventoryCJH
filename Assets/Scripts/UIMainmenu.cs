using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : MonoBehaviour
{
    public Button statButton;
    public Button inventoryButton;
    public PlayerClass playerClass;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI currentGold;
    public TextMeshProUGUI exp;
    public Image expBar;

    private void Awake()
    {
        if (statButton != null)
        {
            statButton.onClick.AddListener(OnStatButton);
        }
        if (inventoryButton != null)
        {
            inventoryButton.onClick.AddListener(OnInventoryButton);
        }
    }

    public void SetCharacterInfo(Character character)
    {
        playerClass = character.UserClass;
        playerName.text = character.PlayerName;
        currentLevel.text = character.Level.ToString();
        currentGold.text = character.Gold.ToString();
        exp.text = $"{character.CurrentExp}/{character.MaxExp}";
        expBar.fillAmount = character.CurrentExp / character.MaxExp;
}

    private void OnStatButton()
    {
        UIManager.Instance.mainMenu.gameObject.SetActive(false);

        UIManager.Instance.status.gameObject.SetActive(true);
    }

    public void OnMainMenuReturn()
    {
        UIManager.Instance.inventory.gameObject.SetActive(false);
        UIManager.Instance.status.gameObject.SetActive(false);

        UIManager.Instance.mainMenu.gameObject.SetActive(true);
        Debug.Log("메인메뉴 부르기 완료");
    }

    private void OnInventoryButton()
    {
        UIManager.Instance.mainMenu.gameObject.SetActive(false);

        UIManager.Instance.inventory.gameObject.SetActive(true);
    }
}
