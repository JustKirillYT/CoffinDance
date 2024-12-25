using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public float Budget { get; set; } // ������ �������
    public float TimeLimit { get; set; } // ���� ���������� ������ � �������
    public string ClientName { get; set; }
    public List<string> DesiredItems { get; set; } // ������ ���������, ������� ������ �����
    [SerializeField]
    public List<string> allItems;
    public string OrderType { get; set; }


    public Order(float budget, List<string> desiredItems, float timeLimit, string clientName, string orderType)
    {
        Budget = budget;
        DesiredItems = desiredItems;
        TimeLimit = timeLimit;
        ClientName = clientName;
    OrderType = orderType;
    }


    // ��������� ���������� �������
    public static Order GenerateRandomOrder()
    {
        var random = new System.Random();
        string[] names = { "���� ������", "���� �������", "���� �������", "������ ���������" };
        string[] orderTypes = { "VIP", "�������", "������" };
        List<string> allItems = new List<string> { "����", "�����", "������", "���������", };
        string type = orderTypes[random.Next(orderTypes.Length)];
        string name = names[random.Next(names.Length)];
        string clientType = orderTypes[random.Next(orderTypes.Length)];

        // ������������� ��������� ���������� ���������, ������� ����� ������
        int itemsCount = random.Next(1, 4); // �� 1 �� 3 ���������
        List<string> desiredItems = new List<string>();
        for (int i = 0; i < itemsCount; i++)
        {
            string item = allItems[random.Next(allItems.Count)];
            if (!desiredItems.Contains(item)) // ���������, ��� ������� �� �����������
            {
                desiredItems.Add(item);
            }
        }
        // ��������� ���������� ������� � �������
        float budget;
        if (clientType == "VIP") budget = random.Next(2000, 4000);
        else if (clientType == "�������") budget = random.Next(1000, 2500);
        else budget = random.Next(100, 500);
        float timeLimit = random.Next(10, 60); // ����� �� 10 �� 60 �����
        return new Order(budget, desiredItems, timeLimit, name, type);
    }
}
