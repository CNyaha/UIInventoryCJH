using System.Collections;
using System.Collections.Generic;
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
        private set { }
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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            mainMenu = uiMainMenu;
            status = uiStatus;
            inventory = uiInventory;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    public void SetData()
    {
        Character player = new Character("¼ö°­»ý", 100, 5, 3, 3);

        
    }

}
