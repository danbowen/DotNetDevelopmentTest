using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GroceryStoreTestLibTests
{
    [TestFixture]
    class AcceptanceTests
    {
        [Test]
        public void Test1()
        {
            var store = new GroceryStoreTestLib.GroceryStoreApp();
            var result=store.RunSimulation(@"input1.txt");
            Assert.That(result, Is.EqualTo(7));

        }

        [Test]
        public void Test2()
        {
            var store = new GroceryStoreTestLib.GroceryStoreApp();
            var result = store.RunSimulation(@"input2.txt");
            Assert.That(result, Is.EqualTo(13));
        }

        [Test]
        public void Test3()
        {
            var store = new GroceryStoreTestLib.GroceryStoreApp();
            var result = store.RunSimulation(@"input3.txt");
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Test4()
        {
            var store = new GroceryStoreTestLib.GroceryStoreApp();
            var result = store.RunSimulation(@"input4.txt");
            Assert.That(result, Is.EqualTo(9));
        }
        [Test]

        public void Test5()
        {
            var store = new GroceryStoreTestLib.GroceryStoreApp();
            var result = store.RunSimulation(@"input5.txt");
            Assert.That(result, Is.EqualTo(11));
        }
    }
}
