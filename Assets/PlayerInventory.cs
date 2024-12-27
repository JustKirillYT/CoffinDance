using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel; // Панель инвентаря
    [SerializeField] private GameObject inventorySlotPrefab; // Префаб слота инвентаря

    public static PlayerInventory Instance { get; private set; }
    private List<InventoryObject> inventoryItems = new List<InventoryObject>(); // Список предметов инвентаря

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать объект при смене сцены
            Debug.Log("PlayerInventory создан и сохранен.");
        }
        else if (Instance != this)
        {
            Debug.Log("PlayerInventory уже существует. Уничтожение нового объекта.");
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
        Debug.Log($"Сцена {scene.name} загружена. Количество предметов: {GameDataManager.PlayerInventory.Count}");

        // Перепривязываем ссылку на панель инвентаря
        inventoryPanel = GameObject.Find("InventoryPanel").transform;

        DisplayInventory();
    }

    public void AddItem(InventoryObject item)
    {
        if (item.ItemPrefab == null)
        {
            // Префаб не задан, пытаемся загрузить из ресурсов
            item.ItemPrefab = Resources.Load<GameObject>($"Assets/Prefabs/ПрефабОбъектаПустой");
            if (item.ItemPrefab == null)
            {
                Debug.LogWarning($"Prefab для предмета {item.ItemName} не найден в Resources!");
            }
        }

        GameDataManager.PlayerInventory.Add(item);
        SaveInventory();

        Debug.Log($"Добавлен предмет {item.ItemName}. Префаб: {(item.ItemPrefab != null ? item.ItemPrefab.name : "Не указан")}.");
    }

    // Добавление предмета на сцену
    private void AddItemToScene(InventoryObject item)
    {
        // Убедитесь, что Prefab задан
        if (item.ItemPrefab == null)
        {
            Debug.LogError($"Prefab для предмета {item.ItemName} не найден!");
            return;
        }

        // Найдите OrderAndCreateCanvasUI
        GameObject canvas = GameObject.Find("OrderAndCreateCanvasUI");
        if (canvas == null)
        {
            Debug.LogError("OrderAndCreateCanvasUI не найден!");
            return;
        }

        // Создайте объект как дочерний элемент Canvas
        GameObject itemOnScene = Instantiate(item.ItemPrefab, canvas.transform);

        // Убедитесь, что объект корректно размещён в UI
        RectTransform rectTransform = itemOnScene.GetComponent<RectTransform>();
        itemOnScene.name = item.ItemName;
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero; // Задайте позицию по центру
            rectTransform.localScale = Vector3.one; // Масштаб по умолчанию
        }
        else
        {
            Debug.LogWarning("У созданного объекта отсутствует RectTransform!");
        }

        // Удаляем предмет из инвентаря
        GameDataManager.PlayerInventory.Remove(item);
        Debug.Log($"Предмет {item.ItemName} успешно добавлен в OrderAndCreateCanvasUI и удалён из инвентаря.");
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
                ItemSprite = Resources.Load<Sprite>(itemSprite), // Если используете спрайты из ресурсов
                ItemCost = itemCost,

            };
            GameDataManager.PlayerInventory.Add(item);
        }

        Debug.Log($"Инвентарь загружен. Предметов: {GameDataManager.PlayerInventory.Count}");
        DisplayInventory();
    }
    private void OnUseItem(InventoryObject item)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "GameScene")
        {
            Debug.Log($"Нажата кнопка для предмета: {item.ItemName} на сцене {currentSceneName}");

            if (item.ItemPrefab != null)
            {
                AddItemToScene(item);
            }
            else
            {
                Debug.LogError($"Prefab для предмета {item.ItemName} не найден!");
            }
        }
        else if (currentSceneName == "CreateCeremonyScene")
        {
            Debug.Log($"Добавление предметов из инвентаря отключено на сцене {currentSceneName}.");
        }
        else
        {
            Debug.LogWarning($"Неизвестная сцена: {currentSceneName}. Действие отменено.");
        }
    }
    public void SaveInventory()
    {
        PlayerPrefs.SetInt("InventoryCount", inventoryItems.Count);
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            PlayerPrefs.SetString($"Item_{i}_Name", inventoryItems[i].ItemName);
            PlayerPrefs.SetString($"Item_{i}_Sprite", inventoryItems[i].ItemSprite.name); // Сохранение имени спрайта
            PlayerPrefs.SetFloat($"Item_{i}_Cost", inventoryItems[i].ItemCost);
        }
        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log($"Инвентарь сохранен. в нём {inventoryItems.Count} предметов");
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
                Debug.LogWarning("Кнопка или предмет не найдены!");
            }
        }
    }

    public List<InventoryObject> GetItems()
    {
        return inventoryItems;
    }
}
