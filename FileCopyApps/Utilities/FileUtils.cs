using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.Utilities
{
    public class FileUtils
    {
        internal List<string> ReadEachRow(string fileName)
        {
            string filePath = Path.Combine("C:\\", fileName);

            if (File.Exists(filePath))
            {
                return new List<string>(File.ReadAllLines(filePath));
            }
            else
            {
                Console.WriteLine($"File {filePath} does not exist.");
                return new List<string>();
            }
        }
    }
}
