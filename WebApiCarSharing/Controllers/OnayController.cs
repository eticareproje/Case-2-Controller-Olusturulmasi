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
    public class OnayController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: api/Onay
        public IQueryable<Onay> GetOnay()
        {
            return db.Onay;
        }

        // GET: api/Onay/5
        [ResponseType(typeof(Onay))]
        public IHttpActionResult GetOnay(int id)
        {
            Onay onay = db.Onay.Find(id);
            if (onay == null)
            {
                return NotFound();
            }

            return Ok(onay);
        }

        // PUT: api/Onay/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOnay(int id, Onay onay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != onay.OnayId)
            {
                return BadRequest();
            }

            db.Entry(onay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnayExists(id))
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

        // POST: api/Onay
        [ResponseType(typeof(Onay))]
        public IHttpActionResult PostOnay(Onay onay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Onay.Add(onay);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = onay.OnayId }, onay);
        }

        // DELETE: api/Onay/5
        [ResponseType(typeof(Onay))]
        public IHttpActionResult DeleteOnay(int id)
        {
            Onay onay = db.Onay.Find(id);
            if (onay == null)
            {
                return NotFound();
            }

            db.Onay.Remove(onay);
            db.SaveChanges();

            return Ok(onay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OnayExists(int id)
        {
            return db.Onay.Count(e => e.OnayId == id) > 0;
        }
    }
}