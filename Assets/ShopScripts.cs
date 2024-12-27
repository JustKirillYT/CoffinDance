using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScripts : MonoBehaviour
{
    [SerializeField] public List<RectTransform> categoryPanels; // Панели категорий
    [SerializeField] public List<string> categoryNames; // Названия категорий
    [SerializeField] private GameObject shopItemPrefab; // Префаб товара
    [SerializeField] private List<ShopItemClass> items; // Все доступные товары
    [SerializeField] public PlayerInventory playerInventory; // Ссылка на инвентарь
    [SerializeField] public Order currentOrder; // Ссылка на текущий заказ

    private Dictionary<string, RectTransform> categoryPanelMap; // Словарь для сопоставления категорий с панелями

    private void Start()
    {
        categoryPanelMap = new Dictionary<string, RectTransform>();
        for (int i = 0; i < categoryPanels.Count; i++)
        {
            if (i < categoryNames.Count)
            {
                categoryPanelMap[categoryNames[i]] = categoryPanels[i];
            }
            else
            {
                Debug.LogWarning("Количество категорий и панелей не совпадает!");
            }
        }

        PopulateShop();
    }

    public void Update()
    {
        currentOrder = GameDataManager.CurrentOrder;
    }
    public void PopulateShop()
    {
        // Очищаем старые товары из панелей
        foreach (var panel in categoryPanels)
        {
            foreach (Transform child in panel)
            {
                Destroy(child.gameObject);
            }
        }

        // Создаём элементы для всех товаров
        foreach (var item in items)
        {
            int categoryIndex = categoryNames.IndexOf(item.Type);
            if (categoryIndex >= 0 && categoryIndex < categoryPanels.Count)
            {
                GameObject shopItem = Instantiate(shopItemPrefab, categoryPanels[categoryIndex]);
                ShopItemClass shopItemUI = shopItem.GetComponent<ShopItemClass>();

                if (shopItemUI != null)
                {
                    var itemNameText = shopItem.transform.Find("ItemName").GetComponent<Text>();
                    var itemPriceText = shopItem.transform.Find("ItemPrice").GetComponent<Text>();
                    var itemImage = shopItem.transform.Find("ItemImage").GetComponent<Image>();

                    itemNameText.text = item.Name;
                    itemPriceText.text = $"{item.Cost} $";
                    itemImage.sprite = item.sprite;

                    Button itemButton = itemImage.GetComponent<Button>();
                    if (itemButton != null)
                    {
                        itemButton.onClick.AddListener(() => OnItemPurchase(item));
                    }
                }
            }
        }
    }

    public void OnItemPurchase(ShopItemClass purchasedItem)
    {
        
        if (purchasedItem == null)
        {
            Debug.LogError("purchasedItem is null!");
            return;
        }
        Debug.Log($"Текущий бюджет: {currentOrder.Budget}, Стоимость товара: {purchasedItem.Cost}");

        if (IsItemAlreadyPurchased(purchasedItem))
        {
            Debug.Log($"Предмет {purchasedItem.Name} уже куплен!");
            return; // Прерываем выполнение, если предмет уже в инвентаре
        }
        playerInventory.ClearInventory();
        if (currentOrder.Budget >= purchasedItem.Cost)
        {
            // Списываем бюджет
            currentOrder.Budget -= purchasedItem.Cost;

            // Добавляем товар в инвентарь
            InventoryObject inventoryObject = new InventoryObject
            {
                ItemName = purchasedItem.Name,
                ItemSprite = purchasedItem.sprite,
                ItemCost = purchasedItem.Cost,
                ItemCategory = purchasedItem.Type,
                ItemPrefab = purchasedItem.Prefab,
                
            };
            GameDataManager.Instance.AddToInventory(inventoryObject);
            Debug.Log($"Товар {purchasedItem.Name} куплен. Остаток бюджета: {currentOrder.Budget}");
            playerInventory.DisplayInventory();

        }
        else
        {
            Debug.Log("Недостаточно средств для покупки!");
        }

    }
        public List<ShopItemClass> GetAvailableItems()
    {
        return items;
    }
    public void SetCurrentOrder(Order order)
    {
        currentOrder = order;
    }

    private bool IsItemAlreadyPurchased(ShopItemClass item)
    {
        List<InventoryObject> inventory = GameDataManager.Instance.GetInventory();
        foreach (var inventoryItem in inventory)
        {
            if (inventoryItem.ItemName == item.Name)
            {
                return true; // Если такой товар уже есть, возвращаем true
            }
        }
        return false; // Товара нет в инвентаре
    }

}

