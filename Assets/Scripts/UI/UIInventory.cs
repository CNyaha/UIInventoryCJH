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

    // ������ ������ ������ �� ��ư
    [Header("Select Item")]
    public Slot selectItem;
    public Image selectItemIcon;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;
    public GameObject itemSlotClickImage;

    // ������ Ǯ
    [Header("Item Pool")]
    public List<Item> equipmentItemPool;
    public GameObject randomEquipItemButton;


    private void Awake()
    {
        InitInventoryUI();
    }

    // �κ��丮 ���ʼ���
    public void InitInventoryUI()
    {

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

    // ������ �߰�
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

    // ��� ������ ��ư
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


    // ���� ������ ��ư
    public void OnEquipButton()
    {
        // ���� ������ Ÿ������ Ȯ�� �� ���ٸ� �ش� �������� �����ϰ� ������ �ִ� �������� ������ ����


        selectItem.equipped = true;
        selectItem.equipImage.gameObject.SetActive(true);
        GameManager.Instance.player.Equip(selectItem);

        UpdateUI();
        itemSlotClickImage.SetActive(false);

    }


    //���� ���� ������ ��ư
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

    // ���� ����
    void UnEquip()
    {
        selectItem.equipped = false;
        selectItem.equipImage.gameObject.SetActive(false);
        GameManager.Instance.player.UnEquip(selectItem);

        UpdateUI();
    }

    // ����ִ� ������ ã�� ���� ����ִ� ������ ���ٸ� ������ ����
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
            // �κ��丮�� ����á�ٴ� �޽��� ����
        }

        return null;
    }

    // UI������Ʈ
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

    // ������ ������ ��ø��Ű�� �޼���
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

    // ������ ������(���콺�� ������ Ŭ���� ��)
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

    // ������ ������â ���� �ʱ�ȭ
    public void ClearSelectItemWindow()
    {
        selectItem = null;


        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    // ������ ����
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
