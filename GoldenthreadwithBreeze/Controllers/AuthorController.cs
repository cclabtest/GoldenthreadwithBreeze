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
    public class AuthorController : ApiController
    {
        private GoldenThreadEntities db = new GoldenThreadEntities();
        readonly EFContextProvider<GoldenThreadEntities> _contextProvider = new EFContextProvider<GoldenThreadEntities>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [BasicAuthentication]
        [HttpGet]
        public IQueryable<VoiceViewModel> GetVoice(int id)
        {
            string apikeyheader = HttpContext.Current.Request.Headers["AuthToken"];
            Guid appKey = Guid.Parse(apikeyheader);

            int APIId = (from a in db.Applications
                         where a.APIKey == appKey
                         select a.ID).FirstOrDefault();

            List<VoiceViewModel> voice = new List<VoiceViewModel>();
            try
            {
                voice = (from v in db.Voices
                         join c in db.Chapters on v.IDChapter equals c.ID
                         join a in db.Applications on c.IDApp equals a.ID
                         join aa in db.Authors on v.AuthorID equals aa.ID
                         join t in db.Tags on v.IDTag equals t.ID
                         where a.ID == APIId && v.AuthorID == id
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
                             VoiceStatus = v.Status
                         }
                           ).Distinct().ToList();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }

            return voice.AsQueryable();
        }

        

        // DELETE api/Author/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();

            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.ID == id) > 0;
        }
    }
}