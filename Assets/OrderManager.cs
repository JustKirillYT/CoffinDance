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

    // ����������� ������ � ������
    public void DisplayOrderDetails(Order order)
    {
        string orderDetails = $"������: {order.ClientName}\n" +
                              $"��� �������: {order.OrderType}\n" +
                              $"������: {order.Budget} $ \n" +
                              $"���� ����������: {order.TimeLimit} ���\n" +
                              $"�������� ��������: {string.Join(", ", order.DesiredItems)}";

        orderDetailsText.text = orderDetails;
    }
}