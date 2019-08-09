using Shahnameh.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Globalization;

namespace Shahnameh.Controllers
{
    public class ApiController : Controller
    {
        public ActionResult Post(long id)
        {
            return this.DoWithDatabase(model =>
                {
                    var post = model.Posts.Where(p => p.ID == id).FirstOrDefault();
                    return Json(post, JsonRequestBehavior.AllowGet);
                }
                );
        }

        public ActionResult Voice(long postId)
        {
            return this.DoWithDatabase(model =>
            {
                var voice = model.Voices.Where(v => v.PostID == postId).FirstOrDefault();
                return File(voice.Content, "audio/mp3");
            });
        }

        public ActionResult Posts(string category, string narrator, int? from, int? count)
        {
            return this.DoWithDatabase(model =>
            {
                var posts = (IQueryable<Post>)model.Posts;

                if (!string.IsNullOrEmpty(category))
                {
                    posts = posts.Where(p => p.Category == category);
                }

                if (!string.IsNullOrEmpty(narrator))
                {
                    posts = posts.Where(p => p.Narrator == narrator);
                }

                if (from.HasValue)
                {
                    posts = posts.Skip(from.Value);
                }

                if (count.HasValue)
                {
                    posts = posts.Take(count.Value);
                }

                return Json(posts.Select(p => new { id = p.ID, title = p.Title, category = p.Category, narrator = p.Narrator, story = p.Story, }).ToList(), JsonRequestBehavior.AllowGet);
            });
        }

        public ActionResult PodcastPosts(string category, int? from, int? count)
        {
            return this.DoWithDatabase(model =>
            {
                var posts = (IQueryable<PodcastPost>)model.PodcastPosts;

                if (!string.IsNullOrEmpty(category))
                {
                    posts = posts.Where(p => p.Category == category);
                }

                if (from.HasValue)
                {
                    posts = posts.Skip(from.Value);
                }

                if (count.HasValue)
                {
                    posts = posts.Take(count.Value);
                }

                return Json(posts.ToList().Select(p => new { id = p.ID, title = p.Title, category = p.Category, description = p.Description, story = p.Story, url = p.PodcastUrl, date = ToPersianDate(p.PublishDate), }).ToList(), JsonRequestBehavior.AllowGet);
            });
        }

        string ToPersianDate(DateTime date)
        {
            var persianCalendar = new PersianCalendar();
            return $"{date.TimeOfDay.Hours:00}:{date.TimeOfDay.Minutes:00} {persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date):00}/{persianCalendar.GetDayOfMonth(date):00}";
        }

        public ActionResult PodcastPost(Guid id)
        {
            return this.DoWithDatabase(model =>
            {
                var posts = (IQueryable<PodcastPost>)model.PodcastPosts.Where(p => p.ID == id);
                
                return Json(posts.ToList().Select(p => new { id = p.ID, title = p.Title, category = p.Category, description = p.Description, story = p.Story, url = p.PodcastUrl, date = ToPersianDate(p.PublishDate), }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            });
        }

        public ActionResult Verses(long postId)
        {
            return this.DoWithDatabase(model =>
            {
                return Json(model.Verses.Where(v => v.PostID == postId).OrderBy(v => v.Index).Select(v => new { index = v.Index, part1 = new { start = v.Part1StartTime, end = v.Part1EndTime }, part2 = new { start = v.Part2StartTime, end = v.Part2EndTime } }).ToList(), JsonRequestBehavior.AllowGet);
            });
        }

         public ActionResult Persons(long postId)
        {
            return this.DoWithDatabase(model =>
            {
                return Json(model.Persons.Where(pe => pe.PostID == postId).Select(pe => new { name = pe.Name, verse = pe.VerseIndex, inPart1 = pe.IsPresentInPart1, inPart2 = pe.IsPresentInPart2, }).ToList(), JsonRequestBehavior.AllowGet);
            });
        }

        public ActionResult Phrases(long? postId, string startingWith, int? from, int? count)
        {
            return this.DoWithDatabase(model =>
            {
                var phrases = (IQueryable<Phrase>)model.Phrases;

                if (postId.HasValue)
                {
                    phrases = phrases.Where(ph => ph.PostID == postId.Value);
                }

                if (!string.IsNullOrEmpty(startingWith))
                {
                    phrases = phrases.Where(ph => ph.Value.StartsWith(startingWith));
                }

                if (from.HasValue)
                {
                    phrases = phrases.Skip(from.Value);
                }

                if (count.HasValue)
                {
                    phrases = phrases.Take(count.Value);
                }

                return Json(phrases.Select(ph => new { value = ph.Value, description = ph.Description, postId = ph.PostID, verse = ph.VerseIndex, inPart1 = ph.IsPresentInPart1, inPart2 = ph.IsPresentInPart2, }).ToList(), JsonRequestBehavior.AllowGet);
            });
        }

        public ActionResult Categories()
        {
            return this.DoWithDatabase(model =>
            {
                return Json(model.Posts.GroupBy(p =>  p.Category, (category, posts) => new { Category = category, MinPostId = posts.Min(p => p.ID), }).OrderBy(i => i.MinPostId).Select(i => i.Category).ToList(), JsonRequestBehavior.AllowGet);
            });
        }

        TResult DoWithDatabase<TResult>(Func<DataModel, TResult> function)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                using (var model = new DataModel(connection, false))
                {
                    return function(model);
                }
            }
        }
    }
}
