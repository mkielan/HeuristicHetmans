using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHeuristicHetmans.Core
{
    public class Board : ICloneable
    {
        /// <summary>
        /// Rozstawienie hetmanów na planszy
        /// </summary>
        //public bool[,] Spacing { get; private set; }
        public int[] Spacing { get; private set; }
        public int Length 
        {
            get 
            {
                return Spacing.Length;
                //return Spacing.GetLength(0);
            }
        }

        public bool WithoutConflict
        {
            get
            {
                /*for (var i = 0; i < Length; i++)
                {
                    for (var j = 0; j < Length; j++)
                    {
                        if (Spacing[i, j] == true)
                        {
                            if (SearchFor(i, j))
                            {
                                return false;
                            }

                            break;
                        }
                    }
                }*/
                var list = new List<int>();

                for (var i = 1; i < Length; i++)
                {
                    var element = Spacing[i];

                    if (element >= 0)
                    {
                        list.Clear();

                        for (var k = - 1; k <= 1; k++)
                        {
                            if (element + k >= 0) list.Add(element + k);
                        }

                        if (list.Contains(Spacing[i - 1]))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        private bool SearchFor(int x, int y)
        {
            for (var pozX = x > 0 ? x - 1 : x; pozX <= x + 1 && pozX < Length; pozX++)
            {
                for (var pozY = y > 0 ? y - 1 : y; pozY <= y + 1 && pozY < Length; pozY++)
                {
                    //if (!(pozX == x && pozY == y) && Spacing[pozX, pozY])
                    if (!(pozX == x && pozY == y) && Spacing[pozX] == pozY)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Get true if board is complete
        /// </summary>
        public bool Complete
        {
            get
            {/*
                var sum = 0;
                for (var i = 0; i < Length; i++)
                {
                    for (var j = 0; j < Length; j++)
                    {
                        sum += Spacing[i, j] == true ? 1 : 0;
                    }
                }
                */

                //return sum == Length;

                foreach (var i in Spacing)
                {
                    if (i < 0) return false;
                }

                return true;
            }
        }

        public Board(int n)
        {
            //Spacing = new bool[n,n];
            Spacing = new int[n];

            for(var i = 0; i < n; i++) Spacing[i] = -1;
        }

        public object Clone()
        {
            var obj = new Board(Length);

            //obj.Spacing = (bool[,]) Spacing.Clone();
            obj.Spacing = (int[])Spacing.Clone();

            return obj;
        }
    }
}
