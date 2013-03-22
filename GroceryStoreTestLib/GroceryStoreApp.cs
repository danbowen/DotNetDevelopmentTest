using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryStoreTestLib
{
    public class GroceryStoreApp
    {
        public int RunSimulation(string inputFile)
        {
            if (inputFile == null)
            {
                throw new ArgumentNullException("inputFile");
            }

            var fileConfig = new FileConfig(inputFile);
            fileConfig.Load();

            var simulation = new Simulation(fileConfig);
            simulation.Run();

            return simulation.ResultTime;
        }
    }
}
