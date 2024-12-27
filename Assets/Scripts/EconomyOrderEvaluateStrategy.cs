using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class EconomyOrderEvaluationStrategy : IEvaluationStrategy
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
                    score -= 10; // За каждый ненахъод предмет -10 баллов
                }
            }

            // + 0.5 за каждый найденный еще
            score += order.DesiredItems.Count * 5;

            return score;
        }
    }
}
