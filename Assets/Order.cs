using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public float Budget { get; set; } // Бюджет клиента
    public float TimeLimit { get; set; } // Срок выполнения заказа в минутах
    public string ClientName { get; set; }
    public List<string> DesiredItems { get; set; } // Список атрибутов, которые клиент хочет
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


    // Генерация случайного клиента
    public static Order GenerateRandomOrder()
    {
        var random = new System.Random();
        string[] names = { "Иван Иванов", "Анна Петрова", "Олег Сидоров", "Марина Алексеева" };
        string[] orderTypes = { "VIP", "Обычный", "Эконом" };
        List<string> allItems = new List<string> { "Гроб", "Цветы", "Музыка", "Украшения", };
        string type = orderTypes[random.Next(orderTypes.Length)];
        string name = names[random.Next(names.Length)];
        string clientType = orderTypes[random.Next(orderTypes.Length)];

        // Сгенерировать случайное количество атрибутов, которые хочет клиент
        int itemsCount = random.Next(1, 4); // от 1 до 3 предметов
        List<string> desiredItems = new List<string>();
        for (int i = 0; i < itemsCount; i++)
        {
            string item = allItems[random.Next(allItems.Count)];
            if (!desiredItems.Contains(item)) // Убедиться, что предмет не повторяется
            {
                desiredItems.Add(item);
            }
        }
        // Генерация случайного бюджета и времени
        float budget;
        if (clientType == "VIP") budget = random.Next(2000, 4000);
        else if (clientType == "Обычный") budget = random.Next(1000, 2500);
        else budget = random.Next(100, 500);
        float timeLimit = random.Next(10, 60); // Время от 10 до 60 минут
        return new Order(budget, desiredItems, timeLimit, name, type);
    }
}
