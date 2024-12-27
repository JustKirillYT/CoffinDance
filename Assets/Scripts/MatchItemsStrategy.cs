using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class BasicOrderEvaluationStrategy : IEvaluationStrategy
{
    public int Evaluate(Order order, List<GameObject> placedItems)
    {
        int score = 100;

        foreach (string desiredItem in order.DesiredItems)
        {
            bool itemFound = false;
            foreach (var placedItem in placedItems)
            {
                if (placedItem.CompareTag("addedItem") && placedItem.name == desiredItem)
                {
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
            {
                score -= 20; // За каждый ненайденный предмет +20 баллов
            }
        }

        return score;
    }
}