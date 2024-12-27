using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<InventoryObject> InventoryItems { get; private set; } = new List<InventoryObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���������� ������ ��� ����� �����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ����� ��� ���������� �������� � ���������
    public void AddItem(InventoryObject item)
    {
        InventoryItems.Add(item);
    }

    // ����� ��� ��������� ���� ���������
    public List<InventoryObject> GetItems()
    {
        return InventoryItems;
    }
}

