using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Json.Logic
{
    public class FileWriter
    {
        public void Save(string path, string value)
        {
            File.WriteAllText(path, value);
        }
    }
}
