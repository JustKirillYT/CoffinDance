using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButtonClick : MonoBehaviour
{
    [SerializeField]
    public OrderManager manager;
    public bool isCreated = false;
    [SerializeField]
    public Order order;
    public void OnMouseDown()
    {
        
        if (!isCreated) {
            order = manager.CreateOrder();
            Debug.Log(order);
            manager.DisplayOrderDetails(order);
            CurrentDataOrder.CurrentOrder = order;
            isCreated = true;
        }
        else
        {
            Debug.Log($"Заказ уже существует - {order.ClientName}\n" +
                $"Её заказ - {order.DesiredItems.Count}\n" +
                $"Тип - {order.OrderType}");
            manager.DisplayOrderDetails(order);
        }

    }

    public void OnMouseEnter()
    {
        
    }
}
