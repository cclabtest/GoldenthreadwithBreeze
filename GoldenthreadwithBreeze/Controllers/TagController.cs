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
    public class TagController : ApiController
    {
        private GoldenThreadEntities db = new GoldenThreadEntities();
        readonly EFContextProvider<GoldenThreadEntities> _contextProvider = new EFContextProvider<GoldenThreadEntities>();


        // GET api/Tag
        public IQueryable<Tag> GetTags()
        {
            return db.Tags;
        }

        // GET api/Tag/5
        [ResponseType(typeof(Tag))]
        public IHttpActionResult GetTag(int id)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

       
        // POST api/Tag
        [ResponseType(typeof(Tag))]
        public IHttpActionResult PostTag(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tags.Add(tag);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tag.ID }, tag);
        }

        // DELETE api/Tag/5
        [ResponseType(typeof(Tag))]
        public IHttpActionResult DeleteTag(int id)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            db.Tags.Remove(tag);
            db.SaveChanges();

            return Ok(tag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TagExists(int id)
        {
            return db.Tags.Count(e => e.ID == id) > 0;
        }
    }
}