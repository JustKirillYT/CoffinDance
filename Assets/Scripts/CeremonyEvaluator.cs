using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CeremonyEvaluator
    {
        private readonly IEvaluationStrategy _evaluationStrategy;

        public CeremonyEvaluator(IEvaluationStrategy evaluationStrategy)
        {
            _evaluationStrategy = evaluationStrategy;
        }

        public int Evaluate(Order order, List<GameObject> placedItems)
        {
            return _evaluationStrategy.Evaluate(order, placedItems);
        }
    }
}
