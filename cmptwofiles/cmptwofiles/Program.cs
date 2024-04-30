using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmptwofiles
{
    class Program
    {
        static int cf = 0, cs = 0, c = 0;
        static void Main(string[] args)
        {
            string file1Path = string.Empty;
            string file2Path = string.Empty;
            string fileName1 = string.Empty;
            string fileName2 = string.Empty;
            if (args.Length >= 1)
            {
                file1Path = args[0];
                file2Path = args[1];
            }
            else
            {
                //file1Path = @"D:\NESProjects\CMPFiles\April2024-IDS.txt";
                //file2Path = @"D:\NESProjects\CMPFiles\Feb2024-IDS.txt";
                //file1Path = @"April2024-IDS.txt";
                //file2Path = @"Feb2024-IDS.txt";
                file1Path = ConfigurationManager.AppSettings["File1Path"];
                file2Path = ConfigurationManager.AppSettings["File2Path"];

            }
            //if(file1Path.Length ==0)
            //{
            //    file1Path = @"April2024-IDS.txt";
            //    file2Path = @"Feb2024-IDS.txt";
            //}

            fileName1 = Path.GetFileName(file1Path);
             fileName2 = Path.GetFileName(file2Path);

            // Output file paths
            //string file1OnlyPath = @"D:\NESProjects\CMPFiles\only1.txt";
            //string file2OnlyPath = @"D:\NESProjects\CMPFiles\only2.txt";
            //string bothFilesPath = @"D:\NESProjects\CMPFiles\common.txt";

            string file1OnlyPath = @"only1.txt";
            string file2OnlyPath = @"only2.txt";
            string bothFilesPath = @"common.txt";

            //StringBuilder sb1 = new StringBuilder();
            //StringBuilder sb2 = new StringBuilder();
            //StringBuilder sbc = new StringBuilder();
            //sb1.AppendLine("Exist in file1.txt AND Not exist in file2.txt");
            //sb2.AppendLine("Exist in file2.txt AND Not exist in file1.txt");
            //sbc.AppendLine("Exist in Feb2024-IDS.txt AND April2024-IDS.txt");

            // Create sets to store lines
            var file1Lines = new HashSet<string>();
            var file2Lines = new HashSet<string>();

            file1Lines.Add("Exist in "+ fileName1+" AND Not exist in "+ fileName2);
            //file2Lines.Add("Exist in Feb2024-IDS.txt AND Not exist in April2024-IDS.txt");
            file2Lines.Add("Exist in " + fileName2 + " AND Not exist in " + fileName1);

            // Read lines from both files
            ReadLinesIntoSet(file1Path, file1Lines, ref cf);
            ReadLinesIntoSet(file2Path, file2Lines, ref cs);

            // Compare lines
            var file1Only = new List<string>(file1Lines);
            /// file1Only.Add("Exist file1");
            file1Only.RemoveAll(file2Lines.Contains);


            var file2Only = new List<string>(file2Lines);
            file2Only.RemoveAll(file1Lines.Contains);


            var bothFiles = new List<string>(file1Lines);




            //bothFiles.Add("Exist in Feb2024-IDS.txt AND April2024-IDS.txt");
            bothFiles.RemoveAll(line => !file2Lines.Contains(line));
            // Add custom string at the top
            bothFiles.Insert(0, "Exist in " + fileName2 + " AND " + fileName1);


            //file2Lines.Add("Exist in " + fileName2 + " AND Not exist in " + fileName1);
            //sb1.AppendLine(file1Only.ToString());
            //sb2.AppendLine(file2Only.ToString());
            //sbc.AppendLine(bothFiles.ToString());
            // Write results to files
            cf = file1Only.Count;
            cs = file2Only.Count;
            c = bothFiles.Count;

            file1Only.Insert(1, "Total Count: " + cf);
            file2Only.Insert(1, "Total Count: " + cs);
            bothFiles.Insert(1, "Total Count: " + c);

            File.WriteAllLines(file1OnlyPath, file1Only);
            File.WriteAllLines(file2OnlyPath, file2Only);
            File.WriteAllLines(bothFilesPath, bothFiles);

            //File.WriteAllText(file1OnlyPath, sb1.ToString());
            //File.WriteAllText(file2OnlyPath, sb2.ToString());
            //File.WriteAllText(bothFilesPath, sbc.ToString());
            Console.WriteLine("File1 Only Toatal count: " + cf);
            Console.WriteLine("File2 Only Toatal count: " + cs);
            Console.WriteLine("Common Toatal count: " + c);

            Console.WriteLine("Comparison completed. Output files generated successfully.");
        }


        static void ReadLinesIntoSet(string filePath, HashSet<string> linesSet, ref int c)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    linesSet.Add(line);
                    c++;
                }
            }
        }


    }
}
