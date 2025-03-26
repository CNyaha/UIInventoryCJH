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
    public int maxSlots = 40;   //슬롯의 최대 숫자
    public GameObject slotPrefab;     // 슬롯 프리팹
    public Transform slotPanel; // 슬롯들을 배치할 위치
    public List<Slot> slots;    // 슬롯들을 넣을 리스트

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

    [Header("Item Pool")]
    public List<Item> equipmentItemPool;
    public GameObject randomEquipItemButton;


    private void Awake()
    {
        InitInventoryUI();
    }

    public void InitInventoryUI()
    {
        // 아이템 창 띄울 정보
        //selectItemIcon = transform.Find("InfoBG/InfoIcon").GetComponent<Image>();
        //selectItemName = transform.Find("InfoBG/ItemName").GetComponent<TextMeshProUGUI>();
        //selectItemDescription = transform.Find("InfoBG/Description").GetComponent<TextMeshProUGUI>();
        //selectItemStatName = transform.Find("InfoBG/StatName").GetComponent<TextMeshProUGUI>();
        //selectItemStatValue = transform.Find("InfoBG/StatValue").GetComponent<TextMeshProUGUI>();

        slotPanel = transform.Find("Image/Scroll View/Viewport/Slots").transform;
        itemSlotClickImage = transform.Find("ItemSlotClickImage").gameObject;



        randomEquipItemButton = transform.Find("RandomWeaponButton").gameObject;
        useButton = transform.Find("ItemSlotClickImage/UseButton").gameObject;
        equipButton = transform.Find("ItemSlotClickImage/EquipButton").gameObject;
        unEquipButton = transform.Find("ItemSlotClickImage/UnEquipButton").gameObject;
        dropButton = transform.Find("ItemSlotClickImage/DropButton").gameObject;

        useButton.GetComponent<Button>().onClick.AddListener(OnUseButton);
        //dropButton.GetComponent<Button>().onClick.AddListener(OnDropButton);
        equipButton.GetComponent<Button>().onClick.AddListener(OnEquipButton);
        unEquipButton.GetComponent<Button>().onClick.AddListener(OnUnEquipButton);
        randomEquipItemButton.GetComponent<Button>().onClick.AddListener(OnRandomEquipAquireButton);


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
        // 같은 아이템 타입인지 확인 후 같다면 해당 아이템을 착용하고 기존에 있던 아이템의 장착을 해제


        selectItem.equipped = true;
        selectItem.equipImage.gameObject.SetActive(true);
        GameManager.Instance.player.Equip(selectItem);

        UpdateUI();
        itemSlotClickImage.SetActive(false);

    }

    public void OnUnEquipButton()
    {
        UnEquip();
        UpdateUI();
        itemSlotClickImage.SetActive(false);
    }

    public void OnRandomEquipAquireButton()
    {
        if (equipmentItemPool.Count > 0)
        {
            int random = Random.Range(0, equipmentItemPool.Count);
            Item selectedItemData = equipmentItemPool[random];
            Item newItem = new Item(selectedItemData.itemName, selectedItemData.itemDescription, selectedItemData.icon, selectedItemData.type, selectedItemData.canStack
                , selectedItemData.maxStackAmount, selectedItemData.consumables, selectedItemData.itemEquipData);

            AddItem(newItem, 1);
            UpdateUI();
        }
    }

    void UnEquip()
    {
        selectItem.equipped = false;
        selectItem.equipImage.gameObject.SetActive(false);
        GameManager.Instance.player.UnEquip(selectItem);

        UpdateUI();
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
            newSlot.index = slots.Count;
            slots.Add(newSlot);
            return newSlot;
        }
        else
        {
            // 인벤토리가 가득찼다는 메시지 띄우기
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

        //아이템 설명창
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
