using ClosedXML.Excel;
using Shahnameh.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=--;Initial Catalog=--;User ID=--;Password=--";
            var path = @"C:\---\---\---\";

            var postFiles = new Dictionary<long, string>();

            foreach (var file in Directory.EnumerateFiles(path, "*.xlsx"))
            {
                if (!long.TryParse(Path.GetFileName(file).Split('-')[0].Trim(), out var index)) continue;

                postFiles[index] = file;
            }

            using (var workbook = new XLWorkbook(Path.Combine(path, "Posts.xlsx")))
            {
                var importer = new PostImporter(connectionString);
                long id = 1;
                foreach (var row in workbook.Worksheet("Sheet1").Rows())
                {
                    var content = File.ReadAllBytes(Path.Combine(path, string.Format("MP3\\{0:0000}.mp3", id)));
                    importer.ImportPost(id, row.Cell("A").Value.ToString(), row.Cell("B").Value.ToString(), row.Cell("C").Value.ToString(), content);

                    if (postFiles.TryGetValue(id, out var fileName))
                    {
                        importer.ImportPostAnalysis(id, fileName);
                    }

                    id++;
                }

            }
        }
    }
}
