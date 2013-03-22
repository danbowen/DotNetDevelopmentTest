using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroceryStoreTestLib;

namespace GroceryStoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error: Must include filename");
                return;
            }
            try
            {
                var filename = args[0];

                var store = new GroceryStoreApp();
                var time = store.RunSimulation(filename);
                Console.WriteLine("Finished at: " + time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
    }
}
