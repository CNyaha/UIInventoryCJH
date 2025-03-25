using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("Slot")]
    public int maxSlots = 40;   //������ �ִ� ����
    public GameObject slotPrefab;     // ���� ������
    public Transform slotPanel; // ���Ե��� ��ġ�� ��ġ
    public List<Slot> slots;    // ���Ե��� ���� ����Ʈ

    [Header("Select Item")]
    public Slot selectItem;
    public Image selectItemIcon;
    //public TextMeshProUGUI selectItemName;
    //public TextMeshProUGUI selectItemDescription;
    //public TextMeshProUGUI selectItemStatName;
    //public TextMeshProUGUI selectItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;
    public GameObject itemSlotClickImage;

    private void Awake()
    {
        InitInventoryUI();
    }

    public void InitInventoryUI()
    {
        // ������ â ��� ����
        //selectItemIcon = transform.Find("InfoBG/InfoIcon").GetComponent<Image>();
        //selectItemName = transform.Find("InfoBG/ItemName").GetComponent<TextMeshProUGUI>();
        //selectItemDescription = transform.Find("InfoBG/Description").GetComponent<TextMeshProUGUI>();
        //selectItemStatName = transform.Find("InfoBG/StatName").GetComponent<TextMeshProUGUI>();
        //selectItemStatValue = transform.Find("InfoBG/StatValue").GetComponent<TextMeshProUGUI>();

        slotPanel = transform.Find("Image/Scroll View/Viewport/Content/Slots");
        itemSlotClickImage = transform.Find("ItemSlotClickImage").gameObject;



        useButton = transform.Find("ItemSlotClickImage/UseButton").gameObject;
        equipButton = transform.Find("ItemSlotClickImage/EquipButton").gameObject;
        unEquipButton = transform.Find("ItemSlotClickImage/UnEquipButton").gameObject;
        dropButton = transform.Find("ItemSlotClickImage/DropButton").gameObject;

        useButton.GetComponent<Button>().onClick.AddListener(OnUseButton);
        //dropButton.GetComponent<Button>().onClick.AddListener(OnDropButton);
        equipButton.GetComponent<Button>().onClick.AddListener(OnEquipButton);
        unEquipButton.GetComponent<Button>().onClick.AddListener(OnUnEquipButton);

        ClearSelectItemWindow();

        itemSlotClickImage.SetActive(false);

    }

    public void AddItem(Item item, int quantity)
    {

        for (int i = 0; i < quantity; i++)
        {

            if (item.canStack)
            {
                Slot slot = GetItemStack(item);
                if (slot != null)
                {
                    slot.quantity++;
                    continue;
                }
            }

            Slot emptySlot = GetEmptySlot();

            if (emptySlot != null)
            {
                emptySlot.item = item;
                emptySlot.quantity = 1;
                continue;
            }

        }

    }


    public void OnUseButton()
    {

        for (int i = 0; i < selectItem.item.consumables.Length; i++)
        {
            switch (selectItem.item.consumables[i].type)
            {
                case ConsumableType.Health:
                    //condition.Heal(selectItem.item.consumables[i].value);
                    break;

            }
        }
        RemoveSelectItem();

        itemSlotClickImage.SetActive(false);
    }

    public void OnEquipButton()
    {
        // ���� ������ Ÿ������ Ȯ�� �� ���ٸ� �ش� �������� �����ϰ� ������ �ִ� �������� ������ ����

        int saveSelectItemIndex = selectItem.index;

        selectItem.equipped = true;

        selectItem.index = saveSelectItemIndex;

        RemoveSelectItem();

        itemSlotClickImage.SetActive(false);
        UpdateUI();

    }

    public void OnUnEquipButton()
    {
        UnEquip();
        itemSlotClickImage.SetActive(false);
    }

    void UnEquip()
    {
        selectItem.equipped = false;
        Slot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            UpdateUI();
        }

    }

    public Slot GetEmptySlot()
    {
        Slot availableSlot = slots.Find(slot => !slot.gameObject.activeSelf);

        if (availableSlot != null)
        {
            availableSlot.ActivateSlot();
            return availableSlot;
        }

        if (slots.Count < maxSlots)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotPanel);
            Slot newSlot = slotObj.GetComponent<Slot>();
            newSlot.ActivateSlot();
            slots.Add(newSlot);
            return newSlot;
        }
        else
        {
            // �κ��丮�� ����á�ٴ� �޽��� ����
        }

        return null;
    }

    public void UpdateUI()

    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }

        //������ ����â
    }

    public Slot GetItemStack(Item data)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SelectItem(int index)
    {
        Slot sSlot;

        if (slots[index].item == null) return;

        sSlot = slots[index];

        useButton.SetActive(selectItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectItem.item.type == ItemType.Equipable && !sSlot.equipped);
        unEquipButton.SetActive(selectItem.item.type == ItemType.Equipable && sSlot.equipped);


        dropButton.SetActive(true);

    }

    public void ClearSelectItemWindow()
    {
        selectItem = null;

        //selectItemName.text = string.Empty;
        //selectItemDescription.text = string.Empty;
        //selectItemStatName.text = string.Empty;
        //selectItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    void RemoveSelectItem()
    {
        slots[selectItem.index].quantity--;

        if (slots[selectItem.index].quantity <= 0)
        {
            selectItem.item = null;
            ClearSelectItemWindow();
        }

        UpdateUI();
    }
}
