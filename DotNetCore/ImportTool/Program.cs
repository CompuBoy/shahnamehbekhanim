using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using Shahnameh.Import;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Shahnameh.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;

namespace ImportTool
{
    class Program
    {
        static void Main(string[] args)
        {
           var config = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json", false, true)
                     .Build();


            var connectionString = config.GetConnectionString("Database");
            var options = new DbContextOptionsBuilder<DataModel>();
            options.EnableSensitiveDataLogging(true);
            options.EnableDetailedErrors(true);

            if (config.GetValue<bool>("UseMySql")) 
            {
                options.UseMySql(connectionString, mySqlOptions => {
                    mySqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql);
                });
            } 
            else if (config.GetValue<bool>("UseSqlite"))
            {
                options.UseSqlite(connectionString);
            }
            else
            {
                options.UseSqlServer(connectionString);
            }

            var path = config.GetValue<string>("Path");

            var postFiles = new Dictionary<long, string>();

            foreach (var file in Directory.EnumerateFiles(path, "*.xlsx"))
            {
                if (!long.TryParse(Path.GetFileName(file).Split('-')[0].Trim(), out var index)) continue;

                postFiles[index] = file;
            }

            using (var workbook = new XLWorkbook(Path.Combine(path, "Posts.xlsx")))
            {
                var importer = new PostImporter(options.Options);
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
