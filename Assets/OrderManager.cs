using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    public Text orderDetailsText;


    public Order CreateOrder()
    {
        Order order;
        order = Order.GenerateRandomOrder();
        return order;
    }

    // Отображение данных о заказе
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