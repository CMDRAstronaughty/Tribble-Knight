using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "input.csv";
        string outputFile = "output.csv";
        SplitSemicolonData(inputFile, outputFile);
    }

    static void SplitSemicolonData(string inputFile, string outputFile)
    {
        var lines = File.ReadAllLines(inputFile);
        var header = lines[0];
        var data = lines.Skip(1);

        var splitData = new List<string[]>();

        foreach (var line in data)
        {
            var columns = line.Split(',');
            var splitRows = new List<string[]>() { columns };

            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].Contains(";"))
                {
                    var newSplitRows = new List<string[]>();
                    foreach (var row in splitRows)
                    {
                        foreach (var splitValue in row[i].Split(';'))
                        {
                            var newRow = (string[])row.Clone();
                            newRow[i] = splitValue.Trim();
                            newSplitRows.Add(newRow);
                        }
                    }
                    splitRows = newSplitRows;
                }
            }

            splitData.AddRange(splitRows);
        }

        using (var writer = new StreamWriter(outputFile))
        {
            writer.WriteLine(header);
            foreach (var row in splitData)
            {
                writer.WriteLine(string.Join(",", row));
            }
        }
    }
}
