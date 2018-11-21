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
    public class ResimController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Resim
        public IQueryable<Resim> GetResim()
        {
            return db.Resim;
        }

        // GET: api/Resim/5
        [ResponseType(typeof(Resim))]
        public IHttpActionResult GetResim(int id)
        {
            Resim resim = db.Resim.Find(id);
            if (resim == null)
            {
                return NotFound();
            }

            return Ok(resim);
        }

        // PUT: api/Resim/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResim(int id, Resim resim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resim.ResimId)
            {
                return BadRequest();
            }

            db.Entry(resim).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResimExists(id))
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

        // POST: api/Resim
        [ResponseType(typeof(Resim))]
        public IHttpActionResult PostResim(Resim resim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resim.Add(resim);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resim.ResimId }, resim);
        }

        // DELETE: api/Resim/5
        [ResponseType(typeof(Resim))]
        public IHttpActionResult DeleteResim(int id)
        {
            Resim resim = db.Resim.Find(id);
            if (resim == null)
            {
                return NotFound();
            }

            db.Resim.Remove(resim);
            db.SaveChanges();

            return Ok(resim);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResimExists(int id)
        {
            return db.Resim.Count(e => e.ResimId == id) > 0;
        }
    }
}