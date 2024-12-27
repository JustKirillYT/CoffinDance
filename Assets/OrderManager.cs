using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private Text orderDetailsText;
    [SerializeField] private ShopScripts shopScripts; // Ссылка на скрипт магазина

   public Order CreateOrder()
{
    if (shopScripts == null)
    {
        Debug.LogError("ShopScripts не подключен к OrderManager!");
        return null;
    }

    List<ShopItemClass> availableItems = shopScripts.GetAvailableItems();
    if (availableItems == null || availableItems.Count == 0)
    {
        Debug.LogError("Нет доступных товаров для генерации заказа!");
        return null;
    }

    Order order = Order.GenerateRandomOrder(availableItems);
    GameDataManager.CurrentOrder = order;

    Debug.Log($"Заказ создан: {order.ClientName}, {order.OrderType}"); // Добавьте этот лог для отладки
    return order;
}

    public void DisplayOrderDetails(Order order)
    {

        string orderDetails = $"Клиент: {order.ClientName}\n" +
                              $"Тип клиента: {order.OrderType}\n" +
                              $"Бюджет: {order.Budget} $ \n" +
                              $"Срок выполнения: {order.TimeLimit} мин\n" +
                              $"Желаемые атрибуты: {string.Join(", ", order.DesiredItems)}";

        orderDetailsText.text = orderDetails;
    }
}
