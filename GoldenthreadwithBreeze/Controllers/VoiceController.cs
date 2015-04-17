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
    public class VoiceController : ApiController
    {
        private GoldenThreadEntities db = new GoldenThreadEntities();
        readonly EFContextProvider<GoldenThreadEntities> _contextProvider = new EFContextProvider<GoldenThreadEntities>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }
        // GET api/Voice
        [HttpGet]
        [BasicAuthentication]
        public IQueryable<VoiceViewModel> Get()
        {
            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);

            int APIId = (from a in db.Applications
                         where a.APIKey == appKey
                         select a.ID).FirstOrDefault();
            List<VoiceViewModel> Voc = new List<VoiceViewModel>();
            try
            {
                Voc = (from v in db.Voices
                       join c in db.Chapters on v.IDChapter equals c.ID
                       join a in db.Applications on c.IDApp equals a.ID
                       join aa in db.Authors on v.AuthorID equals aa.ID
                       join t in db.Tags on v.IDTag equals t.ID
                       where a.ID == APIId
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


        [HttpPut]
        public IHttpActionResult Edit(int id, Voice voice)
        {

            if (id != voice.ID)
            {
                return BadRequest();
            }

            try
            {
                db.USP_GoldenThreadVoiceEdit(id,
                    voice.AuthorID,
                    voice.Status,
                    voice.Type,
                    voice.Image,
                    voice.Video,
                    voice.Text,
                    voice.IDChapter,
                    voice.CreatedBy);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoiceExists(id))
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


        // POST api/Voice
        [BasicAuthentication]
        [HttpPost]
        public IHttpActionResult Add(Voice voice)
        {
            try
            {
                // voice.CreatedDate = System.DateTime.UtcNow;
                db.USP_GoldenThreadVoiceAdd(voice.AuthorID, voice.Status, voice.Type, voice.Image, voice.Video, voice.Text, voice.IDTag, voice.IDChapter, voice.CreatedBy);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(HttpStatusCode.OK);
        }

        [HttpGet]
        [BasicAuthentication]
        public IQueryable<Author> GetAuthor()
        {
            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);

            int APIId = (from a in db.Applications
                         where a.APIKey == appKey
                         select a.ID).FirstOrDefault();
            List<Author> Author = new List<Author>();
            try
            {
                Author = (from a in db.USP_GetVoiceAuthorsByAPIId(APIId)
                          select new Author
                          {
                              ID = (int)(a.ID),
                              CreatedBy = a.CreatedBy,
                              CreatedDate = (DateTime)(a.CreatedDate),
                              IDFacebook = a.IDFacebook,
                              IDGoogle = a.IDGoogle,
                              IDInstagram = a.IDInstagram,
                              IDTwitter = a.IDTwitter,
                              Status = a.Status,
                              UserID = a.UserID
                          }
                              ).Distinct().ToList();
            }
            catch (Exception ex)
            {

                BadRequest(ex.Message);
            }
            return Author.AsQueryable();
        }

        [HttpGet]
        [BasicAuthentication]
        public IQueryable<VoiceViewModel> GetVoicesByStatus(string Status)
        {
            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);

            int APIId = (from a in db.Applications
                         where a.APIKey == appKey
                         select a.ID).FirstOrDefault();
            List<VoiceViewModel> Voc = new List<VoiceViewModel>();

            Voc = (from v in db.Voices
                   join c in db.Chapters on v.IDChapter equals c.ID
                   join a in db.Applications on c.IDApp equals a.ID
                   join aa in db.Authors on v.AuthorID equals aa.ID
                   join t in db.Tags on v.IDTag equals t.ID
                   where v.Status == Status && a.ID == APIId
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
            return Voc.AsQueryable();
        }

        [HttpGet]
        [BasicAuthentication]
        public IQueryable<Tag> GetTags()
        {
            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);

            int APIId = (from a in db.Applications
                         where a.APIKey == appKey
                         select a.ID).FirstOrDefault();
            List<Tag> Voc = new List<Tag>();
            try
            {
                Voc = (from t in db.USP_GetTagsByAPIId(APIId)
                       select new Tag
                       {
                           ID = (int)(t.ID),
                           Tag1 = t.Tag

                       }
                           ).Distinct().ToList();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Voc.AsQueryable();
        }

        [HttpDelete]
        [BasicAuthentication]
        public IHttpActionResult Remove(int id)
        {
            Voice voice = db.Voices.Find(id);
            if (voice == null)
            {
                return NotFound();
            }
            try
            {
                db.Voices.Remove(voice);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(HttpStatusCode.OK);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoiceExists(int id)
        {
            return db.Voices.Count(e => e.ID == id) > 0;
        }
    }
}