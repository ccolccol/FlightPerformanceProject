using System;
using System.IO;
using System.Collections.Generic;

namespace DataImport
{
    /// <summary> Data structure, wrapped dictionary inside.
    /// </summary>
    class DataReader
    {
        public Dictionary<string, double> dataDictionary { get; private set; }
            = new Dictionary<string, double>();

        public DataReader(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string line;
                string[] parts;
                char[] charSeparator = new char[] { ',' };
                while (reader.Peek() != -1)
                {
                    line = reader.ReadLine();
                    if (line.Contains("*")) continue;
                    parts = line.Split(charSeparator, 2);
                    dataDictionary.Add(parts[1].Trim(), double.Parse(parts[0].Trim()));
                }
                reader.Close();
            }
            else Console.WriteLine("File not exists"); // use console.writeline temporarily
        }

        public bool Contain(string key)
        {
            return dataDictionary.ContainsKey(key);
        }

        public double Import(string key)
        {
            double value;
            dataDictionary.TryGetValue(key, out value);
            return value;
        }
    }
}
