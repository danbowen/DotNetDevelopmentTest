using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryStoreTestLib
{
    public class Register
    {
        private readonly string _id;

        public Register(double itemsPerMinute, string id)
        {
            _id = id;
            ItemsPerMinute = itemsPerMinute;
            QueueHistory = new List<Customer>[1000];
        }

        public double ItemsPerMinute { get; set; }
        public List<Customer>[] QueueHistory { get; set; }
        public int LastTime { get; set; }

        public void AddCustomer(Customer customer)
        {
            int start = customer.Arrival;

            ValidateListForTime(start);

            MoveForwardInTimeSettingStateInLine(customer, start);
        }

        public int LineSizeAtTime(int time)
        {
            if (QueueHistory[time] == default(List<Customer>)) return 0;

            return QueueHistory[time].Count;
        }

        public int ItemsForLastCustomerAtTime(int time)
        {
            if (QueueHistory[time] == default(List<Customer>)) return 0;

            return (int) Math.Ceiling(QueueHistory[time].Last().Items);
        }

        private void MoveForwardInTimeSettingStateInLine(Customer customer, int start)
        {
            int time = start;
            var clone = customer.Clone();
            while (true)
            {
                ValidateListForTime(time);
                QueueHistory[time].Add(clone);
                
                bool customerIsInCheckoutPosition = QueueHistory[time].Count == 1;
                if (customerIsInCheckoutPosition)
                {
                    clone.Items = clone.Items - this.ItemsPerMinute;
                    if (clone.Items <= 0)
                    {
                        LastTime = time;
                        break;
                    }
                }
                clone = clone.Clone();
                time++;
            }
        }

        public void ValidateListForTime(int time)
        {
            if (QueueHistory[time] == default(List<Customer>))
            {
                QueueHistory[time]=new List<Customer>();
            }
        }

        public override string ToString()
        {
            var bldr = new StringBuilder();
            bldr.AppendLine("Register: " + _id );
            for (int i = 0; i < LastTime; i++)
            {
                bldr.AppendLine("t" + i + ": " +  CreateStringForLine(QueueHistory[i]));
            }

            return bldr.ToString();
        }

        private string CreateStringForLine(List<Customer> customers)
        {
            if (customers == default(List<Customer>))
            {
                return "[]";
            }
            var bldr = new StringBuilder();
            foreach (var customer in customers)
            {
                bldr.AppendFormat("[{0}{1}{2}]", customer.Id, customer.Type, customer.Items);
            }
            return bldr.ToString();
        }
    }
}