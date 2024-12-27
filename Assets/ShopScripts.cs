using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScripts : MonoBehaviour
{
    [SerializeField] public List<RectTransform> categoryPanels; // ������ ���������
    [SerializeField] public List<string> categoryNames; // �������� ���������
    [SerializeField] private GameObject shopItemPrefab; // ������ ������
    [SerializeField] private List<ShopItemClass> items; // ��� ��������� ������
    [SerializeField] public PlayerInventory playerInventory; // ������ �� ���������
    [SerializeField] public Order currentOrder; // ������ �� ������� �����

    private Dictionary<string, RectTransform> categoryPanelMap; // ������� ��� ������������� ��������� � ��������

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
                Debug.LogWarning("���������� ��������� � ������� �� ���������!");
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
        // ������� ������ ������ �� �������
        foreach (var panel in categoryPanels)
        {
            foreach (Transform child in panel)
            {
                Destroy(child.gameObject);
            }
        }

        // ������ �������� ��� ���� �������
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
        Debug.Log($"������� ������: {currentOrder.Budget}, ��������� ������: {purchasedItem.Cost}");

        if (IsItemAlreadyPurchased(purchasedItem))
        {
            Debug.Log($"������� {purchasedItem.Name} ��� ������!");
            return; // ��������� ����������, ���� ������� ��� � ���������
        }
        playerInventory.ClearInventory();
        if (currentOrder.Budget >= purchasedItem.Cost)
        {
            // ��������� ������
            currentOrder.Budget -= purchasedItem.Cost;

            // ��������� ����� � ���������
            InventoryObject inventoryObject = new InventoryObject
            {
                ItemName = purchasedItem.Name,
                ItemSprite = purchasedItem.sprite,
                ItemCost = purchasedItem.Cost,
                ItemCategory = purchasedItem.Type,
                ItemPrefab = purchasedItem.Prefab,
                
            };
            GameDataManager.Instance.AddToInventory(inventoryObject);
            Debug.Log($"����� {purchasedItem.Name} ������. ������� �������: {currentOrder.Budget}");
            playerInventory.DisplayInventory();

        }
        else
        {
            Debug.Log("������������ ������� ��� �������!");
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
                return true; // ���� ����� ����� ��� ����, ���������� true
            }
        }
        return false; // ������ ��� � ���������
    }

}

