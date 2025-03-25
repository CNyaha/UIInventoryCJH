using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReturnButtonController : MonoBehaviour
{
    public Button exitButton;

    public void Awake()
    {
        if (exitButton == null)
        {

            exitButton = GetComponent<Button>();
            if (exitButton == null)
            {
                Debug.LogError("exit Button이 등록되지 않았습니다.");
                return;
            }
        }

        exitButton.onClick.AddListener(OnReturnButton);
    }

    public void OnReturnButton()
    {
        UIManager.Instance.mainMenu.OnMainMenuReturn();
    }

}
