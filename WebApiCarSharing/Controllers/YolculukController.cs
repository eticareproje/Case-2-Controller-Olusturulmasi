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
    public class YolculukController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Yolculuk
        public IQueryable<Yolculuk> GetYolculuk()
        {
            return db.Yolculuk;
        }

        // GET: api/Yolculuk/5
        [ResponseType(typeof(Yolculuk))]
        public IHttpActionResult GetYolculuk(int id)
        {
            Yolculuk yolculuk = db.Yolculuk.Find(id);
            if (yolculuk == null)
            {
                return NotFound();
            }

            return Ok(yolculuk);
        }

        // PUT: api/Yolculuk/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutYolculuk(int id, Yolculuk yolculuk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != yolculuk.YolculukId)
            {
                return BadRequest();
            }

            db.Entry(yolculuk).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YolculukExists(id))
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

        // POST: api/Yolculuk
        [ResponseType(typeof(Yolculuk))]
        public IHttpActionResult PostYolculuk(Yolculuk yolculuk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Yolculuk.Add(yolculuk);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = yolculuk.YolculukId }, yolculuk);
        }

        // DELETE: api/Yolculuk/5
        [ResponseType(typeof(Yolculuk))]
        public IHttpActionResult DeleteYolculuk(int id)
        {
            Yolculuk yolculuk = db.Yolculuk.Find(id);
            if (yolculuk == null)
            {
                return NotFound();
            }

            db.Yolculuk.Remove(yolculuk);
            db.SaveChanges();

            return Ok(yolculuk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool YolculukExists(int id)
        {
            return db.Yolculuk.Count(e => e.YolculukId == id) > 0;
        }
    }
}