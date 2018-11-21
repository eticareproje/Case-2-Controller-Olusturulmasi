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
    public class UyeBilgiController : ApiController
    {
        private RentACarEntities db = new RentACarEntities();
        //anasayfada araçların listelenmesi geri dönüş olarak araç ıd ve lokasyon bilgileri gelecek
        // GET: api/UyeBilgi
        public IQueryable<UyeBilgi> GetUyeBilgi()
        {
            return db.UyeBilgi;
        }
        //anasyafada tıklanan aracın bilgileri listelenecek
        // GET: api/UyeBilgi/5
        [ResponseType(typeof(UyeBilgi))]
        public IHttpActionResult GetUyeBilgi(int id)
        {
            UyeBilgi uyeBilgi = db.UyeBilgi.Find(id);
            if (uyeBilgi == null)
            {
                return NotFound();
            }

            return Ok(uyeBilgi);
        }
        
        // PUT: api/UyeBilgi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUyeBilgi(int id, UyeBilgi uyeBilgi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uyeBilgi.UserId)
            {
                return BadRequest();
            }

            db.Entry(uyeBilgi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UyeBilgiExists(id))
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
        //üye kayıt olurken gelen bilgileri veri tabanına kaydeder
        // POST: api/UyeBilgi
        [ResponseType(typeof(UyeBilgi))]
        public IHttpActionResult PostUyeBilgi(UyeBilgi uyeBilgi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        UyeBilgi bilgi = db.UyeBilgi.Where(c => c.EMail == uyeBilgi.EMail).FirstOrDefault();

            if (bilgi == null)
            {
                db.UyeBilgi.Add(uyeBilgi);
                db.SaveChanges();
                return CreatedAtRoute("Kullanıcı Eklendi", new { id = uyeBilgi.UserId }, uyeBilgi);
            }
            else
            {
                return CreatedAtRoute("Kullanıcı eklenemedi", new { id = uyeBilgi.UserId }, uyeBilgi);


            }

        }

        // DELETE: api/UyeBilgi/5
        [ResponseType(typeof(UyeBilgi))]
        public IHttpActionResult DeleteUyeBilgi(int id)
        {
            UyeBilgi uyeBilgi = db.UyeBilgi.Find(id);
            if (uyeBilgi == null)
            {
                return NotFound();
            }

            db.UyeBilgi.Remove(uyeBilgi);
            db.SaveChanges();

            return Ok(uyeBilgi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UyeBilgiExists(int id)
        {
            return db.UyeBilgi.Count(e => e.UserId == id) > 0;
        }
    }
}