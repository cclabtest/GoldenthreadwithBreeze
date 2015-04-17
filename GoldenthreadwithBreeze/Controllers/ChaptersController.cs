using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GoldenthreadwithBreeze.Models;
using Breeze.ContextProvider.EF6;
using Breeze.ContextProvider;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using System.Web.Http.OData;
using System.Web.Http.Cors;
using System.Web;


namespace GoldenthreadwithBreeze.Controllers
{
    [BreezeController]
    [BreezeJsonFormatter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]    
    public class ChaptersController : ApiController
    {
        private GoldenThreadEntities db = new GoldenThreadEntities();        
        readonly EFContextProvider<GoldenThreadEntities> _contextProvider = new EFContextProvider<GoldenThreadEntities>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }
        // GET api/Chapters
        [HttpGet]
        [BasicAuthentication]
       public IQueryable<Chapter> Get()
        {
            int APIId = 0;
            try
            {
                string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
                Guid appKey = Guid.Parse(apikeyheader);

                 APIId = (from a in db.Applications
                             where a.APIKey == appKey
                             select a.ID).FirstOrDefault();

                
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return db.Chapters.Where(x => x.IDApp == APIId);
        }

        // PUT api/Chapters/5
        [HttpPut]
        public IHttpActionResult Edit(int id, Chapter chapter)
        {

            if (id != chapter.ID)
            {
                return BadRequest();
            }

            try
            {
                db.USP_GoldenThreadChapterEdit(id, chapter.IDApp, chapter.Title, chapter.ModifiedBy);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }



        // POST api/Chapters
       // [BasicAuthentication]
        [HttpPost]
        [BasicAuthentication]
        public IHttpActionResult Add(Chapter chapter)
        {

            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);

            int APIId = (from a in db.Applications
                         where a.APIKey == appKey
                         select a.ID).FirstOrDefault();
            try
            {
                if (APIId > 0)
                    db.USP_GoldenThreadChapterAdd(APIId, chapter.Title, chapter.CreatedBy);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, chapter);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = chapter.ID }));
                return Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;
            }
        }


        // DELETE api/Chapters/5
       // [BasicAuthentication]
        [HttpDelete]
        [BasicAuthentication]
        public IHttpActionResult Remove(int id)
        {
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return NotFound();
            }

            db.Chapters.Remove(chapter);
            db.SaveChanges();

            return Ok(HttpStatusCode.OK);
        }

        [HttpGet]
        [BasicAuthentication]
        public IQueryable<VoiceViewModel> GetVoices(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);
            List<VoiceViewModel> Voc = new List<VoiceViewModel>();
            try
            {
                int APIId = (from a in db.Applications
                             where a.APIKey == appKey
                             select a.ID).FirstOrDefault();


                Voc = (from v in db.Voices
                       join c in db.Chapters on v.IDChapter equals c.ID
                       join a in db.Applications on c.IDApp equals a.ID
                       join aa in db.Authors on v.AuthorID equals aa.ID
                       join t in db.Tags on v.IDTag equals t.ID
                       where c.ID == id && a.ID == APIId
                       select new VoiceViewModel
                       {
                           ID = (int)(v.ID),
                           AuthorID = (int)(v.AuthorID),
                           ChapterTitle = c.Title,
                           CreatedDate = (DateTime)(v.CreatedDate),
                           IDChapter = (int)(v.IDChapter),
                           Image = v.Image,
                           Tag = t.Tag1,
                           Text = v.Text,
                           Type = v.Type,
                           Video = v.Video,
                           VoiceStatus = v.Status,
                           IDFacebook = aa.IDFacebook,
                           IDGoogle = aa.IDGoogle,
                           IDInstagram = aa.IDInstagram,
                           IDTwitter = aa.IDTwitter,
                           UserName = aa.User.UserName
                       }
                           ).ToList();

            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Voc.AsQueryable();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChapterExists(int id)
        {
            return db.Chapters.Count(e => e.ID == id) > 0;
        }
    }
}