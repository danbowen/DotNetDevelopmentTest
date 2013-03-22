using System;
using System.Collections.Generic;
using System.IO;

namespace GroceryStoreTestLib
{
    public class StringConfig:IConfig
    {
        public StringConfig(string contents)
        {
            Customers = new List<Customer>();
            SetConfigFromString(contents);
        }

        public void SetConfigFromString(string contents)
        {
            var reader = new StringReader(contents);

            var firstline = reader.ReadLine();

            NumberOfRegisters = int.Parse(firstline);

            int index = 0;
            while (reader.Peek() > 0)
            {
                var line = reader.ReadLine();
                
                var customer = CreateCustomerFromInputLine(line, index);

                Customers.Add(customer);
                index++;
            }
        }

        private static Customer CreateCustomerFromInputLine(string line, int index)
        {
            var linedata = line.Split(new[] { ' ' });
            var custType = (CustomerType)Enum.Parse(typeof(CustomerType), linedata[0], true);
            Customer customer = Customer.Create(custType);

            customer.Id = index;
            customer.Arrival = int.Parse(linedata[1]);
            customer.Items = int.Parse(linedata[2]);
            return customer;
        }

        public int NumberOfRegisters { get; set; }

        public List<Customer> Customers { get; set; }
    }
}