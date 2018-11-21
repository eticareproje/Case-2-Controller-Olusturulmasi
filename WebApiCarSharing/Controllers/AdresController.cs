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
    public class AdresController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Adres
        public IQueryable<Adres> GetAdres()
        {
            return db.Adres;
        }

        // GET: api/Adres/5
        [ResponseType(typeof(Adres))]
        public IHttpActionResult GetAdres(int id)
        {
            Adres adres = db.Adres.Find(id);
            if (adres == null)
            {
                return NotFound();
            }

            return Ok(adres);
        }

        // PUT: api/Adres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdres(int id, Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adres.AdresId)
            {
                return BadRequest();
            }

            db.Entry(adres).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresExists(id))
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

        // POST: api/Adres
        [ResponseType(typeof(Adres))]
        public IHttpActionResult PostAdres(Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Adres.Add(adres);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adres.AdresId }, adres);
        }

        // DELETE: api/Adres/5
        [ResponseType(typeof(Adres))]
        public IHttpActionResult DeleteAdres(int id)
        {
            Adres adres = db.Adres.Find(id);
            if (adres == null)
            {
                return NotFound();
            }

            db.Adres.Remove(adres);
            db.SaveChanges();

            return Ok(adres);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdresExists(int id)
        {
            return db.Adres.Count(e => e.AdresId == id) > 0;
        }
    }
}