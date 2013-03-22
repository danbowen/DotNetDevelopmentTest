using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroceryStoreTestLib;
using NUnit.Framework;

namespace GroceryStoreTestLibTests
{
    [TestFixture]
    public class SimulationTests
    {
        [Test]
        public void Test1()
        {
            var config = new StringConfig(
                @"1
A 1 2
A 2 1");

            var sim = new Simulation(config);
            sim.Run();

            Assert.That(sim.ResultTime, Is.EqualTo(7));
        }

          [Test]
        public void Test2()
        {
            var config = new StringConfig(
                 @"2
A 1 5
B 2 1
A 3 5
B 5 3
A 8 2");

            var sim = new Simulation(config);
            sim.Run();

            Assert.That(sim.ResultTime, Is.EqualTo(13));
        }

          [Test]
          public void Test3()
          {
              var config = new StringConfig(
                   @"2
A 1 2
A 1 2
A 2 1
A 3 2");

              var sim = new Simulation(config);
              sim.Run();

              Assert.That(sim.ResultTime, Is.EqualTo(6));
          }

          [Test]
          public void Test4()
          {
              var config = new StringConfig(
                   @"2
A 1 2
A 1 3
A 2 1
A 2 1");

              var sim = new Simulation(config);
              sim.Run();

              Assert.That(sim.ResultTime, Is.EqualTo(9));
          }

          [Test]
          public void Test5()
          {
              var config = new StringConfig(
                   @"2
A 1 3
A 1 5
A 3 1
B 4 1
A 4 1");

              var sim = new Simulation(config);
              sim.Run();

              Assert.That(sim.ResultTime, Is.EqualTo(11));
          }
     
    }

}
