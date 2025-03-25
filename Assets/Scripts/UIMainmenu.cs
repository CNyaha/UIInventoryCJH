using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : MonoBehaviour
{
    public Button statButton;
    public Button inventoryButton;

    private void Start()
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

    private void OnStatButton()
    {

    }

    private void OnInventoryButton()
    {

    }
}
