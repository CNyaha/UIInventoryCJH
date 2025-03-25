using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (Instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetData();
    }

    public void SetData()
    {
        Character player = new Character(PlayerClass.SPARTASTUDENT, "코딩의 노예", 100, 5, 3, 3, 30, 10);

        UIManager.Instance.status.SetCharacterInfo(player);
    }
}
