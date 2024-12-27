using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public float Budget { get; set; } // ������ �������
    public float TimeLimit { get; set; } // ���� ���������� ������ � �������
    public string ClientName { get; set; }
    public List<string> DesiredItems { get; set; } // ������ ���������, ������� ������ �����
    public string OrderType { get; set; }

    public Order(float budget, List<string> desiredItems, float timeLimit, string clientName, string orderType)
    {
        Budget = budget;
        DesiredItems = desiredItems;
        TimeLimit = timeLimit;
        ClientName = clientName;
        OrderType = orderType;
    }

    // ��������� ���������� ������ �� ������ ��������� �������
    public static Order GenerateRandomOrder(List<ShopItemClass> availableItems)
    {
        if (availableItems == null || availableItems.Count == 0)
        {
            Debug.LogError("������ ��������� ������� ����!");
            return null;
        }

        var random = new System.Random();
        string[] names = { "���� ������", "���� �������", "���� �������", "������ ���������" };
        string[] orderTypes = { "VIP", "�������", "������" };

        string type = orderTypes[random.Next(orderTypes.Length)];
        string name = names[random.Next(names.Length)];

        // ������������� ��������� ���������� ���������, ������� ����� ������
        int itemsCount = random.Next(1, 4); // �� 1 �� 3 ���������
        List<string> desiredItems = new List<string>();
        for (int i = 0; i < itemsCount; i++)
        {
            ShopItemClass randomItem = availableItems[random.Next(availableItems.Count)];
            if (!desiredItems.Contains(randomItem.Name)) // ���������, ��� ������� �� �����������
            {
                desiredItems.Add(randomItem.Name);
            }
        }

        // ��������� ���������� ������� � �������
        float budget = type switch
        {
            "VIP" => random.Next(2000, 4000),
            "�������" => random.Next(1000, 2500),
            _ => random.Next(300, 500),
        };
        
        float timeLimit = random.Next(10, 60); // ����� �� 10 �� 60 �����
        Order newOrder = new Order(budget, desiredItems, timeLimit, name, type);
       GameDataManager.CurrentOrder = newOrder;
        return newOrder;
    }
}
