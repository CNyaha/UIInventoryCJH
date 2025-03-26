using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private UIMainmenu uiMainMenu;
    [SerializeField]
    private UIStatus uiStatus;
    [SerializeField]
    private UIInventory uiInventory;


    public UIMainmenu mainMenu { get; private set; }
    public UIStatus status { get; private set; }
    public UIInventory inventory { get; private set; }


    private void Awake()
    {


        if (Instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        mainMenu = uiMainMenu;
        status = uiStatus;
        inventory = uiInventory;

        if (mainMenu == null || status == null || inventory == null)
        {
            Debug.LogError("UI들이 등록되지 않았습니다.");
            return;
        }

        mainMenu.OnMainMenuReturn();

    }

    private void Start()
    {


    }


}
