using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreTestLib
{
    public abstract class Customer
    {
        protected Customer(CustomerType type)
        {
            Type = type;
        }

        public int Id { get; set; }
        public CustomerType Type { get; protected set; }
        public int Arrival { get; set; }
        public double Items { get; set; }

        public abstract Register SelectRegister(IEnumerable<Register> registers);

        public Customer Clone()
        {
            var clone = Create(Type);
            clone.Id = this.Id;
            clone.Type = this.Type;
            clone.Arrival = this.Arrival;
            clone.Items = this.Items;
            return clone;
        }

        public static Customer Create(CustomerType customerType)
        {
            if (customerType == CustomerType.A)
            {
                return new CustomerA();
            }
            else if (customerType == CustomerType.B)
            {
                return new CustomerB();
            }
            else
            {
                throw new ArgumentException("Unrecognized customer type: " + customerType);
            }
        }
    }

    class CustomerB : Customer
    {
        public CustomerB():base(CustomerType.B)
        {}

        public override Register SelectRegister(IEnumerable<Register> registers)
        {
            var existsEmptyRegister = registers.Any(r => r.LineSizeAtTime(Arrival) == 0);
            if (existsEmptyRegister)
            {
                return registers.First(r => r.LineSizeAtTime(Arrival) == 0);
            }

            var fewestLastCustomerItems = registers.Min(r => r.ItemsForLastCustomerAtTime(Arrival));
            return registers.First(r => r.ItemsForLastCustomerAtTime(Arrival) == fewestLastCustomerItems);
        }

      
    }

    public class CustomerA : Customer
    {
        public CustomerA():base(CustomerType.A)
        {}


        public override Register SelectRegister(IEnumerable<Register> registers)
        {
            var minLineSize = registers.Min(r => r.LineSizeAtTime(Arrival));
            return registers.First(r => r.LineSizeAtTime(Arrival) == minLineSize);
        }

      
    }

    public enum CustomerType
    {
        A,
        B
    }
}