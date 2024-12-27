using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private Text orderDetailsText;
    [SerializeField] private ShopScripts shopScripts; // ������ �� ������ ��������

   public Order CreateOrder()
{
    if (shopScripts == null)
    {
        Debug.LogError("ShopScripts �� ��������� � OrderManager!");
        return null;
    }

    List<ShopItemClass> availableItems = shopScripts.GetAvailableItems();
    if (availableItems == null || availableItems.Count == 0)
    {
        Debug.LogError("��� ��������� ������� ��� ��������� ������!");
        return null;
    }

    Order order = Order.GenerateRandomOrder(availableItems);
    GameDataManager.CurrentOrder = order;

    Debug.Log($"����� ������: {order.ClientName}, {order.OrderType}"); // �������� ���� ��� ��� �������
    return order;
}

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
