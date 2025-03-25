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
                Debug.LogError("exit Button�� ��ϵ��� �ʾҽ��ϴ�.");
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
