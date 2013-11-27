using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHeuristicHetmans.Core
{
    public class StandardEvaluateFunction : IEvaluateFunction
    {
        public double Evaluation(StepVariant variant)
        {
            //todo
            if (!variant.Board.WithoutConflict) return -1;

            var count = 0;
            for (var i = 0; i < variant.Board.Length; i++)
            {
                if(variant.Board.Spacing[i] >= 0) count++;
            }

            if (count % 1 == 1)
            {
                return 0.8;
            }
            else return 0.9;

            //return 0.0;
        }
    }
}
