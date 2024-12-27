using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButtonClick : MonoBehaviour
{
    [SerializeField]
    public OrderManager manager;
    public bool isCreated = false;
    public Order order;
    public void OnMouseDown()
    {
        
        if (!isCreated) {
            order = manager.CreateOrder();
            Debug.Log(order);
            manager.DisplayOrderDetails(order);
            isCreated = true;
        }
        else
        {
            Debug.Log($"����� ��� ���������� - {order.ClientName}\n" +
                $"Ÿ ����� - {order.DesiredItems.Count}\n" +
                $"��� - {order.OrderType}");
            manager.DisplayOrderDetails(order);
        }

    }
}
