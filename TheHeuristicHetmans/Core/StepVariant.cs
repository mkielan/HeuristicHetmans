using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHeuristicHetmans.Core
{
    public class StepVariant : ICloneable
    {
        public Board Board { get; set; }

        public double Rating { get; set; }

        public object Clone()
        {
            var obj = new StepVariant();

            obj.Board = (Board) Board.Clone();
            obj.Rating = Rating;

            return obj;
        }
    }
}
