using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; set; }
    private static Order _currentOrder;
    public static List<InventoryObject> PlayerInventory { get; set; } = new List<InventoryObject>(); // ��������� ������
    public static Order CurrentOrder
    {
        get { return _currentOrder; }
        set
        {
            Debug.Log("CurrentOrder ������: " + value); // �������� ���������
            _currentOrder = value;
        }
    }

  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ������ �� ����������� ��� ����� �����
        }
        else
        {
            Destroy(gameObject); // ���� ������ ��� ����������, ���������� �����
        }
    }

    // ����� ��� ���������� ��������� � ���������
    public void AddToInventory(InventoryObject item)
    {
        PlayerInventory.Add(item);
    }

    // ����� ��� ������� ���������
    public void ClearInventory()
    {
        PlayerInventory.Clear();
    }

    public List<InventoryObject> GetInventory()
    {
        return PlayerInventory;
    }
}

