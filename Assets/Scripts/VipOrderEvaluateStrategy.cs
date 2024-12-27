using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class VIPOrderEvaluationStrategy : IEvaluationStrategy
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
                    score += 30; // VIP клиенты получают -30 баллов за каждый ненайденный предмет
                }
                else
                {
                    score += 15; // Если предмет нашли, штрафуем на +15
                }
            }

            // Ограничение минимального балла
            return Mathf.Max(0, score);
        }
    }
}
