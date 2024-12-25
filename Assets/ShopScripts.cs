using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ShopScripts : MonoBehaviour
{
    [SerializeField] public List<RectTransform> categoryPanels; // Список панелей категорий;
    [SerializeField] public List<string> categoryNames;
    [SerializeField] private GameObject shopItemPrefab; // Префаб товара
    [SerializeField] private List<ShopItemClass> items; // Список товаров
    [SerializeField]
    private Dictionary<string, RectTransform> categoryPanelMap;


    public Order currentOrder; // Ссылка на текущий заказ
    private float remainingBudget; // Остаток бюджета

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
        // Инициализация оставшегося бюджета
        if (currentOrder != null)
        {
            remainingBudget = currentOrder.Budget;
        }

        // Заполнение магазина
        PopulateShop();
    }

    public void PopulateShop()
    {
        // Удаляем старые элементы из всех панелей категорий
        foreach (var panel in categoryPanels)
        {
            foreach (Transform child in panel)
            {
                Destroy(child.gameObject);
            }
        }

        // Проверяем, есть ли товары в списке
        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("Список товаров пуст!");
            return;
        }

        // Создаём элементы для каждого товара
        foreach (var item in items)
        {
            // Определяем индекс категории товара
            int categoryIndex = categoryNames.IndexOf(item.Type);
            if (categoryIndex >= 0 && categoryIndex < categoryPanels.Count)
            {
                // Создаём элемент магазина
                GameObject shopItem = Instantiate(shopItemPrefab, categoryPanels[categoryIndex]);

                // Настраиваем отображение товара
                ShopItemClass shopItemUI = shopItem.GetComponent<ShopItemClass>();
                if (shopItemUI != null)
                {
                    var itemNameText = shopItem.transform.Find("ItemName").GetComponent<Text>();
                    var itemPriceText = shopItem.transform.Find("ItemPrice").GetComponent<Text>();
                    var itemImage = shopItem.transform.Find("ItemImage").GetComponent<Image>();

                    itemNameText.text = item.Name;
                    itemPriceText.text = $"{item.Cost} $";
                    itemImage.sprite = item.sprite; // Устанавливаем изображение товара
                }
            }
            else
            {
                Debug.LogWarning($"Категория {item.Type} не найдена!");
                foreach (KeyValuePair<string, RectTransform> entry in categoryPanelMap)
                {
                    Debug.Log($"Категория: {entry.Key}, Панель: {entry.Value.gameObject.name}");
                }
            }
        }
    }

    //public void PopulateShop()
    //{
    //    // Удаляем старые элементы, если они есть
    //    foreach (Transform child in categoryPanels)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // Проверяем, есть ли товары в списке
    //    if (items == null || items.Count == 0)
    //    {
    //        Debug.LogWarning("Список товаров пуст!");
    //        return;
    //    }

    //    // Создаём элементы для каждого товара

    //    foreach (var item in items)
    //    {
    //        // Проверяем, существует ли панель для указанной категории
    //        if (categoryPanelMap.TryGetValue(item.Type, out RectTransform targetPanel))
    //        {
    //            // Создаём элемент магазина
    //            GameObject shopItem = Instantiate(shopItemPrefab, targetPanel);

    //            // Настраиваем элемент
    //            ShopItemClass shopItemUI = shopItem.GetComponent<ShopItemClass>();
    //            if (shopItemUI != null)
    //            {
    //                var itemNameText = shopItem.transform.Find("ItemName").GetComponent<Text>();
    //                var itemPriceText = shopItem.transform.Find("ItemPrice").GetComponent<Text>();
    //                var itemImage = shopItem.transform.Find("ItemImage").GetComponent<Image>();
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"Категория {item.Type} не найдена!");
    //        }
    //    }
}


