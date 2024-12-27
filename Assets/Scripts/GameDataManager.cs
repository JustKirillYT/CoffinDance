using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; set; }
    private static Order _currentOrder;
    public static List<InventoryObject> PlayerInventory { get; set; } = new List<InventoryObject>(); // Инвентарь игрока
    public static Order CurrentOrder
    {
        get { return _currentOrder; }
        set
        {
            Debug.Log("CurrentOrder изменён: " + value); // Логируем изменение
            _currentOrder = value;
        }
    }

  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Этот объект не уничтожится при смене сцены
        }
        else
        {
            Destroy(gameObject); // Если объект уже существует, уничтожаем новый
        }
    }

    // Метод для добавления предметов в инвентарь
    public void AddToInventory(InventoryObject item)
    {
        PlayerInventory.Add(item);
    }

    // Метод для очистки инвентаря
    public void ClearInventory()
    {
        PlayerInventory.Clear();
    }

    public List<InventoryObject> GetInventory()
    {
        return PlayerInventory;
    }
}

