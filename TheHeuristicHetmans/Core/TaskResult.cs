using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHeuristicHetmans.Core
{
    public class TaskResult
    {
        public Board Board { get; set; }
        public int StepCount { get; set; }
        public int BackCount { get; set; }
    }
}
