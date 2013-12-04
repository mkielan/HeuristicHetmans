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
            if (!variant.Board.WithoutConflict) return 1;
            if (variant.Board.Complete) return 0;

            var length = variant.Board.Length;
            var last = 0;
            var l = 0;

            for (; l < length; l++)
            {
                if (variant.Board.Spacing[l] == -1)
                {
                    if (l == 0) return 0;

                    last = variant.Board.Spacing[l-1];
                    break;
                }
            }

            var wsp = last == 0 || last == 2 ? 0.05 : 0.1;

            return wsp * (length - l)/length;
        }
    }
}
