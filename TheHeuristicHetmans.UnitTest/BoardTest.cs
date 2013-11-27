using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheHeuristicHetmans.Core;

namespace TheHeuristicHetmans.UnitTest
{
    [TestClass]
    public class BroadTest
    {
        [TestMethod]
        public void WithoutConflictTest()
        {
            var a = new Board(5);
            //a.Spacing[0, 0] = true;
            //a.Spacing[0, 1] = true;
            
            a.Spacing[0] = 0;
            a.Spacing[1] = 1;
            Assert.AreEqual(a.WithoutConflict, false);

            //a.Spacing[0, 1] = true;
            //a.Spacing[2, 4] = true;
            a.Spacing[0] = 1;
            a.Spacing[1] = -1;
            a.Spacing[2] = 4;

            Assert.AreEqual(a.WithoutConflict, true);

            a.Spacing[2] = -1;
            a.Spacing[1] = 4;

            Assert.AreEqual(a.WithoutConflict, true);

            for (var i = 0; i < 5; i++)
            {
                a.Spacing[i] = i;
            }

            Assert.AreEqual(a.WithoutConflict, false);

            Assert.AreEqual(new Board(5).WithoutConflict, true);
        }

        [TestMethod]
        public void CompleteTest()
        {
            var a = new Board(5);
            var b = new Board(5);
            //b.Spacing[0, 1] = true;
            b.Spacing[0] = 1;

            var c = new Board(5);

            for (var i = 0; i < c.Length; i++ )
            {
                //c.Spacing[i, i] = true;
                c.Spacing[i] = i;
            }

            var e = a.Complete;
            Assert.AreEqual(a.Complete, false);
            Assert.AreEqual(b.Complete, false);
            Assert.AreEqual(c.Complete, true);
        }

        [TestMethod]
        public void LengthTest()
        {
            for (var i = 1; i < 55; i++)
            {
                Assert.AreEqual(new Board(i).Length, i);
                Assert.AreNotEqual(new Board(i).Length, i + 10);
            }
        }
    }
}
