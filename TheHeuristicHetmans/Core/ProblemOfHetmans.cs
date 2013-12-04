using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHeuristicHetmans.Core
{
    public class ProblemOfHetmans
    {
        public IEvaluateFunction EvaluateFunction { get; set; }

        public ProblemOfHetmans()
        {
            EvaluateFunction = new StandardEvaluateFunction();
        }

        public TaskResult Solve(int n)
        {
            var variants = new List<StepVariant>();

            var backCount = 0;
            var stepCount = -1;
            variants.Add(new StepVariant { Board = new Board(n), Rating = 1 });

            var currentElement = 0;
            var variant = variants[currentElement];

            do {
                stepCount++;

                variants.RemoveAt(currentElement);
                var complete = variant.Board.Complete;
                var withoutConflicts = variant.Board.WithoutConflict;

                if (complete && withoutConflicts)
                {
                    break; //rozwiązanie
                }
                else
                {//przejście do następnego możliwego wieżchołka

                    if (!complete) //dodanie ewentualnych wariantów kolejnego kroku
                    {
                        if (withoutConflicts)
                        {
                            var newv = GenerateVariantsForCurrent(variant);

                            //oblicznie brakujących ocen
                            foreach (var v in newv)
                            {
                                v.Rating = EvaluateFunction.Evaluation(v);
                            }

                            variants.AddRange(newv);
                        }
                        else
                        {
                            backCount++;
                        }
                    }

                    //wybranie najlepszego niesprawdzonego elementu
                    variant = variants.FirstOrDefault(v => v.Rating == variants.Min(x => x.Rating));
                    currentElement = variants.IndexOf(variant);
                }
            }
            while(currentElement >= 0);

            if (variant != null)
            {
                return new TaskResult
                {
                    Board = variant.Board,
                    StepCount = stepCount,
                    BackCount = backCount
                };
            }
                
            return null;
        }

        private ICollection<StepVariant> GenerateVariantsForCurrent(StepVariant variant)
        {
            var row = 0;
            var length = variant.Board.Length;

            #region oszacowanie wiersza

            for (; row < length; row++)
            {
                if (variant.Board.Spacing[row] == -1)
                {
                    break;
                }
                else if (row == length - 1) return null;
            }
            #endregion

            #region generowanie wariantów

            var list = new List<StepVariant>();
            for (var i = 0; i < length; i++)
            {
                var copy = variant.Clone() as StepVariant;
                copy.Board.Spacing[row] = i;
                list.Add(copy);
            }

            #endregion

            return list;
        }
    }
}
