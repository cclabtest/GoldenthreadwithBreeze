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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BreezeController]
    public class ApplicationController : ApiController
    {
        private GoldenThreadEntities db = new GoldenThreadEntities();
        readonly EFContextProvider<GoldenThreadEntities> _contextProvider = new EFContextProvider<GoldenThreadEntities>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        // GET api/Application
         [BasicAuthentication]
        public IQueryable<Application> Get()
        {
            return db.Applications;
        }
       
        [HttpPost]
        [BasicAuthentication]
        public IHttpActionResult Add(Application application)
        {
            try
            {
                db.USP_GoldenThreadApplicationAdd(application.CreatedBy);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, application);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = application.ID }));
                response.Content = new StringContent("Create Successfully..!!");
                return Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        // DELETE api/Application/5       
        [HttpDelete]
        [BasicAuthentication]
        public IHttpActionResult Remove(int id)
        {
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return NotFound();
            }
            try
            {
                db.Applications.Remove(application);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(HttpStatusCode.OK);
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
            try
            {
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
                           VoiceStatus = v.Status
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

        private bool ApplicationExists(int id)
        {
            return db.Applications.Count(e => e.ID == id) > 0;
        }
    }
}