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
using WebApiCarSharing.Models;

namespace WebApiCarSharing.Controllers
{
    public class OdemelerController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Odemeler
        public IQueryable<Odemeler> GetOdemeler()
        {
            return db.Odemeler;
        }

        // GET: api/Odemeler/5
        [ResponseType(typeof(Odemeler))]
        public IHttpActionResult GetOdemeler(int id)
        {
            Odemeler odemeler = db.Odemeler.Find(id);
            if (odemeler == null)
            {
                return NotFound();
            }

            return Ok(odemeler);
        }

        // PUT: api/Odemeler/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOdemeler(int id, Odemeler odemeler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != odemeler.OdemeId)
            {
                return BadRequest();
            }

            db.Entry(odemeler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdemelerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Odemeler
        [ResponseType(typeof(Odemeler))]
        public IHttpActionResult PostOdemeler(Odemeler odemeler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Odemeler.Add(odemeler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = odemeler.OdemeId }, odemeler);
        }

        // DELETE: api/Odemeler/5
        [ResponseType(typeof(Odemeler))]
        public IHttpActionResult DeleteOdemeler(int id)
        {
            Odemeler odemeler = db.Odemeler.Find(id);
            if (odemeler == null)
            {
                return NotFound();
            }

            db.Odemeler.Remove(odemeler);
            db.SaveChanges();

            return Ok(odemeler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OdemelerExists(int id)
        {
            return db.Odemeler.Count(e => e.OdemeId == id) > 0;
        }
    }
}