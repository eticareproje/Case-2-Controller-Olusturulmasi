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
    public class AracController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Arac
        public IQueryable<Arac> GetArac()
        {
            return db.Arac;
        }

        // GET: api/Arac/5
        [ResponseType(typeof(Arac))]
        public IHttpActionResult GetArac(int id)
        {
            Arac arac = db.Arac.Find(id);
            if (arac == null)
            {
                return NotFound();
            }

            return Ok(arac);
        }

        // PUT: api/Arac/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArac(int id, Arac arac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arac.AracId)
            {
                return BadRequest();
            }

            db.Entry(arac).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AracExists(id))
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

        // POST: api/Arac
        [ResponseType(typeof(Arac))]
        public IHttpActionResult PostArac(Arac arac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Arac.Add(arac);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = arac.AracId }, arac);
        }

        // DELETE: api/Arac/5
        [ResponseType(typeof(Arac))]
        public IHttpActionResult DeleteArac(int id)
        {
            Arac arac = db.Arac.Find(id);
            if (arac == null)
            {
                return NotFound();
            }

            db.Arac.Remove(arac);
            db.SaveChanges();

            return Ok(arac);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AracExists(int id)
        {
            return db.Arac.Count(e => e.AracId == id) > 0;
        }
    }
}