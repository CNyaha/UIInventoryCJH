using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public UIInventory inventory;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public Image equipImage;

    public int index;
    public bool equipped = false;
    public int quantity;

    private void Awake()
    {
        button = GetComponent<Button>();
        icon = transform.Find("Icon").GetComponent<Image>();
        quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        equipImage = transform.Find("EquipImage").GetComponent<Image>();
    }

    private void Start()
    {
        inventory = UIManager.Instance.inventory;
        equipImage.gameObject.SetActive(false);
        quantityText.gameObject.SetActive(false);
    }

    public void Set()
    {
        icon = item.icon;

        if (quantity > 1)
        {
            quantityText.text = quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }
        else
        {
            quantityText.text = string.Empty;
            quantityText.gameObject.SetActive(false);
        }

        if (quantity <= 0)
        {
            Clear();
        }


        equipImage.gameObject.SetActive(equipped);
        
    }

    public void Clear()
    {
        item = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
        quantityText.gameObject.SetActive(false);
        equipImage.gameObject.SetActive(false);
    }

    public void ActivateSlot()
    {
        gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
