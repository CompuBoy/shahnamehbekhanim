using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shahnameh.Models;

namespace Shahnameh.Import
{
    public class PostImporter
    {
        #region Fields

        DbContextOptions<DataModel> options;

        #endregion

        #region Constructors

        public PostImporter(DbContextOptions<DataModel> options)
        {
            this.options = options;
        }

        #endregion

        #region Methods

        public void ImportPost(long id, string category, string title, string narrator, byte[] voiceContent)
        {
            using (var model = new DataModel(options))
            {
                model.Database.EnsureCreated();
                
                var post = model.Posts.Where(p => p.ID == id).FirstOrDefault();
                if (post == null)
                {
                    post = new Models.Post { ID = id };
                    model.Posts.Add(post);
                }

                post.Category = category;
                post.Title = title;
                post.Narrator = narrator;

                var voice = model.Voices.Where(v => v.PostID == id).FirstOrDefault();
                if (voice == null)
                {
                    voice = new Models.Voice { PostID = id, };
                    model.Voices.Add(voice);
                }

                voice.Content = voiceContent;

                model.SaveChanges();
            }
        }

        public void ImportPostAnalysis(long id, string filename)
        {
            using (var model = new DataModel(options))
            {
                var post = model.Posts.Where(p => p.ID == id).First();

                using (var workbook = new XLWorkbook(filename))
                {
                    var versesSheet = workbook.Worksheets.Worksheet(1);
                    var index = 1;
                    var verses = model.Verses.Where(v => v.PostID == id).ToDictionary(v => v.Index);
                    foreach (var row in versesSheet.Rows().Skip(1))
                    {
                        var indexString = row.Cell("A").Value?.ToString();
                        if (string.IsNullOrEmpty(indexString)) break;

                        if (!verses.TryGetValue(index, out var verse))
                        {
                            verses[index] = verse = new Models.Verse { PostID = id, Index = index, };
                            model.Verses.Add(verse);
                        }

                        verse.Part1StartTime = decimal.Parse(row.Cell("B").Value.ToString());
                        verse.Part1EndTime = decimal.Parse(row.Cell("C").Value.ToString());
                        verse.Part2StartTime = decimal.Parse(row.Cell("D").Value.ToString());
                        verse.Part2EndTime = decimal.Parse(row.Cell("E").Value.ToString());
                        index++;
                    }

                    var phrasesSheet = workbook.Worksheets.Worksheet(2);
                    model.Phrases.RemoveRange(model.Phrases.Where(p => p.PostID == id));
                    foreach (var row in phrasesSheet.Rows().Skip(1))
                    {
                        var value = row.Cell("A").Value?.ToString();
                        if (string.IsNullOrEmpty(value)) break;

                        var phrase = new Models.Phrase { PostID = id, Value = value, };
                        phrase.VerseIndex = int.Parse(row.Cell("B").Value.ToString());
                        phrase.IsPresentInPart1 = int.Parse(row.Cell("C").Value.ToString()) == 1;
                        phrase.IsPresentInPart2 = int.Parse(row.Cell("D").Value.ToString()) == 1;
                        phrase.Description = row.Cell("E").Value.ToString();

                        model.Phrases.Add(phrase);
                    }

                    var personsSheet = workbook.Worksheets.Worksheet(3);
                    model.Persons.RemoveRange(model.Persons.Where(p => p.PostID == id));
                    foreach (var row in personsSheet.Rows().Skip(1))
                    {
                        var name = row.Cell("A").Value?.ToString();
                        if (string.IsNullOrEmpty(name)) break;

                        var person = new Models.Person { PostID = id, Name = name, };
                        person.VerseIndex = int.Parse(row.Cell("B").Value.ToString());
                        person.IsPresentInPart1 = int.Parse(row.Cell("C").Value.ToString()) == 1;
                        person.IsPresentInPart2 = int.Parse(row.Cell("D").Value.ToString()) == 1;

                        model.Persons.Add(person);
                    }
                }

                model.SaveChanges();
            }
        }

        #endregion
    }
}
