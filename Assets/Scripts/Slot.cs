using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public UIInventory inventory;

    public Button button;
    public Sprite icon;
    public TextMeshProUGUI quantityText;
    public Image equipImage;

    private void Awake()
    {
        button = GetComponent<Button>();
        icon = transform.Find("Icon").GetComponent<Sprite>();
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
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
