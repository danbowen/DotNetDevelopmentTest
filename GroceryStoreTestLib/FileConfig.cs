using System.Collections.Generic;
using System.IO;

namespace GroceryStoreTestLib
{
    public interface IConfig
    {
        int NumberOfRegisters { get;  }
        List<Customer> Customers { get;  }
    }

    public class FileConfig : IConfig
    {
        private readonly string _filePath;
        private  StringConfig _stringConfig ;
        public FileConfig(string filePath)
        {
            _filePath = filePath;
        
        }

        public void Load()
        {
            FileStream file = File.OpenRead(_filePath);
            var filereader = new StreamReader(file);
            var contents = filereader.ReadToEnd();
            
            _stringConfig=new StringConfig(contents);
        }


        public int NumberOfRegisters
        {
            get { return _stringConfig.NumberOfRegisters; }
        }

        public List<Customer> Customers
        {
            get { return _stringConfig.Customers; }
        }
    }

  
}