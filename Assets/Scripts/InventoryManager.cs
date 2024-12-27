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
            DontDestroyOnLoad(gameObject); // Не уничтожать объект при смене сцены
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Метод для добавления предмета в инвентарь
    public void AddItem(InventoryObject item)
    {
        InventoryItems.Add(item);
    }

    // Метод для получения всех предметов
    public List<InventoryObject> GetItems()
    {
        return InventoryItems;
    }
}

