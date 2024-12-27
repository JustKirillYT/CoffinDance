using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public float Budget { get; set; } // Бюджет клиента
    public float TimeLimit { get; set; } // Срок выполнения заказа в минутах
    public string ClientName { get; set; }
    public List<string> DesiredItems { get; set; } // Список атрибутов, которые клиент хочет
    public string OrderType { get; set; }

    public Order(float budget, List<string> desiredItems, float timeLimit, string clientName, string orderType)
    {
        Budget = budget;
        DesiredItems = desiredItems;
        TimeLimit = timeLimit;
        ClientName = clientName;
        OrderType = orderType;
    }

    // Генерация случайного заказа на основе доступных товаров
    public static Order GenerateRandomOrder(List<ShopItemClass> availableItems)
    {
        if (availableItems == null || availableItems.Count == 0)
        {
            Debug.LogError("Список доступных товаров пуст!");
            return null;
        }

        var random = new System.Random();
        string[] names = { "Иван Иванов", "Анна Петрова", "Олег Сидоров", "Марина Алексеева" };
        string[] orderTypes = { "VIP", "Обычный", "Эконом" };

        string type = orderTypes[random.Next(orderTypes.Length)];
        string name = names[random.Next(names.Length)];

        // Сгенерировать случайное количество атрибутов, которые хочет клиент
        int itemsCount = random.Next(1, 4); // от 1 до 3 предметов
        List<string> desiredItems = new List<string>();
        for (int i = 0; i < itemsCount; i++)
        {
            ShopItemClass randomItem = availableItems[random.Next(availableItems.Count)];
            if (!desiredItems.Contains(randomItem.Name)) // Убедиться, что предмет не повторяется
            {
                desiredItems.Add(randomItem.Name);
            }
        }

        // Генерация случайного бюджета и времени
        float budget = type switch
        {
            "VIP" => random.Next(2000, 4000),
            "Обычный" => random.Next(1000, 2500),
            _ => random.Next(300, 500),
        };
        
        float timeLimit = random.Next(10, 60); // Время от 10 до 60 минут
        Order newOrder = new Order(budget, desiredItems, timeLimit, name, type);
       GameDataManager.CurrentOrder = newOrder;
        return newOrder;
    }
}
