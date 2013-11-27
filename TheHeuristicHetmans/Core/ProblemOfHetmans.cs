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
            var stepCount = 0;
            variants.Add(new StepVariant { Board = new Board(n), Rating = 1 });

            var currentElement = 0;
            var variant = variants[currentElement];

            do {
                stepCount++;

                variants.RemoveAt(currentElement);

                if (variant.Board.Complete && variant.Board.WithoutConflict)
                {
                    break; //rozwiązanie
                }
                else
                {//przejście do następnego możliwego wieżchołka

                    if (!variant.Board.Complete) //dodanie ewentualnych wariantów kolejnego kroku
                    {
                        if (variant.Board.WithoutConflict)
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
                    variant = variants.FirstOrDefault(v => v.Rating == variants.Max(x => x.Rating));
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
            //while (row < length)
            //{
            //    var i = 0;
            //    for (; i < length; i++)
            //    {
            //        if (variant.Board.Spacing[row, i] == true)
            //        {
            //            break;
            //        }
            //    }

            //    if (i < length) row++;
            //    else break;
            //}

            while (row < length)
            {
                var i = 0;
                for (; i < length; i++)
                {
                    //if (variant.Board.Spacing[row, i] == true)
                    if (variant.Board.Spacing[row] == i)
                    {
                        break;
                    }
                }

                if (i < length) row++;
                else break;
            }
            //row++;
            #endregion

            #region generowanie wariantów

            var list = new List<StepVariant>();
            for (var i = 0; i < length; i++)
            {//todo bo robiona jest płytka kopia, referencja
                var copy = variant.Clone() as StepVariant;
                //copy.Board.Spacing[row, i] = true;
                copy.Board.Spacing[row] = i;
                list.Add(copy);
            }

            #endregion

            return list;
        }
    }
}
