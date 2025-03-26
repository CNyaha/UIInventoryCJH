using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
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

        button.onClick.AddListener(OnItemUseImage);
    }

    private void Start()
    {
        inventory = UIManager.Instance.inventory;
        equipImage.gameObject.SetActive(false);
        quantityText.gameObject.SetActive(false);
    }

    // 아이템 아이콘과 갯수 세팅
    public void Set()
    {
        icon.sprite = item.icon;

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

    // 아이템 슬롯 비우기
    public void Clear()
    {
        item = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
        quantityText.gameObject.SetActive(false);
        equipImage.gameObject.SetActive(false);
    }

    // 슬롯 활성화
    public void ActivateSlot()
    {
        gameObject.SetActive(true);
    }

    // 슬롯을 클릭했을 때 나오는 이미지(사용 or 장착 or 장착해제 , 버리기)
    // 만약 화면을 벗어난다면 마우스 왼쪽방향에 나오게 설정
    public void OnItemUseImage()
    {
        if (item == null) return;
        Vector2 mousePos = Input.mousePosition;
        

        if (mousePos.x + 100 > Screen.width)
        {
            mousePos.x -= 60;
        }
        else
        {
            mousePos.x += 60;
        }

        inventory.itemSlotClickImage.transform.position = mousePos;
        inventory.itemSlotClickImage.SetActive(!inventory.itemSlotClickImage.activeSelf);

        inventory.selectItem = this;
        inventory.SelectItem(index);

    }

}
