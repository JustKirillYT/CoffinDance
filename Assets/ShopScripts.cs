using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ShopScripts : MonoBehaviour
{
    [SerializeField] public List<RectTransform> categoryPanels; // ������ ������� ���������;
    [SerializeField] public List<string> categoryNames;
    [SerializeField] private GameObject shopItemPrefab; // ������ ������
    [SerializeField] private List<ShopItemClass> items; // ������ �������
    [SerializeField]
    private Dictionary<string, RectTransform> categoryPanelMap;


    public Order currentOrder; // ������ �� ������� �����
    private float remainingBudget; // ������� �������

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
        // ������������� ����������� �������
        if (currentOrder != null)
        {
            remainingBudget = currentOrder.Budget;
        }

        // ���������� ��������
        PopulateShop();
    }

    public void PopulateShop()
    {
        // ������� ������ �������� �� ���� ������� ���������
        foreach (var panel in categoryPanels)
        {
            foreach (Transform child in panel)
            {
                Destroy(child.gameObject);
            }
        }

        // ���������, ���� �� ������ � ������
        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("������ ������� ����!");
            return;
        }

        // ������ �������� ��� ������� ������
        foreach (var item in items)
        {
            // ���������� ������ ��������� ������
            int categoryIndex = categoryNames.IndexOf(item.Type);
            if (categoryIndex >= 0 && categoryIndex < categoryPanels.Count)
            {
                // ������ ������� ��������
                GameObject shopItem = Instantiate(shopItemPrefab, categoryPanels[categoryIndex]);

                // ����������� ����������� ������
                ShopItemClass shopItemUI = shopItem.GetComponent<ShopItemClass>();
                if (shopItemUI != null)
                {
                    var itemNameText = shopItem.transform.Find("ItemName").GetComponent<Text>();
                    var itemPriceText = shopItem.transform.Find("ItemPrice").GetComponent<Text>();
                    var itemImage = shopItem.transform.Find("ItemImage").GetComponent<Image>();

                    itemNameText.text = item.Name;
                    itemPriceText.text = $"{item.Cost} $";
                    itemImage.sprite = item.sprite; // ������������� ����������� ������
                }
            }
            else
            {
                Debug.LogWarning($"��������� {item.Type} �� �������!");
                foreach (KeyValuePair<string, RectTransform> entry in categoryPanelMap)
                {
                    Debug.Log($"���������: {entry.Key}, ������: {entry.Value.gameObject.name}");
                }
            }
        }
    }

    //public void PopulateShop()
    //{
    //    // ������� ������ ��������, ���� ��� ����
    //    foreach (Transform child in categoryPanels)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // ���������, ���� �� ������ � ������
    //    if (items == null || items.Count == 0)
    //    {
    //        Debug.LogWarning("������ ������� ����!");
    //        return;
    //    }

    //    // ������ �������� ��� ������� ������

    //    foreach (var item in items)
    //    {
    //        // ���������, ���������� �� ������ ��� ��������� ���������
    //        if (categoryPanelMap.TryGetValue(item.Type, out RectTransform targetPanel))
    //        {
    //            // ������ ������� ��������
    //            GameObject shopItem = Instantiate(shopItemPrefab, targetPanel);

    //            // ����������� �������
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
    //            Debug.LogWarning($"��������� {item.Type} �� �������!");
    //        }
    //    }
}


