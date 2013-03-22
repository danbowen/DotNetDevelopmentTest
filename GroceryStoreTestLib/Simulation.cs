using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GroceryStoreTestLib
{
    public class Simulation
    {
        private readonly IConfig _config;
        private const double TrainingRegisterSpeed=.5;
        private const double RegularRegisterSpeed=1;
        private List<Register> _registers=new List<Register>(); 
        

        public Simulation(IConfig config)
        {
            _config = config;
        }

        public void Run()
        {
            CreateRegisters();
            var customers = LoadAndSortCustomers();

            foreach (var customer in customers)
            {
                var register = SelectRegisterForCustomer(customer);

                register.AddCustomer(customer);
            }

            ResultTime = _registers.Max(r => r.LastTime)+1;

            LogResults();
        }

        public int ResultTime { get; private set; }

        public IEnumerable<Register> Registers { get { return _registers; } }

        private Register SelectRegisterForCustomer(Customer customer)
        {
            if (customer.Type == CustomerType.A) return SelectRegisterForAType(customer.Arrival);
            if (customer.Type == CustomerType.B) return SelectRegisterForBType(customer.Arrival);

            throw new Exception("Unrecognized customer type.");

        }

        private Register SelectRegisterForAType(int arrivalTime)
        {
            var minLineSize = _registers.Min(r => r.LineSizeAtTime(arrivalTime));
            return _registers.First(r => r.LineSizeAtTime(arrivalTime) == minLineSize);

        }

        private Register SelectRegisterForBType(int arrivalTime)
        {
            var existsEmptyRegister = _registers.Any(r => r.LineSizeAtTime(arrivalTime) == 0);
            if (existsEmptyRegister)
            {
                return _registers.First(r => r.LineSizeAtTime(arrivalTime) == 0);
            }

            var fewestLastCustomerItems = _registers.Min(r => r.ItemsForLastCustomerAtTime(arrivalTime));
            return _registers.First(r => r.ItemsForLastCustomerAtTime(arrivalTime) == fewestLastCustomerItems);

        }

        private void CreateRegisters()
        {
            for (int i = 0; i < _config.NumberOfRegisters; i++)
            {
                bool isTrainingRegister = (i == _config.NumberOfRegisters - 1);
                double itemsPerMinute = isTrainingRegister
                                            ? TrainingRegisterSpeed
                                            : RegularRegisterSpeed;

                _registers.Add(new Register(itemsPerMinute, i.ToString(CultureInfo.InvariantCulture)));
            }
        }

        private IEnumerable<Customer> LoadAndSortCustomers()
        {
            return new List<Customer>(_config.Customers.OrderBy(c => c.Arrival)
                                           .ThenBy(c => c.Items)
                                           .ThenBy(c => c.Type));
        }

        private void LogResults()
        {
            var writer =new StreamWriter(File.Create("simOutput.txt"));
            foreach (var register in Registers)
            {
                writer.WriteLine(register.ToString());
            }

            writer.Close();
        }
    }
}