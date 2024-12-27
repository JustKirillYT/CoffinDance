using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CeremonyManager : MonoBehaviour
{
    [SerializeField] private Button conductCeremonyButton; // Кнопка "Провести Церемонию"
    [SerializeField] private Text resultText; // Текст для отображения результата
    [SerializeField] private AudioSource ceremonySound; // Аудиоисточник для звука

    private void Start()
    {
        if (conductCeremonyButton != null)
        {
            conductCeremonyButton.onClick.AddListener(ConductCeremony);
        
        }
    }

    private void ConductCeremony()
    {
        // Получаем текущий заказ и размещённые объекты
        List<GameObject> placedItems = GetPlacedItems();

        Debug.Log(GameDataManager.CurrentOrder.OrderType);
        // Определяем стратегию оценки в зависимости от типа клиента
        IEvaluationStrategy evaluationStrategy;
        string strategyName;

        if (GameDataManager.CurrentOrder.OrderType == "VIP")
        {
            evaluationStrategy = new VIPOrderEvaluationStrategy();
            strategyName = "VIPOrderEvaluationStrategy";
        }
        else if (GameDataManager.CurrentOrder.OrderType == "Эконом")
        {
            evaluationStrategy = new EconomyOrderEvaluationStrategy();
            strategyName = "EconomyOrderEvaluationStrategy";
        }
        else
        {
            evaluationStrategy = new BasicOrderEvaluationStrategy();
            strategyName = "BasicOrderEvaluationStrategy";
        }

        // Лог выбранной стратегии
        Debug.Log($"Используется стратегия оценки: {strategyName}");

        // Используем стратегию оценки
        CeremonyEvaluator evaluator = new CeremonyEvaluator(evaluationStrategy);
        int score = evaluator.Evaluate(GameDataManager.CurrentOrder, placedItems);

        // Отображаем результат
        Debug.Log($"Оценка клиента: {score}/100");
        resultText.text = $"Оценка клиента: {score}/100";
        PlayCeremonySound();
        // Удаляем предметы из инвентаря после церемонии
        RemoveItemsFromInventory(placedItems);
    }

    private List<GameObject> GetPlacedItems()
    {
        // Находим все объекты, добавленные на сцену (по тегу или другой логике)
        GameObject[] placedObjects = GameObject.FindGameObjectsWithTag("addedItem");
        return new List<GameObject>(placedObjects);
    }

    private void RemoveItemsFromInventory(List<GameObject> placedItems)
    {
        foreach (var placedItem in placedItems)
        {
            // Предполагаем, что на сцене объект имеет имя, которое соответствует имени предмета в инвентаре
            string itemName = placedItem.name;

            // Найдем предмет в инвентаре по имени (или используйте уникальный ID, если нужно)
            InventoryObject item = GameDataManager.PlayerInventory.Find(i => i.ItemName == itemName);

            if (item != null)
            {
                // Удаляем предмет из инвентаря
                GameDataManager.PlayerInventory.Remove(item);
                Debug.Log($"Предмет {item.ItemName} удалён из инвентаря.");
            }
            else
            {
                Debug.LogWarning($"Предмет с именем {itemName} не найден в инвентаре.");
            }
        }


    }
    private void PlayCeremonySound()
    {
        if (ceremonySound != null)
        {
            ceremonySound.Play();
            Debug.Log("Звук церемонии проигрывается.");
        }
        else
        {
            Debug.LogWarning("Аудиоисточник не найден!");
        }
    }
}
