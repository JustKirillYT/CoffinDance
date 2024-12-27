using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel; // ������ ���������
    [SerializeField] private GameObject inventorySlotPrefab; // ������ ����� ���������

    public static PlayerInventory Instance { get; private set; }
    private List<InventoryObject> inventoryItems = new List<InventoryObject>(); // ������ ��������� ���������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���������� ������ ��� ����� �����
            Debug.Log("PlayerInventory ������ � ��������.");
        }
        else if (Instance != this)
        {
            Debug.Log("PlayerInventory ��� ����������. ����������� ������ �������.");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"����� {scene.name} ���������. ���������� ���������: {GameDataManager.PlayerInventory.Count}");

        // ��������������� ������ �� ������ ���������
        inventoryPanel = GameObject.Find("InventoryPanel").transform;

        DisplayInventory();
    }

    public void AddItem(InventoryObject item)
    {
        if (item.ItemPrefab == null)
        {
            // ������ �� �����, �������� ��������� �� ��������
            item.ItemPrefab = Resources.Load<GameObject>($"Assets/Prefabs/�������������������");
            if (item.ItemPrefab == null)
            {
                Debug.LogWarning($"Prefab ��� �������� {item.ItemName} �� ������ � Resources!");
            }
        }

        GameDataManager.PlayerInventory.Add(item);
        SaveInventory();

        Debug.Log($"�������� ������� {item.ItemName}. ������: {(item.ItemPrefab != null ? item.ItemPrefab.name : "�� ������")}.");
    }

    // ���������� �������� �� �����
    private void AddItemToScene(InventoryObject item)
    {
        // ���������, ��� Prefab �����
        if (item.ItemPrefab == null)
        {
            Debug.LogError($"Prefab ��� �������� {item.ItemName} �� ������!");
            return;
        }

        // ������� OrderAndCreateCanvasUI
        GameObject canvas = GameObject.Find("OrderAndCreateCanvasUI");
        if (canvas == null)
        {
            Debug.LogError("OrderAndCreateCanvasUI �� ������!");
            return;
        }

        // �������� ������ ��� �������� ������� Canvas
        GameObject itemOnScene = Instantiate(item.ItemPrefab, canvas.transform);

        // ���������, ��� ������ ��������� �������� � UI
        RectTransform rectTransform = itemOnScene.GetComponent<RectTransform>();
        itemOnScene.name = item.ItemName;
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero; // ������� ������� �� ������
            rectTransform.localScale = Vector3.one; // ������� �� ���������
        }
        else
        {
            Debug.LogWarning("� ���������� ������� ����������� RectTransform!");
        }

        // ������� ������� �� ���������
        GameDataManager.PlayerInventory.Remove(item);
        Debug.Log($"������� {item.ItemName} ������� �������� � OrderAndCreateCanvasUI � ����� �� ���������.");
    }

    public void LoadInventory()
    {
        int itemCount = PlayerPrefs.GetInt("InventoryCount", 0);
        GameDataManager.PlayerInventory.Clear();

        for (int i = 0; i < itemCount; i++)
        {
            string itemName = PlayerPrefs.GetString($"Item_{i}_Name");
            string itemSprite = PlayerPrefs.GetString($"Item_{i}_Sprite");
            int itemCost = PlayerPrefs.GetInt($"Item_{i}_Cost");

            InventoryObject item = new InventoryObject
            {
                ItemName = itemName,
                ItemSprite = Resources.Load<Sprite>(itemSprite), // ���� ����������� ������� �� ��������
                ItemCost = itemCost,

            };
            GameDataManager.PlayerInventory.Add(item);
        }

        Debug.Log($"��������� ��������. ���������: {GameDataManager.PlayerInventory.Count}");
        DisplayInventory();
    }
    private void OnUseItem(InventoryObject item)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "GameScene")
        {
            Debug.Log($"������ ������ ��� ��������: {item.ItemName} �� ����� {currentSceneName}");

            if (item.ItemPrefab != null)
            {
                AddItemToScene(item);
            }
            else
            {
                Debug.LogError($"Prefab ��� �������� {item.ItemName} �� ������!");
            }
        }
        else if (currentSceneName == "CreateCeremonyScene")
        {
            Debug.Log($"���������� ��������� �� ��������� ��������� �� ����� {currentSceneName}.");
        }
        else
        {
            Debug.LogWarning($"����������� �����: {currentSceneName}. �������� ��������.");
        }
    }
    public void SaveInventory()
    {
        PlayerPrefs.SetInt("InventoryCount", inventoryItems.Count);
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            PlayerPrefs.SetString($"Item_{i}_Name", inventoryItems[i].ItemName);
            PlayerPrefs.SetString($"Item_{i}_Sprite", inventoryItems[i].ItemSprite.name); // ���������� ����� �������
            PlayerPrefs.SetFloat($"Item_{i}_Cost", inventoryItems[i].ItemCost);
        }
        PlayerPrefs.Save(); // ��������� ���������
        Debug.Log($"��������� ��������. � �� {inventoryItems.Count} ���������");
    }

    public void ClearInventory()
    {
        inventoryItems.Clear();
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void DisplayInventory()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in GameDataManager.PlayerInventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            slot.transform.Find("ItemImage").GetComponent<Image>().sprite = item.ItemSprite;

            Button useButton = slot.transform.Find("ItemImage").GetComponent<Button>();
            if (useButton != null && item != null)
            {
                useButton.onClick.RemoveAllListeners();
                useButton.onClick.AddListener(() => OnUseItem(item));
            }
            else
            {
                Debug.LogWarning("������ ��� ������� �� �������!");
            }
        }
    }

    public List<InventoryObject> GetItems()
    {
        return inventoryItems;
    }
}
