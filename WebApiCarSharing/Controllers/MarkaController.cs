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
    public class MarkaController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Marka
        public IQueryable<Marka> GetMarka()
        {
            return db.Marka;
        }

        // GET: api/Marka/5
        [ResponseType(typeof(Marka))]
        public IHttpActionResult GetMarka(int id)
        {
            Marka marka = db.Marka.Find(id);
            if (marka == null)
            {
                return NotFound();
            }

            return Ok(marka);
        }

        // PUT: api/Marka/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarka(int id, Marka marka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marka.MarkaId)
            {
                return BadRequest();
            }

            db.Entry(marka).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkaExists(id))
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

        // POST: api/Marka
        [ResponseType(typeof(Marka))]
        public IHttpActionResult PostMarka(Marka marka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marka.Add(marka);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = marka.MarkaId }, marka);
        }

        // DELETE: api/Marka/5
        [ResponseType(typeof(Marka))]
        public IHttpActionResult DeleteMarka(int id)
        {
            Marka marka = db.Marka.Find(id);
            if (marka == null)
            {
                return NotFound();
            }

            db.Marka.Remove(marka);
            db.SaveChanges();

            return Ok(marka);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarkaExists(int id)
        {
            return db.Marka.Count(e => e.MarkaId == id) > 0;
        }
    }
}