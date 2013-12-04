using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHeuristicHetmans.Core
{
    public interface IEvaluateFunction
    {
        double Evaluation(StepVariant variant);
    }
}
